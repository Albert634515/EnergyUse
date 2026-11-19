using System.Globalization;
using System.Text;
using EnergyUse.Core.Context;
using OfficeOpenXml;

namespace EnergyUse.Core.Manager;

public class LibEpplus
{

    private readonly EnergyUseContext _context;

    public LibEpplus(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);
    }

    public async Task ExportMeterReadings(string exportFileName, Models.EnergyType eneryType, long addressId)
    {
        var meterReadingRepo = new EnergyUse.Core.Repositories.RepoMeterReading(_context);
        List<EnergyUse.Models.MeterReading> meterReadings = (await meterReadingRepo.SelectByEnergyIdAndAddressId(eneryType.Id, addressId)).ToList();
        if (meterReadings.Count == 0)
        {
            throw new Exception("No data to export");
        }

        createExportFile(meterReadings, exportFileName, eneryType);
    }

    public async Task ExportMeterReadings(string exportFileName, Models.EnergyType eneryType, long addressId, DateTime startRange, DateTime endRange)
    {
        var meterReadingRepo = new Repositories.RepoMeterReading(_context);
        List<Models.MeterReading> meterReadings = (await meterReadingRepo.SelectByRange(startRange, endRange, eneryType.Id, addressId)).ToList();
        if (meterReadings.Count == 0)
        {
            throw new Exception("No data to export");
        }

        createExportFile(meterReadings, exportFileName, eneryType);
    }

    private string createExportFile(List<Models.MeterReading> meterReadings, string exportFileName, Models.EnergyType eneryType)
    {
        FileInfo file = new FileInfo(exportFileName);
        if (file.Exists) file.Delete();

        ExcelPackage.License.SetNonCommercialPersonal("Albert634515");
        using (ExcelPackage excelPackage = new ExcelPackage())
        {
            ExcelWorksheet hoja = excelPackage.Workbook.Worksheets.Add("MeterReadings");
            hoja.Cells[1, 1].Value = "Id"; ;
            hoja.Cells[1, 2].Value = "EnergyTypeName";
            hoja.Cells[1, 3].Value = "RegistrationDate";
            hoja.Cells[1, 4].Value = "RateNormal";
            hoja.Cells[1, 5].Value = "RateLow";
            hoja.Cells[1, 6].Value = "ReturnDeliveryLow";
            hoja.Cells[1, 7].Value = "ReturnDeliveryNormal";

            hoja.Cells[2, 1].LoadFromCollection(meterReadings);

            if (eneryType.HasNormalAndLow == false)
            {
                hoja.DeleteColumn(16);
                hoja.DeleteColumn(15);
                hoja.DeleteColumn(14);
                hoja.DeleteColumn(13);
                hoja.DeleteColumn(12);
                hoja.DeleteColumn(11);
                hoja.DeleteColumn(10);
                hoja.DeleteColumn(9);
                hoja.DeleteColumn(8);
                hoja.DeleteColumn(7);
                hoja.DeleteColumn(6);
                hoja.DeleteColumn(5);
            }
            else
            {
                hoja.DeleteColumn(16);
                hoja.DeleteColumn(15);
                hoja.DeleteColumn(14);
                hoja.DeleteColumn(13);
                hoja.DeleteColumn(12);
                hoja.DeleteColumn(11);
                hoja.DeleteColumn(10);
                hoja.DeleteColumn(9);
                hoja.DeleteColumn(8);
            }

            hoja.Cells[$"C2:C{meterReadings.Count + 1}"].Style.Numberformat.Format = "dd-MM-yyyy";
            hoja.Cells.AutoFitColumns();
            excelPackage.SaveAs(file);

            return exportFileName;
        }
    }


    public List<Models.MeterReading> ImportFromCsvFile(string fileName, Models.EnergyType energyType, Models.Meter meter)
        => ReadCsvFile(fileName, energyType, meter);

    public static List<Models.MeterReading> ReadCsvFile(string fileName, Models.EnergyType energyType, Models.Meter meter)
    {
        List<Models.MeterReading> meterReadings = new();
        Models.MeterReading meterReading;

        //set the formatting options
        ExcelTextFormat format = new();
        format.Delimiter = ';';
        format.Culture = new CultureInfo(Thread.CurrentThread.CurrentCulture.ToString());
        format.Culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
        format.Encoding = new UTF8Encoding();
        format.EOL = "\n";

        //read the CSV file from disk
        if (File.Exists(fileName))
        {
            ExcelPackage.License.SetNonCommercialPersonal("Albert634515");

            //create a new Excel package
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //create a WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                //load the CSV data into cell A1
                var csv = File.ReadAllText(fileName, Encoding.UTF8).Replace("\r\n", "\n").Replace('\r', '\n');
                worksheet.Cells["A1"].LoadFromText(csv, format);

                if (worksheet.Dimension == null)
                    return meterReadings;
                var firstNonEmptyRow = true;
                for (var rowNum = 1; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    var row = worksheet.Cells[string.Format("{0}:{0}", rowNum)];
                    // just an example, you want to know if all cells of this row are empty
                    bool allEmpty = row.All(c => string.IsNullOrWhiteSpace(c.Text));
                    if (allEmpty) continue;

                    // A header is optional; never discard the first meter reading.
                    var dateColumn = worksheet.Cells[rowNum, 1].Text.Trim();
                    var isHeader = firstNonEmptyRow &&
                        (dateColumn.Equals("Date", StringComparison.OrdinalIgnoreCase) ||
                         dateColumn.Equals("RegistrationDate", StringComparison.OrdinalIgnoreCase) ||
                         dateColumn.Equals("OpnameDatum", StringComparison.OrdinalIgnoreCase) ||
                         dateColumn.Equals("Datum", StringComparison.OrdinalIgnoreCase));
                    firstNonEmptyRow = false;
                    if (isHeader) continue;

                    try
                    {
                        if (string.IsNullOrWhiteSpace(worksheet.Cells[rowNum, 1].Text) ||
                            string.IsNullOrWhiteSpace(worksheet.Cells[rowNum, 2].Text))
                            throw new FormatException("Date and normal reading are required.");
                        meterReading = new Import.CsvMeterReading { SourceRow = rowNum };
                        meterReading.Id = 0;
                        meterReading.EnergyTypeId = energyType.Id;
                        meterReading.RegistrationDate = Convert.ToDateTime(worksheet.Cells[rowNum, 1].Value);
                        meterReading.RateNormal = Convert.ToDecimal(worksheet.Cells[rowNum, 2].Value);
                        meterReading.RateLow = Convert.ToDecimal(worksheet.Cells[rowNum, 3].Value);
                        meterReading.ReturnDeliveryNormal = Convert.ToDecimal(worksheet.Cells[rowNum, 4].Value);
                        meterReading.ReturnDeliveryLow = Convert.ToDecimal(worksheet.Cells[rowNum, 5].Value);

                        if (meter != null)
                            meterReading.MeterId = meter.Id;

                        meterReadings.Add(meterReading);
                    }
                    catch (Exception ex) when (ex is FormatException or InvalidCastException or OverflowException)
                    {
                        throw new InvalidDataException($"Invalid CSV data on source row {rowNum}: {ex.Message}", ex);
                    }
                }
            }
        }

        return meterReadings;
    }
}
