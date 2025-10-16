using EnergyUse.Models.Common;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace EnergyUse.Core.Reports;

public class SettlementSplitByType : SettlementBase
{
    public SettlementSplitByType(string dbFileName) : base(dbFileName)
    {

    }

    public async Task<string> GetSettlementPdfAsync(ParameterSelection parameterSelection)
    {
        Table table;
        DateTime startRange, endRange;
        Models.EnergyType energyType;

        var dest = System.IO.Path.GetTempPath();
        var fileName = $"SettlementSplitByType_{DateTime.Now:yyyyMMddHHmmss}.pdf";
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

            _periodicDataList = await LibPeriodicDate.GetRangeAsync(parameterPeriod);
            _settlementDataList = _unitOfWork.CostCategoriesRepo.MapCostCategories(_periodicDataList);
            if (parameterSelection.ShowRates == false)
                _settlementDataList = mergeSettlementData(_settlementDataList);

            if (_periodicDataList.Count == 0)
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

                var list1 = _settlementDataList.Where(w => w.CostCategory.EnergySubTypeId < 3 || w.CostCategory.EnergySubTypeId > 7).ToList();
                table = getCostTable(item, list1, parameterSelection.ShowRates, $"Sub total {item.EnergyType.Name}");
                document.Add(table);
                document.Add(new Paragraph(""));

                var list2 = _settlementDataList.Where(w => !(w.CostCategory.EnergySubTypeId < 3) && (w.CostCategory.EnergySubTypeId >=3 || w.CostCategory.EnergySubTypeId <= 7) && w.CostCategory.EnergySubTypeId != 5).ToList();
                table = getCostTable(item, list2, parameterSelection.ShowRates, $"Sub total {item.EnergyType.Name} return");
                document.Add(table);
                document.Add(new Paragraph(""));

                var list3 = _settlementDataList.Where(w => w.CostCategory.EnergySubTypeId == 5).ToList();
                table = getCostTable(item, list3, parameterSelection.ShowRates, $"Sub total {item.EnergyType.Name} cost");
                document.Add(table);

                document.Add(new Paragraph(""));

                setSettlementSubTotal(energyType, _settlementDataList);

                table = setTotalToTable(energyType, parameterSelection.ShowRates);
                document.Add(table);
            }
        } // End of loop of selected energy types

        document.Add(new Paragraph(""));
        table = getPricePerKw();
        document.Add(table);

        document.Add(new Paragraph(""));
        table = getPayments(address.Id, parameterSelection.PreSelectedPeriodId, parameterSelection.StartRange, parameterSelection.EndRange);
        document.Add(table);

        document.Close();

        return System.IO.Path.Combine(dest, fileName);
    }
}