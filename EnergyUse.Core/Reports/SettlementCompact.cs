using EnergyUse.Models.Common;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace EnergyUse.Core.Reports;

public class SettlementCompact : SettlementBase
{
    public SettlementCompact(string dbFileName) : base(dbFileName)
    {

    }

    public string GetSettlementPdf(ParameterSelection parameterSelection)
    {
        Table table;
        DateTime startRange, endRange;
        Models.EnergyType energyType;

        var dest = System.IO.Path.GetTempPath();
        var fileName = $"SettlementCompact_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        Models.Address address = _unitOfWork.AddressRepo.Get(parameterSelection.AddressId);
        PdfWriter writer = new(System.IO.Path.Combine(dest, fileName));
        PdfDocument pdf = new(writer);
        pdf.SetDefaultPageSize(PageSize.A4);
        Document document = new(pdf);

        var isFirstPage = true;
        var LibPeriodicDate = new Manager.LibPeriodicDate(_dbFileName);
        _settlementSubTotalList = new List<SettlementSubTotal>();

        foreach (SelectedEnergyType item in parameterSelection.SelectedEnergyTypeList)
        {
            //Header 
            if (!isFirstPage)
                document.Add(new AreaBreak());
            document.Add(getHeaderParagraph(item, address));

            isFirstPage = false;
            energyType = item.EnergyType;
            startRange = item.StartRange;
            endRange = item.EndRange;

            ParameterPeriod parameterPeriod = new();
            parameterPeriod.EnergyType = energyType;
            parameterPeriod.AddressId = address.Id;
            parameterPeriod.StartRange = startRange;
            parameterPeriod.EndRange = endRange;
            parameterPeriod.ShowType = Common.Enums.ShowType.Value;
            parameterPeriod.PeriodType = Common.Enums.Period.SettlementDay;
            parameterPeriod.PredictMissingData = parameterSelection.PredictMissingData;
            parameterPeriod.TarifGroupId = item.TarifGroup;
            parameterPeriod.QuantityReduction = 1;

            List<PeriodicData> periodicData = LibPeriodicDate.GetRange(parameterPeriod);
            List<SettlementData> settlementDataList = _unitOfWork.CostCategoriesRepo.MapCostCategories(periodicData);
            if (parameterSelection.ShowRates == false)
                settlementDataList = mergeSettlementData(settlementDataList);

            if (periodicData.Count == 0)
            {
                document.Add(new Paragraph($"No data found for energy type {energyType.Name}"));
                document.Add(new Paragraph("\n"));
            }
            else
            {
                table = new Table(_pointColumnWidths);
                GetSectionHeader(table, getSectionHeaderText(item, address));
                document.Add(table);
                document.Add(new Paragraph(""));

                table = getCostTable(item, settlementDataList, parameterSelection.ShowRates);
                document.Add(table);

                document.Add(new Paragraph(""));

                setSettlementSubTotal(energyType, settlementDataList);

                table = setTotalToTable(energyType, parameterSelection.ShowRates);
                document.Add(table);
            }
        } // End of loop of selected energy types     
       
        document.Add(new Paragraph(""));

        table = getPayments(address.Id, parameterSelection.PreSelectedPeriodId, parameterSelection.StartRange, parameterSelection.EndRange);
        document.Add(table);

        document.Close();

        return System.IO.Path.Combine(dest, fileName);
    }     
}