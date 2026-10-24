using EnergyUse.Models.Common;
using iText.Kernel.Font;
using iText.Layout.Element;

namespace EnergyUse.Core.Reports;

public class SettlementBase : ReportBase
{
    internal sealed record MeterPeriod(SelectedEnergyType Selection, Models.Meter Meter);

    #region Properties

    //private readonly EnergyUseContext _context;
    internal readonly string _dbFileName;
    internal readonly UnitOfWork.Settlement _unitOfWork;

    internal static readonly float[] _pointColumnWidths = [220F, 130F, 130F, 60F, 75F, 110F, 90F, 110F];
    internal static readonly float[] _costColumnWidthsWithoutRates = [295F, 130F, 130F, 60F, 110F, 90F, 110F];
    internal List<SettlementData> _settlementDataList = new();
    internal List<SettlementSubTotal> _settlementSubTotalList = new();
    internal List<PeriodicData> _periodicDataList = new();
    internal List<FooterText> _footerTextsList = new();

    #endregion

    public SettlementBase(string dbFileName)
    {
        _dbFileName = dbFileName;
        _unitOfWork = new UnitOfWork.Settlement(_dbFileName);
    }

    internal static ParameterPeriod getCalculationPeriod(SelectedEnergyType item, long addressId, bool predictMissingData)
    {
        if (item.EndRange.Date <= item.StartRange.Date)
            throw new ArgumentException("The settlement end reading must be after the start reading.");

        // Daily deltas belong to the interval from this reading to the next one.
        // The end reading closes the period; its delta belongs to the next period.
        return new ParameterPeriod
        {
            EnergyType = item.EnergyType,
            AddressId = addressId,
            StartRange = item.StartRange.Date,
            EndRange = item.EndRange.Date.AddDays(-1),
            ShowType = Common.Enums.ShowType.Value,
            PeriodType = Common.Enums.Period.SettlementDay,
            PredictMissingData = predictMissingData,
            TarifGroupId = item.TarifGroup,
            QuantityReduction = 1
        };
    }

    internal List<SettlementData> mergeSettlementData(List<SettlementData> settlementDataList)
    {
        var mergeSettlementData = new List<SettlementData>();
        foreach (SettlementData settlementData in settlementDataList)
        {
            var mergeSettlementDataItem = mergeSettlementData.LastOrDefault(x => x.CostCategory.Id == settlementData.CostCategory.Id);
            if (mergeSettlementDataItem is null)
            {
                mergeSettlementDataItem = new SettlementData
                {
                    CostCategory = settlementData.CostCategory,
                    Description = settlementData.Description,
                    ValueBaseConsumed = settlementData.ValueBaseConsumed,
                    ValueBaseProduced = settlementData.ValueBaseProduced,
                    CorrectionFactor = settlementData.CorrectionFactor,
                    Rate = settlementData.Rate,
                    LastAvailableRateUsed = settlementData.LastAvailableRateUsed,
                    Value = settlementData.Value,
                    VatTarif = settlementData.VatTarif,
                    VatAmount = settlementData.VatAmount,
                    LastAvailableVatRateUsed = settlementData.LastAvailableVatRateUsed,
                    DataPredicted = settlementData.DataPredicted,
                    PriceAdjustmentFactor = settlementData.PriceAdjustmentFactor,
                    Staffel = settlementData.Staffel,
                    MaxStaffelRange = settlementData.MaxStaffelRange,
                    StartDate = settlementData.StartDate,
                    EndDate = settlementData.EndDate
                };

                mergeSettlementData.Add(mergeSettlementDataItem);
            }
            else
            {
                mergeSettlementDataItem.ValueBaseConsumed += settlementData.ValueBaseConsumed;
                mergeSettlementDataItem.ValueBaseProduced += settlementData.ValueBaseProduced;
                mergeSettlementDataItem.Value += settlementData.Value;
                mergeSettlementDataItem.VatAmount += settlementData.VatAmount;
                mergeSettlementDataItem.LastAvailableRateUsed |= settlementData.LastAvailableRateUsed;
                mergeSettlementDataItem.LastAvailableVatRateUsed |= settlementData.LastAvailableVatRateUsed;
                mergeSettlementDataItem.DataPredicted |= settlementData.DataPredicted;
                if (mergeSettlementDataItem.PriceAdjustmentFactor == 0 && settlementData.PriceAdjustmentFactor != 0)
                    mergeSettlementDataItem.PriceAdjustmentFactor = settlementData.PriceAdjustmentFactor;
                mergeSettlementDataItem.StartDate = mergeSettlementDataItem.StartDate < settlementData.StartDate
                    ? mergeSettlementDataItem.StartDate
                    : settlementData.StartDate;
                mergeSettlementDataItem.EndDate = mergeSettlementDataItem.EndDate > settlementData.EndDate
                    ? mergeSettlementDataItem.EndDate
                    : settlementData.EndDate;
            }
        }
        return mergeSettlementData;
    }

    internal Table getCostTable(SelectedEnergyType item, List<SettlementData> settlementDataList, bool showRates, string subTotalText)
    {
        _footerTextsList = new List<FooterText>();
        var columnWidths = showRates ? _pointColumnWidths : _costColumnWidthsWithoutRates;
        var table = new Table(iText.Layout.Properties.UnitValue.CreatePercentArray(columnWidths))
            .UseAllAvailableWidth()
            .SetFixedLayout();

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

            if (settlementData.LastAvailableRateUsed)
            {
                if (!string.IsNullOrWhiteSpace(footerText))
                    footerText = $"{footerText} ";

                footerTextCorrection = getFooterTextLastAvailableRateUsed(settlementData.PriceAdjustmentFactor);
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

            var roundedValue = roundMoney(settlementData.Value);
            var roundedVat = roundMoney(settlementData.VatAmount);

            table.AddCell(GetNormalText(settlementData.Description, 1, 1, iText.Layout.Properties.TextAlignment.LEFT, footerText));
            table.AddCell(GetNormalText(settlementData.StartDate.ToString("dd-MM-yyyy"), 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddCell(GetNormalText(settlementData.EndDate.ToString("dd-MM-yyyy"), 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddCell(GetNormalText(Math.Round(settlementData.ValueBase, 2).ToString()));

            if (showRates)
                table.AddCell(GetNormalText(settlementData.Rate.ToString("##0.00000")));
            table.AddCell(GetNormalText(roundedValue.ToString("##0.00")));

            if (!settlementData.CostCategory.CalculateVat)
                table.AddCell(GetNormalText(""));
            else
                table.AddCell(GetNormalText(roundedVat.ToString("##0.00")));

            table.AddCell(GetNormalText((roundedValue + roundedVat).ToString("##0.00")));
        }

        // Add Footer to table
        if (_footerTextsList.Count > 0)
            table.AddFooterCell(getFooterText(_footerTextsList, 1, showRates ? 8 : 7));

        // Add sub Footer to table
        var settlementSubTotal = new SettlementSubTotal();
        settlementSubTotal.EngergyTypeId = item.EnergyType.Id;
        settlementSubTotal.ValueBase = settlementDataList.Sum(s => s.ValueBase);
        settlementSubTotal.TotalValue = settlementDataList.Sum(s => roundMoney(s.Value));
        settlementSubTotal.TotalVat = settlementDataList.Sum(s => roundMoney(s.VatAmount));

        setSettlementSubTotal(table, settlementSubTotal, subTotalText, showRates);

        return table;
    }

    internal void getCategoryTableHeader(Table table, bool showRates)
    {
        table.AddHeaderCell(GetBoldText("Description", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
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
        table.AddFooterCell(GetBoldTextGrey(rowDescription, 1, showRates ? 5 : 4, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddFooterCell(GetBoldTextGrey(roundMoney(settlementSubTotal.TotalValue).ToString()));
        table.AddFooterCell(GetBoldTextGrey(roundMoney(settlementSubTotal.TotalVat).ToString()));
        table.AddFooterCell(GetBoldTextGrey(roundMoney(settlementSubTotal.TotalValue + settlementSubTotal.TotalVat).ToString()));
    }

    internal void setSettlementSubTotal(Models.EnergyType energyType, List<SettlementData> settlementDataList)
    {
        foreach (var itemSubTotal in getSettlementSubTotals(energyType, settlementDataList))
        {
            var settlementSubTotal = _settlementSubTotalList.FirstOrDefault(
                x => x.SubTotalType == itemSubTotal.SubTotalType && x.EngergyTypeId == energyType.Id);

            if (settlementSubTotal == null)
            {
                _settlementSubTotalList.Add(itemSubTotal);
                continue;
            }

            settlementSubTotal.ValueBase += itemSubTotal.ValueBase;
            settlementSubTotal.TotalValue += itemSubTotal.TotalValue;
            settlementSubTotal.TotalVat += itemSubTotal.TotalVat;
        }
    }

    private List<SettlementSubTotal> getSettlementSubTotals(Models.EnergyType energyType,
                                                            List<SettlementData> settlementDataList)
    {
        var result = new List<SettlementSubTotal>();

        foreach (var settlementData in settlementDataList)
        {
            var subTotalKey = $"{Manager.LibEnergySubType.GetCombinedType(settlementData.CostCategory.EnergySubType.Id)}{energyType.Id}";
            var settlementSubTotal = result.FirstOrDefault(x => x.SubTotalType == subTotalKey);

            if (settlementSubTotal == null)
            {
                settlementSubTotal = new SettlementSubTotal
                {
                    EngergyTypeId = energyType.Id,
                    SubTotalType = subTotalKey,
                    Description = getSubTotalName(settlementData.CostCategory, energyType)
                };
                result.Add(settlementSubTotal);
            }

            settlementSubTotal.ValueBase += settlementData.ValueBase;
            settlementSubTotal.TotalValue += roundMoney(settlementData.Value);
            settlementSubTotal.TotalVat += roundMoney(settlementData.VatAmount);
        }

        return result;
    }

    internal Table setTotalToTable(SelectedEnergyType item,
                                   List<SettlementData> settlementDataList,
                                   bool showRates)
    {
        Table table = new(_pointColumnWidths);
        table.SetKeepTogether(true);
        GetSectionHeader(table, "Totals");

        table.AddHeaderCell(GetBoldText("", 1, showRates ? 5 : 5));
        table.AddHeaderCell(GetBoldText("Money ex."));
        table.AddHeaderCell(GetBoldText("Vat"));
        table.AddHeaderCell(GetBoldText("Money inc."));

        var settlementSubTotalList = getSettlementSubTotals(item.EnergyType, settlementDataList);
        foreach (SettlementSubTotal settlementSubTotal in settlementSubTotalList)
        {
            table.AddCell(GetNormalText(settlementSubTotal.Description, 1, showRates ? 5 : 5, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddCell(GetNormalText(roundMoney(settlementSubTotal.TotalValue).ToString("##0.00")));
            table.AddCell(GetNormalText(roundMoney(settlementSubTotal.TotalVat).ToString("##0.00")));
            table.AddCell(GetNormalText(roundMoney(settlementSubTotal.TotalValue + settlementSubTotal.TotalVat).ToString("##0.00")));
        }

        table.AddFooterCell(GetBoldTextGrey("Total", 1, showRates ? 5 : 5, iText.Layout.Properties.TextAlignment.LEFT));
        var totalValue = settlementSubTotalList.Sum(x => x.TotalValue);
        var totalVat = settlementSubTotalList.Sum(x => x.TotalVat);
        var monthCount = getMonthCount(item.StartRange, item.EndRange);

        table.AddFooterCell(GetBoldTextGrey(roundMoney(totalValue).ToString("##0.00")));
        table.AddFooterCell(GetBoldTextGrey(roundMoney(totalVat).ToString("##0.00")));
        table.AddFooterCell(GetBoldTextGrey(roundMoney(totalValue + totalVat).ToString("##0.00")));

        table.AddFooterCell(GetBoldText("Per Month", 1, showRates ? 5 : 5, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddFooterCell(GetBoldText(roundMoney(totalValue / monthCount).ToString("##0.00")));
        table.AddFooterCell(GetBoldText(roundMoney(totalVat / monthCount).ToString("##0.00")));
        table.AddFooterCell(GetBoldText(roundMoney((totalValue + totalVat) / monthCount).ToString("##0.00")));

        return table;
    }

    #region Footer

    private Cell getFooterText(List<FooterText> footerTextList, int rowspan = 1, int colspan = 1)
    {
        PdfFont normal = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
        Paragraph paragraph = new();

        foreach (FooterText footerText in footerTextList)
        {
            _ = paragraph.Add(new Text(footerText.Counter.ToString()).SetFont(normal).SetTextRise(5).SetFontSize(5));
            _ = paragraph.Add(new Text($"{footerText.Text}{Environment.NewLine}").SetFont(normal));
        }

        Cell cell = new(rowspan, colspan);
        cell.SetFontSize(6);
        cell.Add(paragraph);

        return cell;
    }

    private FooterText getFooterTextLastAvailableRateUsed(decimal priceAdjustmentFactor)
    {
        string text = "Estimated amount: no active rate; last available rate used";
        if (priceAdjustmentFactor != 0)
        {
            decimal percChange = priceAdjustmentFactor - 1;
            if (percChange > 0)
                text += $", increased with {percChange:#0.00%}";
            else if (percChange < 0)
                text += $", decreased with {percChange:#0.00%}";
        }

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
                subTotalName = $"Energy {energyType.Name}";
                break;
            case 3:
            case 4:
                subTotalName = $"Energy return {energyType.Name}";
                break;
            case 5:
                subTotalName = $"Other {energyType.Name}";
                break;
            case 6:
            case 7:
                subTotalName = $"Energy return {energyType.Name}";
                break;
            default:
                subTotalName = $"{costCategory.Name} {energyType.Name}";
                break;
        }

        return subTotalName;
    }

    internal static Paragraph getHeaderParagraph(SelectedEnergyType item, Models.Address address, Models.Meter meter)
    {
        var meterName = string.IsNullOrWhiteSpace(meter.Number)
            ? meter.Description
            : $"{meter.Description} ({meter.Number})";
        var headerText = $"Settlement period: {item.StartRange:dd-MM-yyyy} - {item.EndRange:dd-MM-yyyy}" +
                         $"{Environment.NewLine}Meter: {meterName}";

        return new Paragraph(headerText);
    }

    internal async Task<Table> getMeterReadingSummary(SelectedEnergyType item, long addressId, long meterId)
    {
        var startReading = await _unitOfWork.MeterReadingRepo.SelectRow(
            item.StartRange, item.EnergyType.Id, addressId, meterId);
        var endReading = await _unitOfWork.MeterReadingRepo.SelectRow(
            item.EndRange, item.EnergyType.Id, addressId, meterId);
        var table = new Table(iText.Layout.Properties.UnitValue.CreatePercentArray(new float[] { 50, 25, 25 }))
            .UseAllAvailableWidth()
            .SetFixedLayout()
            .SetKeepTogether(true)
            .SetMarginBottom(10);

        table.AddHeaderCell(GetBoldTextGrey(
            $"Standen notaperiode: {item.StartRange:dd-MM-yyyy} - {item.EndRange:dd-MM-yyyy}",
            1, 3, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Telwerk", cellAlignment: iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Beginstand"));
        table.AddHeaderCell(GetBoldText("Eindstand"));

        addReading("Levering normaal", startReading?.RateNormal, endReading?.RateNormal);
        if (item.EnergyType.HasNormalAndLow)
            addReading("Levering dal", startReading?.RateLow, endReading?.RateLow);
        if (item.EnergyType.HasEnergyReturn)
        {
            addReading("Teruglevering normaal", startReading?.ReturnDeliveryNormal, endReading?.ReturnDeliveryNormal);
            if (item.EnergyType.HasNormalAndLow)
                addReading("Teruglevering dal", startReading?.ReturnDeliveryLow, endReading?.ReturnDeliveryLow);
        }

        return table;

        void addReading(string description, decimal? start, decimal? end)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("nl-NL");
            table.AddCell(GetNormalText(description, cellAlignment: iText.Layout.Properties.TextAlignment.LEFT));
            table.AddCell(GetNormalText(start?.ToString("#,##0.###", culture) ?? "?"));
            table.AddCell(GetNormalText(end?.ToString("#,##0.###", culture) ?? "?"));
        }
    }

    internal async Task<List<MeterPeriod>> getMeterPeriods(SelectedEnergyType item, long addressId)
    {
        var meters = (await _unitOfWork.MeterRepo.SelectOverlappingPeriod(
                addressId,
                item.EnergyType.Id,
                item.StartRange,
                item.EndRange))
            .ToList();

        if (meters.Count == 0)
        {
            throw new InvalidOperationException(
                $"No meter is available for {item.EnergyType.Name} in the selected period " +
                $"{item.StartRange:dd-MM-yyyy} - {item.EndRange:dd-MM-yyyy}.");
        }

        if (meters.Count > 1)
        {
            throw new InvalidOperationException(
                $"The selected period {item.StartRange:dd-MM-yyyy} - {item.EndRange:dd-MM-yyyy} for " +
                $"{item.EnergyType.Name} overlaps multiple meters.");
        }

        var meter = meters[0];
        if (item.StartRange.Date < meter.ActiveFrom.Date ||
            (meter.ActiveTill.HasValue && item.EndRange.Date > meter.ActiveTill.Value.Date))
        {
            throw new InvalidOperationException(
                $"The selected period for {item.EnergyType.Name} must be within meter '{meter.Description}'.");
        }

        return [new MeterPeriod(item, meter)];
    }

    internal async Task<Table> getPayments(long addressId, long periodId, DateTime startRange, DateTime endRange)
    {
        Table table = new(_pointColumnWidths);
        table.SetKeepTogether(true);
        GetSectionHeader(table, "Payments");

        table.AddHeaderCell(GetBoldText("Pay date", 1, 2, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Description", 1, 5, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Amount", 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));

        List<Models.Payment> payments = new();
        var monthsLeft = 0;

        if (periodId > 0)
        {
            payments = (await _unitOfWork.PaymentRepo.SelectByAddressAndPeriod(addressId, periodId)).ToList();
            var predefinedPeriodDates = (await _unitOfWork.PredefinedPeriodDateRepo.GetByPeriodId(periodId)).ToList();

            if (predefinedPeriodDates.Count > 0)
            {
                var periodStart = predefinedPeriodDates.Min(x => x.StartDate);
                var periodEnd = predefinedPeriodDates.Max(x => x.EndDate).Date.AddDays(-1);
                var predefinedMonthCount = getCalendarMonthCount(periodStart, periodEnd);
                var paidMonthCount = payments
                    .Where(p => p.PayDate >= periodStart && p.PayDate <= periodEnd)
                    .Select(p => (p.PayDate.Year, p.PayDate.Month))
                    .Distinct()
                    .Count();

                monthsLeft = Math.Max(0, predefinedMonthCount - paidMonthCount);
            }
        }
        else
            payments = (await _unitOfWork.PaymentRepo.SelectByAddressAndRange(addressId, startRange, endRange.Date.AddTicks(-1))).ToList();

        payments = payments.OrderBy(payment => payment.PayDate).ToList();

        var totalToPay = roundMoney(_settlementSubTotalList.Sum(x => x.TotalValue + x.TotalVat));
        var totalPaid = (decimal)payments.Sum(s => s.Amount);
        var toBePaid = totalToPay - totalPaid;

        if (payments.Count == 0)
        {
            table.AddCell(GetNormalText("No payments", 1, 8, iText.Layout.Properties.TextAlignment.LEFT));

            if (totalToPay != 0)
            {
                table.AddFooterCell(GetBoldText("To be paid", 1, 7, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddFooterCell(GetBoldText(toBePaid.ToString("#0.00"), 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));
            }

            if (monthsLeft > 0)
            {
                table.AddFooterCell(GetBoldText($"Avg per month left, ({toBePaid}/{monthsLeft})", 1, 7, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddFooterCell(GetBoldText(roundMoney(toBePaid / monthsLeft).ToString("#0.00"), 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));
            }
        }
        else
        {
            foreach (var payment in payments)
            {
                table.AddCell(GetNormalText(payment.PayDate.ToString("dd-MM-yyyy"), 1, 2, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddCell(GetNormalText($"{payment.Description}", 1, 5, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddCell(GetNormalText(payment.Amount.ToString("##0.00"), 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));
            }

            // Add Footer                
            table.AddFooterCell(GetBoldTextGrey("Total paid", 1, 7, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddFooterCell(GetBoldTextGrey(roundMoney(totalPaid).ToString("#0.00"), 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));

            if (totalToPay != 0)
            {
                table.AddFooterCell(GetBoldText("To be paid", 1, 7, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddFooterCell(GetBoldText(roundMoney(toBePaid).ToString("#0.00"), 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));
            }

            if (monthsLeft > 0)
            {
                table.AddFooterCell(GetBoldText($"Avg per month left, ({toBePaid}/{monthsLeft})", 1, 7, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddFooterCell(GetBoldText(roundMoney(toBePaid / monthsLeft).ToString("#0.00"), 1, 1, iText.Layout.Properties.TextAlignment.RIGHT));
            }
        }

        return table;
    }

    internal Table getPricePerUnit(SelectedEnergyType item,
                                   List<PeriodicData> periodicDataList,
                                   List<SettlementData> settlementDataList)
    {
        Table table = new(_pointColumnWidths);
        table.SetKeepTogether(true);
        var unit = string.IsNullOrWhiteSpace(item.EnergyType.UnitId) ? "unit" : item.EnergyType.UnitId;
        GetSectionHeader(table, $"Cost per {unit}");

        var totalUnits = periodicDataList.Sum(x => x.ValueYLow + x.ValueYNormal);
        table.AddHeaderCell(GetBoldText("Description", 1, 6, iText.Layout.Properties.TextAlignment.LEFT));
        table.AddHeaderCell(GetBoldText("Excl. VAT"));
        table.AddHeaderCell(GetBoldText("Incl. VAT"));

        addPriceRow("Actual cost after all charges and credits", settlementDataList);
        addPriceRow("Cost excluding return", settlementDataList
            .Where(x => x.CostCategory.EnergySubTypeId is not 3 and not 4 and not 6 and not 7));
        addPriceRow("Variable delivery cost", settlementDataList
            .Where(x => x.CostCategory.CalculationTypeId == 1
                     && (x.CostCategory.EnergySubTypeId is 1 or 2
                         || (x.CostCategory.EnergySubTypeId == 5 && x.CostCategory.UnitId == item.EnergyType.UnitId))));

        table.AddCell(GetNormalText(
            $"Actual cost: all period charges minus all credits, including netting, divided by {totalUnits:#,##0.###} {unit} supplied. " +
            (totalUnits <= 0 ? "No consumption: price per unit is N/A. " : "") +
            "Excluding return: no return credits or return costs. " +
            "Variable: delivery and per-unit taxes, excluding fixed and return costs.",
            1, 8, iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(6));

        return table;

        void addPriceRow(string description, IEnumerable<SettlementData> costs)
        {
            var lines = costs.ToList();
            var netCost = lines.Sum(x => roundMoney(x.Value));
            var vat = lines.Sum(x => roundMoney(x.VatAmount));
            table.AddCell(GetNormalText(description, 1, 6, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddCell(GetNormalText(formatUnitPrice(divideOrZero(netCost, totalUnits), totalUnits)));
            table.AddCell(GetNormalText(formatUnitPrice(divideOrZero(netCost + vat, totalUnits), totalUnits)));
        }
    }

    private static decimal divideOrZero(decimal numerator, decimal denominator)
    {
        return denominator <= 0 ? 0 : Math.Round(numerator / denominator, 5);
    }

    private static decimal roundMoney(decimal value)
    {
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    private static string formatUnitPrice(decimal price, decimal totalUnits)
    {
        return totalUnits <= 0 ? "N/A" : price.ToString("##0.00000");
    }

    private static int getCalendarMonthCount(DateTime startRange, DateTime endRange)
    {
        if (startRange == DateTime.MinValue || endRange == DateTime.MinValue || endRange < startRange)
            return 0;

        return ((endRange.Year - startRange.Year) * 12) + endRange.Month - startRange.Month + 1;
    }

    private static decimal getMonthCount(DateTime startRange, DateTime endRange)
    {
        if (startRange == DateTime.MinValue || endRange == DateTime.MinValue || endRange < startRange)
            return 1;

        var endExclusive = endRange.Date;
        var wholeMonths = ((endExclusive.Year - startRange.Year) * 12) + endExclusive.Month - startRange.Month;
        var anchor = startRange.Date.AddMonths(wholeMonths);

        if (anchor > endExclusive)
        {
            wholeMonths--;
            anchor = startRange.Date.AddMonths(wholeMonths);
        }

        var daysInRemainderMonth = DateTime.DaysInMonth(anchor.Year, anchor.Month);
        var partialMonth = (endExclusive - anchor).Days / (decimal)daysInRemainderMonth;
        var result = wholeMonths + partialMonth;

        return result > 0 ? result : 1;
    }
}
