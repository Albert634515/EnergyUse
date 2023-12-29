using EnergyUse.Models.Common;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace EnergyUse.Core.Reports
{
    public class Settlement : ReportBase
    {
        #region Properties

        //private readonly EnergyUseContext _context;
        private readonly string _dbFileName;
        private static UnitOfWork.Settlement _unitOfWork;

        private static float[] _pointColumnWidths = { 250F, 115F, 115F, 60F, 75F, 110F, 90F, 110F };
        private static List<SettlementSubTotal> _settlementSubTotalList = new();
        private static List<FooterText> _footerTextsList = new();

        #endregion

        public Settlement(string dbFileName)
        {
            _dbFileName = dbFileName;
            _unitOfWork = new UnitOfWork.Settlement(_dbFileName);
        }

        public string GetSettlementPdf(ParameterSelection parameterSelection)
        {
            Table table;
            DateTime startRange, endRange;
            Models.EnergyType energyType;

            var dest = System.IO.Path.GetTempPath();
            var fileName = $"Settlement_{DateTime.Now:yyyyMMddHHmmss}.pdf";
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
                string lastSubType = "";
                _footerTextsList = new List<FooterText>();
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
                if (periodicData.Count == 0)
                {
                    document.Add(new Paragraph($"No data found for energy type {energyType.Name}"));
                    document.Add(new Paragraph("\n"));
                }
                else
                {
                    table = new Table(_pointColumnWidths);
                    getSectionHeader(table, getSectionHeaderText(item, address));

                    //table.SetKeepTogether(true);
                    getCategoryTableHeader(table);

                    var costCategoryList = _unitOfWork.CostCategoriesRepo.SelectByEnergyTypeAndRange(energyType.Id, startRange, endRange);
                    //List<Db.CostCategory> costCategoryList = new List<CostCategory>();
                    //costCategoryList.Add(Db.CostCategory.SelectById(7));
                    foreach (Models.CostCategory costCategory in costCategoryList)
                    {
                        long tarifGroupId = (long)(costCategory.TariffGroup != null && costCategory.TariffGroup.Id > 0 ? costCategory.TariffGroup.Id : item.TarifGroup);

                        if (lastSubType != Manager.LibEnergySubType.GetCombinedType(costCategory.EnergySubType.Id) && !string.IsNullOrWhiteSpace(lastSubType))
                        {
                            //Write subfooter to last table
                            if (!string.IsNullOrWhiteSpace(lastSubType))
                            {
                                getFooterTable(table, $"{lastSubType}{energyType.Id}", energyType.Id);
                                document.Add(table);
                                document.Add(new Paragraph(""));

                                getSectionHeader(table, costCategory.EnergySubType.Description);
                            }

                            table = new Table(_pointColumnWidths);
                            table.SetKeepTogether(true);
                            getSectionHeader(table, $"Cost categories {energyType.Name}");
                            getCategoryTableHeader(table);

                        }

                        document.Add(new Paragraph(""));
                        getCostTable(table, energyType, periodicData, startRange, endRange, costCategory, tarifGroupId);

                        lastSubType = Manager.LibEnergySubType.GetCombinedType(costCategory.EnergySubType.Id);
                    }

                    //Write last subfooter
                    getFooterTable(table, $"{lastSubType}{energyType.Id}", energyType.Id);
                    getSubTotalEnergyType(table, energyType);

                    document.Add(table);
                    document.Add(new Paragraph(""));
                }
            } // End of loop of selected energy types


            table = setTotalToTable();
            document.Add(table);
            document.Add(new Paragraph(""));

            table = getPayments(address.Id, parameterSelection.PreSelectedPeriodId, parameterSelection.StartRange, parameterSelection.EndRange);
            document.Add(table);

            document.Close();

            return System.IO.Path.Combine(dest, fileName);
        }

        private void getCategoryTableHeader(Table table)
        {
            table.AddHeaderCell(GetBoldText("Description", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddHeaderCell(GetBoldText("From", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddHeaderCell(GetBoldText("Till", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddHeaderCell(GetBoldText("Unit"));
            table.AddHeaderCell(GetBoldText("Rate"));
            table.AddHeaderCell(GetBoldText("Total ex.vat"));
            table.AddHeaderCell(GetBoldText("Vat"));
            table.AddHeaderCell(GetBoldText("Total"));
        }

        private static Paragraph getHeaderParagraph(SelectedEnergyType item, Models.Address address)
        {
            var headerText = $"Settlement period: {item.StartRange:dd-MM-yyyy} - {item.EndRange:dd-MM-yyyy}";

            return new Paragraph(headerText);
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

        /// <summary>
        /// Combine subtotal of energy and other costs
        /// </summary>
        /// <param name="table"></param>
        /// <param name="energyType"></param>
        private void getSubTotalEnergyType(Table table, Models.EnergyType energyType)
        {
            SettlementSubTotal settlementSubTotal = new();
            foreach (SettlementSubTotal item in _settlementSubTotalList.Where(x => x.EngergyTypeId == energyType.Id))
            {
                settlementSubTotal.EngergyTypeId = energyType.Id;
                settlementSubTotal.Description = string.Empty;
                settlementSubTotal.SubTotalType = "SubTotalEnergyType";
                settlementSubTotal.TotalVat += item.TotalVat;
                settlementSubTotal.TotalValue += item.TotalValue;
            }

            if (settlementSubTotal != null)
            {
                // Add Footer
                table.AddFooterCell(GetBoldTextGrey($"Sub total {energyType.Name}", 1, 5, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddFooterCell(GetBoldTextGrey(Math.Round(settlementSubTotal.TotalValue, 2).ToString("#0.00")));
                table.AddFooterCell(GetBoldTextGrey(Math.Round(settlementSubTotal.TotalVat, 2).ToString("0.00")));
                table.AddFooterCell(GetBoldTextGrey(Math.Round(settlementSubTotal.TotalValue + settlementSubTotal.TotalVat, 2).ToString("#0.00")));
            }
        }

        private void setSubTotaltoTable(Table table, SettlementSubTotal settlementSubTotal, string rowDescription)
        {
            // Add Footer
            table.AddFooterCell(GetBoldText(rowDescription, 1, 3, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddFooterCell(GetBoldText(Math.Round(settlementSubTotal.ValueBase, 2).ToString()));
            table.AddFooterCell(GetNormalText(""));
            table.AddFooterCell(GetBoldText(Math.Round(settlementSubTotal.TotalValue, 2).ToString()));
            table.AddFooterCell(GetBoldText(Math.Round(settlementSubTotal.TotalVat, 2).ToString()));
            table.AddFooterCell(GetBoldText(Math.Round(settlementSubTotal.TotalValue + settlementSubTotal.TotalVat, 2).ToString()));
        }

        private Table setTotalToTable()
        {
            Table table = new(_pointColumnWidths);
            table.SetKeepTogether(true);
            getSectionHeader(table, "Totals");

            table.AddHeaderCell(GetBoldText("", 1, 3));
            table.AddHeaderCell(GetBoldText("Unit"));
            table.AddHeaderCell(GetBoldText(""));
            table.AddHeaderCell(GetBoldText("Money ex."));
            table.AddHeaderCell(GetBoldText("Vat"));
            table.AddHeaderCell(GetBoldText("Money inc."));

            foreach (SettlementSubTotal settlementSubTotal in _settlementSubTotalList)
            {
                table.AddCell(GetNormalText(settlementSubTotal.Description, 1, 3, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddCell(GetNormalText(Math.Round(settlementSubTotal.ValueBase, 2).ToString()));
                table.AddCell(GetNormalText(""));
                table.AddCell(GetNormalText(Math.Round(settlementSubTotal.TotalValue, 2).ToString("##0.00")));
                table.AddCell(GetNormalText(Math.Round(settlementSubTotal.TotalVat, 2).ToString("##0.00")));
                table.AddCell(GetNormalText(Math.Round(settlementSubTotal.TotalValue + settlementSubTotal.TotalVat, 2).ToString("##0.00")));
            }

            table.AddFooterCell(GetBoldTextGrey("Total", 1, 5, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddFooterCell(GetBoldTextGrey(Math.Round(_settlementSubTotalList.Sum(x => x.TotalValue), 2).ToString("##0.00")));
            table.AddFooterCell(GetBoldTextGrey(Math.Round(_settlementSubTotalList.Sum(x => x.TotalVat), 2).ToString("##0.00")));
            table.AddFooterCell(GetBoldTextGrey(Math.Round(_settlementSubTotalList.Sum(x => x.TotalValue) + _settlementSubTotalList.Sum(x => x.TotalVat), 2).ToString("##0.00")));

            table.AddFooterCell(GetBoldText("Per Month", 1, 5, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddFooterCell(GetBoldText(Math.Round(_settlementSubTotalList.Sum(x => x.TotalValue) / 12, 2).ToString("##0.00")));
            table.AddFooterCell(GetBoldText(Math.Round(_settlementSubTotalList.Sum(x => x.TotalVat) / 12, 2).ToString("##0.00")));
            table.AddFooterCell(GetBoldText(Math.Round((_settlementSubTotalList.Sum(x => x.TotalValue) + _settlementSubTotalList.Sum(x => x.TotalVat)) / 12, 2).ToString("##0.00")));

            return table;
        }

        private Table getPayments(long addressId, long periodId, DateTime startRange, DateTime endRange)
        {
            Table table = new(_pointColumnWidths);
            table.SetKeepTogether(true);
            getSectionHeader(table, "Payments");

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

        private void getFooterTable(Table table, string subtypeKey, long energyTypeId)
        {
            SettlementSubTotal? settlementSubTotal = _settlementSubTotalList.FirstOrDefault(x => x.SubTotalType == subtypeKey && x.EngergyTypeId == energyTypeId);
            if (settlementSubTotal != null)
            {
                setSubTotaltoTable(table, settlementSubTotal, "Sub total");
            }

            if (_footerTextsList.Count > 0)
                table.AddFooterCell(getFooterText(_footerTextsList, 1, 8));

            _footerTextsList = new List<FooterText>();
        }

        private void getCostTable(Table table, Models.EnergyType energyType, List<PeriodicData> periodicData, DateTime startRange, DateTime endRange, EnergyUse.Models.CostCategory costCategory, long tarifGroupId)
        {
            var libSettlementData = new Manager.LibSettlementData(_dbFileName);
            List<SettlementData> settlementDataList = libSettlementData.GetSettlementCost(energyType.Id, periodicData, costCategory, tarifGroupId);
            foreach (SettlementData settlementData in settlementDataList)
            {
                decimal vat = settlementData.Value * (settlementData.VatTarif / 100);
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

                if (costCategory.CalculateVat && settlementData.VatTarif == 0)
                {
                    if (!string.IsNullOrWhiteSpace(footerText))
                        footerText = $"{footerText} ";

                    footerTextCorrection = addFooterText("Vat percentage missing");
                    footerText = $"{footerText}{footerTextCorrection.Counter}";
                }
                else if (costCategory.CalculateVat && settlementData.VatTarif > 0)
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

                table.AddCell(GetNormalText(settlementData.Description, 1, 1, iText.Layout.Properties.TextAlignment.LEFT, footerText));
                table.AddCell(GetNormalText(settlementData.StartDate.ToString("dd-MM-yyyy"), 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddCell(GetNormalText(settlementData.EndDate.ToString("dd-MM-yyyy"), 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
                table.AddCell(GetNormalText(Math.Round(settlementData.ValueBase, 2).ToString()));

                table.AddCell(GetNormalText(settlementData.Rate.ToString("##0.00000")));
                table.AddCell(GetNormalText(Math.Round(settlementData.Value, 2).ToString("##0.00")));

                if (!costCategory.CalculateVat)
                    table.AddCell(GetNormalText(""));
                else
                    table.AddCell(GetNormalText(Math.Round(vat, 2).ToString("##0.00")));

                table.AddCell(GetNormalText(Math.Round(settlementData.Value + vat, 2).ToString("##0.00")));
            }

            var subTotalKey = $"{Manager.LibEnergySubType.GetCombinedType(costCategory.EnergySubType.Id)}{energyType.Id}";
            SettlementSubTotal? settlementSubTotal = _settlementSubTotalList.FirstOrDefault(x => x.SubTotalType == subTotalKey && x.EngergyTypeId == energyType.Id);
            if (settlementSubTotal == null)
            {
                settlementSubTotal = new SettlementSubTotal();
                settlementSubTotal.EngergyTypeId = energyType.Id;
                settlementSubTotal.SubTotalType = subTotalKey;
                settlementSubTotal.Description = getSubTotalName(costCategory, energyType);
                _settlementSubTotalList.Add(settlementSubTotal);
            }

            settlementSubTotal.ValueBase += settlementDataList.Sum(s => s.ValueBase);
            settlementSubTotal.TotalValue += settlementDataList.Sum(s => s.Value);
            settlementSubTotal.TotalVat += settlementDataList.Sum(s => s.Value * (s.VatTarif / 100));
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

        private string getSubTotalName(Models.CostCategory costCategory, Models.EnergyType energyType)
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

        private Table getRateDatails(Common.Enums.SubEnergyType subEnergyType, List<PeriodicData> periodicDataList)
        {
            Table table = new(_pointColumnWidths);

            table.AddHeaderCell(GetBoldText("Description", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddHeaderCell(GetBoldText("From", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddHeaderCell(GetBoldText("Till", 1, 1, iText.Layout.Properties.TextAlignment.LEFT));
            table.AddHeaderCell(GetBoldText("Unit"));
            table.AddHeaderCell(GetBoldText("Rate"));
            table.AddHeaderCell(GetBoldText("Money ex"));
            table.AddHeaderCell(GetBoldText("Vat"));
            table.AddHeaderCell(GetBoldText("Money inc"));

            foreach (PeriodicData periodicData in periodicDataList)
            {
                decimal btw = periodicData.ValueYMonetaryNormal * (100 / 21);

                table.AddCell(subEnergyType.ToString());
                table.AddCell(periodicData.ValueX.ToString());
                table.AddCell(periodicData.ValueX.ToString());

                switch (subEnergyType)
                {
                    case Common.Enums.SubEnergyType.Normal:
                        table.AddCell(periodicData.ValueYNormal.ToString());
                        table.AddCell(periodicData.RateNormal.ToString());
                        table.AddCell(Math.Round(periodicData.ValueYMonetaryNormal, 2).ToString("##0.00"));
                        table.AddCell(Math.Round(btw, 2).ToString("##0.00"));
                        table.AddCell(Math.Round(periodicData.ValueYMonetaryNormal + btw, 2).ToString("##0.00"));
                        break;
                    case Common.Enums.SubEnergyType.Low:
                        table.AddCell(periodicData.ValueYLow.ToString());
                        table.AddCell(periodicData.RateLow.ToString("##0.00000"));
                        table.AddCell(Math.Round(periodicData.ValueYMonetaryLow, 2).ToString("##0.00"));
                        table.AddCell(Math.Round(btw, 2).ToString("##0.00"));
                        table.AddCell(Math.Round(periodicData.ValueYMonetaryLow + btw, 2).ToString("##0.00"));
                        break;
                    case Common.Enums.SubEnergyType.ReturnNormal:
                        table.AddCell(periodicData.ValueYReturnNormal.ToString());
                        table.AddCell(periodicData.RateReturnNormal.ToString("##0.00000"));
                        table.AddCell(Math.Round(periodicData.ValueYMonetaryNormal, 2).ToString("##0.00"));
                        table.AddCell(Math.Round(btw, 2).ToString("##0.00"));
                        table.AddCell(Math.Round(periodicData.ValueYMonetaryNormal + btw, 2).ToString("##0.00"));
                        break;
                    case Common.Enums.SubEnergyType.ReturnLow:
                        table.AddCell(periodicData.ValueYReturnLow.ToString());
                        table.AddCell(periodicData.RateReturnLow.ToString("##0.00000"));
                        table.AddCell(Math.Round(periodicData.ValueYMonetaryLow, 2).ToString("##0.00"));
                        table.AddCell(Math.Round(btw, 2).ToString("##0.00"));
                        table.AddCell(Math.Round(periodicData.ValueYMonetaryLow + btw, 2).ToString("##0.00"));
                        break;
                    default:
                        break;
                }
            }

            return table;
        }

        #region Layout Methods

        private void getSectionHeader(Table table, string headerText)
        {
            table.SetKeepTogether(true);

            table.AddHeaderCell(GetBoldTextGrey(headerText, 1, 8, iText.Layout.Properties.TextAlignment.LEFT));
        }

        private string getSectionHeaderText(SelectedEnergyType item, Models.Address address)
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

        #endregion
    }
}