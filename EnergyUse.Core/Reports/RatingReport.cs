using EnergyUse.Models;
using EnergyUse.Models.Common;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace EnergyUse.Core.Reports;

public class RatingReport : ReportBase
{
    #region Properties

    private readonly string _dbFileName;
    private readonly UnitOfWork.RatingReport _unitOfWork;

    private static float[] _pointColumnWidths = { 115F, 115F, 60F, 75F, 75F };

    #endregion

    public RatingReport(string dbFileName)
    {
        _dbFileName = dbFileName;
        _unitOfWork = new UnitOfWork.RatingReport(_dbFileName);
    }
    public async Task<string> GetRatingReportPdf(Models.Address address, ParameterSelection parameterSelection)
    {
        Table table;
        DateTime startRange, endRange;
        Models.EnergyType energyType;

        var dest = System.IO.Path.GetTempPath();
        var fileName = $"RatingReport_{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}.pdf";

        using PdfWriter writer = new PdfWriter(System.IO.Path.Combine(dest, fileName));
        using PdfDocument pdf = new PdfDocument(writer);
        pdf.SetDefaultPageSize(PageSize.A4);
        using Document document = new Document(pdf);
        var isFirstPage = true;
        foreach (SelectedEnergyType item in parameterSelection.SelectedEnergyTypeList)
        {
            //Header 
            if (!isFirstPage)
                document.Add(new AreaBreak());
            document.Add(GetHeaderParagraph(item, address));

            isFirstPage = false;
            energyType = item.EnergyType;
            startRange = item.StartRange;
            endRange = item.EndRange;
                            
            var costCategoryList = await _unitOfWork.CostCategoriesRepo.SelectByEnergyTypeAndRange(energyType.Id, startRange, endRange);
            foreach (Models.CostCategory costCategory in costCategoryList)
            {
                long tarifGroupId = 0;
                table = new Table(_pointColumnWidths);
                table.SetKeepTogether(true);

                if (costCategory.TariffGroupId.HasValue)
                    tarifGroupId = costCategory.TariffGroupId.Value;
                if (tarifGroupId == 0)
                    tarifGroupId = item.TarifGroup;

                await GetRateTableHeader(table, costCategory, tarifGroupId);

                var rates = await _unitOfWork.RateRepo.SelectByCostCategoryAndEnergyTypeAndTarifGroup(costCategory.Id, energyType.Id, tarifGroupId);
                foreach (Models.Rate rate in rates) 
                {                        
                    GetRateTable(table, rate);
                }

                document.Add(table);
                document.Add(new Paragraph(""));
            }                
        }

        return System.IO.Path.Combine(dest, fileName);
    }

    private void GetRateTable(Table table, Rate rate)
    {
        table.AddCell(GetNormalText(rate.StartRate.ToString("dd-MM-yyyy"), 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddCell(GetNormalText(rate.EndRate.ToString("dd-MM-yyyy"), 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddCell(GetNormalText(rate.RateValue.ToString()));
        table.AddCell(GetNormalText(rate.PriceChange.ToString()));
        table.AddCell(GetNormalText(rate.ExpectedPriceChange.ToString()));
    }

    private static Paragraph GetHeaderParagraph(SelectedEnergyType item, Models.Address address)
    {
        var headerText = $"Rates for period: {item.StartRange:dd-MM-yyyy} - {item.EndRange:dd-MM-yyyy}";

        return new Paragraph(headerText);
    }

    private async Task GetRateTableHeader(Table table, Models.CostCategory costCategory, long tariffGroupId)
    {
        var tarifGroup = await _unitOfWork.TariffGroupRepo.Get(tariffGroupId);
        if (tarifGroup is null)
            throw new InvalidOperationException($"Tariff group with ID {tariffGroupId} was not found.");

        var range = "";
        if (costCategory.Start.HasValue)
            range += $" from {costCategory.Start.Value.ToString("dd-MM-yyyy")}";
        if (costCategory.End.HasValue)
            range += $"-{costCategory.End.Value.ToString("dd-MM-yyyy")}";

        table.AddHeaderCell(GetBoldTextGrey($"{costCategory.Name}{range}, tariff group: {tarifGroup.Description}", 1, 5, iText.Layout.Properties.TextAlignment.LEFT));

        table.AddHeaderCell(GetBoldText("From", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Till", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Rate"));
        table.AddHeaderCell(GetBoldText("Price change"));
        table.AddHeaderCell(GetBoldText("Expected change"));
    }
}
