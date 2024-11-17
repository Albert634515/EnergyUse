using EnergyUse.Models.Common;
using iText.Kernel.Font;
using iText.Layout.Element;

namespace EnergyUse.Core.Reports;

public class SettlementBase : ReportBase
{
    #region Properties

    //private readonly EnergyUseContext _context;
    internal readonly string _dbFileName;
    internal static UnitOfWork.Settlement _unitOfWork;

    internal static float[] _pointColumnWidths = [250F, 115F, 115F, 60F, 75F, 110F, 90F, 110F];
    internal static List<SettlementSubTotal> _settlementSubTotalList = new();
    internal static List<FooterText> _footerTextsList = new();

    #endregion

    public SettlementBase(string dbFileName)
    {
        _dbFileName = dbFileName;
        _unitOfWork = new UnitOfWork.Settlement(_dbFileName);
    }

    internal List<SettlementData> mergeSettlementData(List<SettlementData> settlementDataList)
    {
        var mergeSettlementData = new List<SettlementData>();
        foreach (SettlementData settlementData in settlementDataList)
        {
            var mergeSettlementDataItem = mergeSettlementData.LastOrDefault(x => x.CostCategory.Id == settlementData.CostCategory.Id);
            if (mergeSettlementDataItem is null)
            {
                mergeSettlementDataItem = new SettlementData();
                mergeSettlementDataItem.CostCategory = settlementData.CostCategory;
                mergeSettlementDataItem.Description = settlementData.Description;
                mergeSettlementDataItem.ValueBaseConsumed = settlementData.ValueBaseConsumed;
                mergeSettlementDataItem.ValueBaseProduced = settlementData.ValueBaseProduced;
                mergeSettlementDataItem.VatAmount = settlementData.VatAmount;
                mergeSettlementDataItem.StartDate = settlementData.StartDate;
                mergeSettlementDataItem.EndDate = settlementData.EndDate;

                mergeSettlementData.Add(mergeSettlementDataItem);
            }
            else
            {
                mergeSettlementDataItem.ValueBaseConsumed += settlementData.ValueBaseConsumed;
                mergeSettlementDataItem.ValueBaseProduced += settlementData.ValueBaseProduced;
                mergeSettlementDataItem.Value += settlementData.Value;
                mergeSettlementDataItem.EndDate = settlementData.EndDate;
            }
        }
        return mergeSettlementData;
    }

    internal Table getCostTable(SelectedEnergyType item, List<SettlementData> settlementDataList, bool showRates)
    {
        _footerTextsList = new List<FooterText>();
        var table = new Table(_pointColumnWidths);

        getCategoryTableHeader(table, showRates);
        foreach (SettlementData settlementData in settlementDataList)
        {
            FooterText footerTextCorrection = new();
            string footerText = string.Empty;

            if (settlementData.CorrectionFactor > 0)
            {
                footerTextCorrection = getFooterTextCorrection(settlementData.CorrectionFactor);
                footerText = footerTextCorrection.Counter.ToString();
            }

            if (settlementData.LastAvailableRateUsed && settlementData.PriceIncrease != 0)
            {
                if (!string.IsNullOrWhiteSpace(footerText))
                    footerText = $"{footerText} ";

                footerTextCorrection = getFooterTextLastAvailableRateUsed(settlementData.PriceIncrease);
                footerText = $"{footerText}{footerTextCorrection.Counter}";
            }

            if (settlementData.DataPredicted)
            {
                if (!string.IsNullOrWhiteSpace(footerText))
                    footerText = $"{footerText} ";

                footerTextCorrection = addFooterText("Data is predicted");
                footerText = $"{footerText}{footerTextCorrection.Counter}";
            }

            if (settlementData.CostCategory.CalculateVat && settlementData.VatTarif == 0)
            {
                if (!string.IsNullOrWhiteSpace(footerText))
                    footerText = $"{footerText} ";

                footerTextCorrection = addFooterText("Vat percentage missing");
                footerText = $"{footerText}{footerTextCorrection.Counter}";
            }
            else if (settlementData.CostCategory.CalculateVat && settlementData.VatTarif > 0)
            {
                if (!string.IsNullOrWhiteSpace(footerText))
                    footerText = $"{footerText} ";

                footerTextCorrection = addFooterText($"Vat percentage {settlementData.VatTarif}%");
                footerText = $"{footerText}{footerTextCorrection.Counter}";
            }

            if (settlementData.LastAvailableVatRateUsed)
            {
                if (!string.IsNullOrWhiteSpace(footerText))
                    footerText = $"{footerText} ";

                footerTextCorrection = addFooterText($"Last available vat tarif used {settlementData.VatTarif}%");
                footerText = $"{footerText}{footerTextCorrection.Counter}";
            }

            table.AddCell(GetNormalText(settlementData.Description, 1, showRates ? 1 : 2, iText.Layout.Properties.TextAlignment.LEFT, footerText));
            table.AddCell(GetNormalText(settlementData.StartDate.ToString("dd-MM-yyyy"), 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddCell(GetNormalText(settlementData.EndDate.ToString("dd-MM-yyyy"), 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddCell(GetNormalText(Math.Round(settlementData.ValueBase, 2).ToString()));

            if (showRates)
                table.AddCell(GetNormalText(settlementData.Rate.ToString("##0.00000")));
            table.AddCell(GetNormalText(Math.Round(settlementData.Value, 2).ToString("##0.00")));

            if (!settlementData.CostCategory.CalculateVat)
                table.AddCell(GetNormalText(""));
            else
                table.AddCell(GetNormalText(Math.Round(settlementData.VatAmount, 2).ToString("##0.00")));

            table.AddCell(GetNormalText(Math.Round(settlementData.Value + settlementData.VatAmount, 2).ToString("##0.00")));
        }

        // Add Footer to table
        if (_footerTextsList.Count > 0)
            table.AddFooterCell(getFooterText(_footerTextsList, 1, 8));

        // Add sub Footer to table
        var settlementSubTotal = new SettlementSubTotal();
        settlementSubTotal.EngergyTypeId = item.EnergyType.Id;
        settlementSubTotal.ValueBase = settlementDataList.Sum(s => s.ValueBase);
        settlementSubTotal.TotalValue = settlementDataList.Sum(s => s.Value);
        settlementSubTotal.TotalVat = settlementDataList.Sum(s => s.Value * (s.VatTarif / 100));

        setSettlementSubTotal(table, settlementSubTotal, $"Sub total {item.EnergyType.Name}", showRates);

        return table;
    }

    internal void getCategoryTableHeader(Table table, bool showRates)
    {
        table.AddHeaderCell(GetBoldText("Description", 1, showRates ? 1 : 2, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("From", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Till", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Unit"));

        if (showRates)
            table.AddHeaderCell(GetBoldText("Rate"));

        table.AddHeaderCell(GetBoldText("Total ex.vat"));
        table.AddHeaderCell(GetBoldText("Vat"));
        table.AddHeaderCell(GetBoldText("Total"));
    }

    /// <summary>
    /// Set footer to table with seltlement datas
    /// </summary>
    /// <param name="table"></param>
    /// <param name="settlementSubTotal"></param>
    /// <param name="rowDescription"></param>
    private void setSettlementSubTotal(Table table, SettlementSubTotal settlementSubTotal, string rowDescription, bool showRates)
    {
        table.AddFooterCell(GetBoldTextGrey(rowDescription, 1, showRates ? 5 : 5, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddFooterCell(GetBoldTextGrey(Math.Round(settlementSubTotal.TotalValue, 2).ToString()));
        table.AddFooterCell(GetBoldTextGrey(Math.Round(settlementSubTotal.TotalVat, 2).ToString()));
        table.AddFooterCell(GetBoldTextGrey(Math.Round(settlementSubTotal.TotalValue + settlementSubTotal.TotalVat, 2).ToString()));
    }

    internal void setSettlementSubTotal(Models.EnergyType energyType, List<SettlementData> settlementDataList)
    {
        foreach (var settlementData in settlementDataList)
        {
            var subTotalKey = $"{Manager.LibEnergySubType.GetCombinedType(settlementData.CostCategory.EnergySubType.Id)}{energyType.Id}";
            SettlementSubTotal? settlementSubTotal = _settlementSubTotalList.FirstOrDefault(x => x.SubTotalType == subTotalKey && x.EngergyTypeId == energyType.Id);
            if (settlementSubTotal == null)
            {
                settlementSubTotal = new SettlementSubTotal();
                settlementSubTotal.EngergyTypeId = energyType.Id;
                settlementSubTotal.SubTotalType = subTotalKey;
                settlementSubTotal.Description = getSubTotalName(settlementData.CostCategory, energyType);

                _settlementSubTotalList.Add(settlementSubTotal);
            }

            settlementSubTotal.ValueBase += settlementData.ValueBase;
            settlementSubTotal.TotalValue += settlementData.Value;
            settlementSubTotal.TotalVat += settlementData.VatAmount;
        }
    }

    internal Table setTotalToTable(Models.EnergyType energyType, bool showRates)
    {
        Table table = new(_pointColumnWidths);
        table.SetKeepTogether(true);
        GetSectionHeader(table, "Totals");

        table.AddHeaderCell(GetBoldText("", 1, showRates ? 5 : 5));
        table.AddHeaderCell(GetBoldText("Money ex."));
        table.AddHeaderCell(GetBoldText("Vat"));
        table.AddHeaderCell(GetBoldText("Money inc."));

        var settlementSubTotalList = _settlementSubTotalList.Where(x => x.EngergyTypeId == energyType.Id).ToList();
        foreach (SettlementSubTotal settlementSubTotal in settlementSubTotalList)
        {
            table.AddCell(GetNormalText(settlementSubTotal.Description, 1, showRates ? 5 : 5, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddCell(GetNormalText(Math.Round(settlementSubTotal.TotalValue, 2).ToString("##0.00")));
            table.AddCell(GetNormalText(Math.Round(settlementSubTotal.TotalVat, 2).ToString("##0.00")));
            table.AddCell(GetNormalText(Math.Round(settlementSubTotal.TotalValue + settlementSubTotal.TotalVat, 2).ToString("##0.00")));
        }

        table.AddFooterCell(GetBoldTextGrey("Total", 1, showRates ? 5 : 5, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddFooterCell(GetBoldTextGrey(Math.Round(_settlementSubTotalList.Sum(x => x.TotalValue), 2).ToString("##0.00")));
        table.AddFooterCell(GetBoldTextGrey(Math.Round(_settlementSubTotalList.Sum(x => x.TotalVat), 2).ToString("##0.00")));
        table.AddFooterCell(GetBoldTextGrey(Math.Round(_settlementSubTotalList.Sum(x => x.TotalValue) + _settlementSubTotalList.Sum(x => x.TotalVat), 2).ToString("##0.00")));

        table.AddFooterCell(GetBoldText("Per Month", 1, showRates ? 5 : 5, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddFooterCell(GetBoldText(Math.Round(_settlementSubTotalList.Sum(x => x.TotalValue) / 12, 2).ToString("##0.00")));
        table.AddFooterCell(GetBoldText(Math.Round(_settlementSubTotalList.Sum(x => x.TotalVat) / 12, 2).ToString("##0.00")));
        table.AddFooterCell(GetBoldText(Math.Round((_settlementSubTotalList.Sum(x => x.TotalValue) + _settlementSubTotalList.Sum(x => x.TotalVat)) / 12, 2).ToString("##0.00")));

        return table;
    }

    #region Footer

    private Cell getFooterText(List<FooterText> footerTextList, int rowspan = 1, int colspan = 1)
    {
        PdfFont normal = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
        Paragraph paragraph = new();

        foreach (FooterText footerText in footerTextList)
        {
            _ = paragraph.Add(new Text(footerText.Counter.ToString()).SetFont(normal).SetTextRise(7).SetFontSize(6));
            _ = paragraph.Add(new Text($"{footerText.Text}{Environment.NewLine}").SetFont(normal));
        }

        Cell cell = new(rowspan, colspan);
        cell.SetFontSize(9);
        cell.Add(paragraph);

        return cell;
    }

    private FooterText getFooterTextLastAvailableRateUsed(decimal priceIncrease)
    {
        string text = $"Last available rate used";
        decimal percChange = (priceIncrease - 1);
        if ((priceIncrease - 1) > 0)
            text += $", increased with {percChange:#0.00%}";
        else if ((priceIncrease - 1) < 0)
            text += $", decreased with {percChange:#0.00%}";

        return addFooterText(text);
    }

    private FooterText getFooterTextCorrection(decimal correctionFactor)
    {
        string text = $"Consumption is calculated with a correction factor of {correctionFactor}";

        return addFooterText(text);
    }

    private FooterText addFooterText(string newText)
    {
        FooterText? footerText = _footerTextsList.FirstOrDefault(x => x.Text == newText);
        if (footerText == null)
        {
            footerText = new FooterText
            {
                Text = newText,
                Counter = _footerTextsList.Count + 1
            };

            _footerTextsList.Add(footerText);
        }

        return footerText;
    }

    #endregion

    internal string getSubTotalName(Models.CostCategory costCategory, Models.EnergyType energyType)
    {
        string subTotalName;

        switch (costCategory.EnergySubType.Id)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                subTotalName = $"Energy {energyType.Name}";
                break;
            case 5:
                subTotalName = $"Other {energyType.Name}";
                break;
            default:
                subTotalName = $"{costCategory.Name} {energyType.Name}";
                break;
        }

        return subTotalName;
    }

    internal static Paragraph getHeaderParagraph(SelectedEnergyType item, Models.Address address)
    {
        var headerText = $"Settlement period: {item.StartRange:dd-MM-yyyy} - {item.EndRange:dd-MM-yyyy}";

        return new Paragraph(headerText);
    }

    internal string getSectionHeaderText(SelectedEnergyType item, Models.Address address)
    {
        var headerText = string.Empty;

        headerText += $"{item.EnergyType.Name}";
        headerText += $", range: {Environment.NewLine}";

        headerText += $" Normal: {getMeterPositionRange(item, address.Id, Common.Enums.SubEnergyType.Normal)}";
        if (item.EnergyType.HasNormalAndLow)
        {
            headerText += $", low: {getMeterPositionRange(item, address.Id, Common.Enums.SubEnergyType.Low)}";
        }

        if (item.EnergyType.HasEnergyReturn)
        {
            headerText += $"{Environment.NewLine}";
            headerText += $" Return normal: {getMeterPositionRange(item, address.Id, Common.Enums.SubEnergyType.ReturnNormal)}";

            if (item.EnergyType.HasNormalAndLow)
            {
                headerText += $", return low: {getMeterPositionRange(item, address.Id, Common.Enums.SubEnergyType.ReturnLow)}";
            }
        }

        return headerText;
    }

    private static string getMeterPositionRange(SelectedEnergyType item, long addressId, Common.Enums.SubEnergyType subEnergyType)
    {
        var positionRange = string.Empty;

        decimal position = getMeterPosition(item.StartRange, item.EnergyType.Id, addressId, subEnergyType);
        positionRange += $"{(position < 0 ? $"?" : $"{position}")}";

        position = getMeterPosition(item.EndRange, item.EnergyType.Id, addressId, subEnergyType);
        positionRange += $" - {(position < 0 ? $"?" : $"{position}")}";

        return positionRange;
    }

    private static decimal getMeterPosition(DateTime registrationDate, long energyTypeId, long addressId, Common.Enums.SubEnergyType subEnergyType)
    {
        decimal position = -1;

        var meterReading = _unitOfWork.MeterReadingRepo.SelectRow(registrationDate, energyTypeId, addressId);
        if (meterReading != null)
        {
            position = subEnergyType switch
            {
                Common.Enums.SubEnergyType.Normal => meterReading.RateNormal,
                Common.Enums.SubEnergyType.Low => meterReading.RateLow,
                Common.Enums.SubEnergyType.ReturnNormal => meterReading.ReturnDeliveryNormal,
                Common.Enums.SubEnergyType.ReturnLow => meterReading.ReturnDeliveryLow,
                Common.Enums.SubEnergyType.Other => -1,
                _ => -1,
            };
        }

        return position;
    }

    internal Table getPayments(long addressId, long periodId, DateTime startRange, DateTime endRange)
    {
        Table table = new(_pointColumnWidths);
        table.SetKeepTogether(true);
        GetSectionHeader(table, "Payments");

        table.AddHeaderCell(GetBoldText("Pay date", 1, 2, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Description", 1, 5, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Amount", 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));

        List<Models.Payment> payments = new();

        if (periodId > 0)
            payments = _unitOfWork.PaymentRepo.SelectByAddressAndPeriod(addressId, periodId).ToList();
        else
            payments = _unitOfWork.PaymentRepo.SelectByAddressAndRange(addressId, startRange, endRange).ToList();

        if (payments.Count == 0)
        {
            table.AddCell(GetNormalText("No payments", 1, 8, iText.Layout.Properties.TextAlignment.LEFT));
        }
        else
        {
            var totalToPay = Math.Round(_settlementSubTotalList.Sum(x => x.TotalValue) + _settlementSubTotalList.Sum(x => x.TotalVat));
            var total = (decimal)payments.Sum(s => s.Amount);
            var toBePaid = totalToPay - total;
            var monthsLeft = 12 - payments.Count;

            foreach (var payment in payments)
            {
                table.AddCell(GetNormalText(payment.PayDate.ToString("dd-MM-yyyy"), 1, 2, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddCell(GetNormalText($"{payment.Description}", 1, 5, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddCell(GetNormalText(payment.Amount.ToString("##0.00"), 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));
            }

            // Add Footer                
            table.AddFooterCell(GetBoldTextGrey("Total paid", 1, 7, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddFooterCell(GetBoldTextGrey(Math.Round(total, 2).ToString("#0.00"), 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));

            if (totalToPay != 0)
            {
                table.AddFooterCell(GetBoldText("To be paid", 1, 7, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddFooterCell(GetBoldText(Math.Round(toBePaid, 2).ToString("#0.00"), 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));
            }

            if (monthsLeft > 0)
            {
                table.AddFooterCell(GetBoldText($"Avg per month left, ({toBePaid}/{monthsLeft})", 1, 7, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddFooterCell(GetBoldText(Math.Round(toBePaid / monthsLeft, 2).ToString("#0.00"), 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));
            }
        }

        return table;
    }
}