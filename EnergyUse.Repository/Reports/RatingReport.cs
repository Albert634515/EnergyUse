using EnergyUse.Core.Context;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace EnergyUse.Core.Reports
{
    public class RatingReport : ReportBase
    {
        #region Properties

        private readonly EnergyUseContext _context;
        private readonly string _dbFileName;
        private static UnitOfWork.RatingReport _unitOfWork;

        private static float[] _pointColumnWidths = { 115F, 115F, 60F, 75F, 75F };

        #endregion

        public RatingReport(string dbFileName)
        {
            _dbFileName = dbFileName;
            _context = new EnergyUseContext(dbFileName);
            _unitOfWork = new UnitOfWork.RatingReport(_dbFileName);
        }
        public string GetRatingReportPdf(Models.Address address, ParameterSelection parameterSelection)
        {
            Table table;
            DateTime startRange, endRange;
            Models.EnergyType energyType;

            var dest = System.IO.Path.GetTempPath();
            var fileName = $"RatingReport_{DateTime.Now:yyyyMMddHHmmss}.pdf";

            PdfWriter writer = new PdfWriter(System.IO.Path.Combine(dest, fileName));
            PdfDocument pdf = new PdfDocument(writer);
            pdf.SetDefaultPageSize(PageSize.A4);
            Document document = new Document(pdf);
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
                                
                var costCategoryList = _unitOfWork.CostCategoriesRepo.SelectByEnergyTypeAndRange(energyType.Id, startRange, endRange);
                foreach (Models.CostCategory costCategory in costCategoryList)
                {
                    long tarifGroupId = 0;
                    table = new Table(_pointColumnWidths);
                    table.SetKeepTogether(true);

                    if (costCategory.TariffGroupId.HasValue)
                        tarifGroupId = costCategory.TariffGroupId.Value;
                    if (tarifGroupId == 0)
                        tarifGroupId = item.TarifGroup;

                    GetRateTableHeader(table, costCategory, tarifGroupId);

                    var rates = _unitOfWork.RateRepo.SelectByCostCategoryAndEnergyTypeAndTarifGroup(costCategory.Id, energyType.Id, tarifGroupId);
                    foreach (Models.Rate rate in rates) 
                    {                        
                        GetRateTable(table, rate);
                    }

                    document.Add(table);
                    document.Add(new Paragraph(""));
                }                
            }

            document.Close();

            return System.IO.Path.Combine(dest, fileName);
        }

        private void GetRateTable(Table table, Rate rate)
        {
            table.AddHeaderCell(GetNormalText(rate.StartRate.ToString("dd-MM-yyyy"), 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddHeaderCell(GetNormalText(rate.EndRate.ToString("dd-MM-yyyy"), 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddHeaderCell(GetNormalText(rate.RateValue.ToString()));
            table.AddHeaderCell(GetNormalText(rate.PriceChange.ToString()));
            table.AddHeaderCell(GetNormalText(rate.ExpectedPriceChange.ToString()));
        }

        private static Paragraph GetHeaderParagraph(SelectedEnergyType item, Models.Address address)
        {
            var headerText = $"Rates for period: {item.StartRange:dd-MM-yyyy} - {item.EndRange:dd-MM-yyyy}";

            return new Paragraph(headerText);
        }

        private void GetRateTableHeader(Table table, Models.CostCategory costCategory, long tariffGroupId)
        {
            var tarifGroup = _unitOfWork.TariffGroupRepo.Get(tariffGroupId);
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
}