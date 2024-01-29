using System.Globalization;
using System.Text;
using EnergyUse.Core.Context;
using OfficeOpenXml;

namespace EnergyUse.Core.Manager
{
    public class LibEpplus
    {

        private readonly EnergyUseContext _context;

        public LibEpplus(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);
        }

        public void ExportMeterReadings(string exportFileName, Models.EnergyType eneryType, long addressId)
        {
            var meterReadingRepo = new EnergyUse.Core.Repositories.RepoMeterReading(_context);
            List<EnergyUse.Models.MeterReading> meterReadings = meterReadingRepo.SelectByEnergyIdAndAddressId(eneryType.Id, addressId).ToList();
            if (meterReadings.Count == 0)
            {
                throw new Exception("No data to export");
            }

            createExportFile(meterReadings, exportFileName, eneryType);
        }

        public void ExportMeterReadings(string exportFileName, Models.EnergyType eneryType, long addressId, DateTime startRange, DateTime endRange)
        {
            var meterReadingRepo = new Repositories.RepoMeterReading(_context);
            List<Models.MeterReading> meterReadings = meterReadingRepo.SelectByRange(startRange, endRange, eneryType.Id, addressId).ToList();
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

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
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
        {
            List<Models.MeterReading> meterReadings = new();
            Models.MeterReading meterReading;

            //set the formatting options
            ExcelTextFormat format = new();
            format.Delimiter = ';';
            format.Culture = new CultureInfo(Thread.CurrentThread.CurrentCulture.ToString());
            format.Culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            format.Encoding = new UTF8Encoding();

            //read the CSV file from disk
            if (File.Exists(fileName))
            {
                FileInfo file = new(fileName);

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                //create a new Excel package
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    //create a WorkSheet
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                    //load the CSV data into cell A1
                    worksheet.Cells["A1"].LoadFromText(file, format);

                    for (var rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                    {
                        var row = worksheet.Cells[string.Format("{0}:{0}", rowNum)];
                        // just an example, you want to know if all cells of this row are empty
                        bool allEmpty = row.All(c => string.IsNullOrWhiteSpace(c.Text));
                        if (allEmpty) break;

                        meterReading = new Models.MeterReading();
                        meterReading.Id = 0;
                        meterReading.EnergyType = energyType;
                        meterReading.RegistrationDate = Convert.ToDateTime(worksheet.Cells[rowNum, 1].Value);
                        meterReading.RateNormal = Convert.ToDecimal(worksheet.Cells[rowNum, 2].Value);
                        meterReading.RateLow = Convert.ToDecimal(worksheet.Cells[rowNum, 3].Value);
                        meterReading.ReturnDeliveryNormal = Convert.ToDecimal(worksheet.Cells[rowNum, 4].Value);
                        meterReading.ReturnDeliveryLow = Convert.ToDecimal(worksheet.Cells[rowNum, 5].Value);

                        if (meter != null)
                        {
                            meterReading.Meter = new Models.Meter();
                            meterReading.Meter.Id = meter.Id;
                        }
                        meterReadings.Add(meterReading);
                    }
                }
            }

            return meterReadings;
        }
    }
}