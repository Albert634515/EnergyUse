﻿using EnergyUse.Core.Context;
using EnergyUse.Core.Graphs.LiveCharts;
using EnergyUse.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Manager
{
    public class LibPriceRate
    {
        #region Properties

        private readonly EnergyUseContext _context;
        private Repositories.RepoRate _rateRepo;
        private Repositories.RepoCostCategories _costCategoryRepo;

        #endregion  

        public LibPriceRate(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            _rateRepo = new Repositories.RepoRate(_context);
            _costCategoryRepo = new Repositories.RepoCostCategories(_context);
        }

        /// <summary>
        /// Calculate cost per 1 item
        /// </summary>
        /// <param name="energyTypeId"></param>
        /// <param name="costDate"></param>
        /// <param name="subEnergyType"></param>
        /// <returns>Db.Rate class</returns>
        public PriceRate GetCalculatedRate(long energyTypeId, DateTime costDate, Common.Enums.SubEnergyType subEnergyType, long tarifGroupId)
        {
            decimal priceIncrease = 0;
            decimal rateValue = 0;
            Models.Rate? rate = null;
            var priceRate = new PriceRate();

            var rateRepo = new Repositories.RepoRate(_context);

            var costCategory = getCostCategory(energyTypeId, subEnergyType);
            List<Models.Rate> rates = getByCostCategoryAndDate(energyTypeId, costCategory.Id, costDate, costDate, tarifGroupId);
            if (rates == null || rates.Count == 0)
            {
                rate = getLastRateByDate(energyTypeId, costCategory.Id, costDate, tarifGroupId);
                if (rate == null)
                    rate = new Models.Rate();

                if (rate.ExpectedPriceChange != 0)
                {
                    priceIncrease = 1 + (rate.ExpectedPriceChange / 100);
                    rateValue = Math.Round(rate.RateValue * priceIncrease, 4);
                }
                else
                    rateValue = rate.RateValue;
            }
            else
            {
                rate = rates.Where(x => x.StartRate.Date <= costDate.Date && x.EndRate.Date >= costDate.Date).FirstOrDefault();
                if (rate != null)
                    rateValue = rate.RateValue;
            }

            priceRate.Rate = rateValue;
            priceRate.Increase = priceIncrease;

            return priceRate;
        }

        #region Rate


        private List<Models.Rate> _lastRate = new();
        private Models.Rate? getLastRateByDate(long energyTypeId, long costCategoryId, DateTime lastDate, long tarifGroupId)
        {
            var rate = _lastRate.Where(x => x.CostCategory.Id == costCategoryId
                                         && x.TariffGroup.Id == tarifGroupId
                                         && x.EnergyType.Id == energyTypeId
                                         && x.StartRate.Date <= lastDate.Date)
                                             .OrderByDescending(o => o.StartRate).FirstOrDefault();
            if (rate != null)
                return rate;
            else
            {
                rate = _rateRepo.SelectLastRateByDate(energyTypeId, costCategoryId, lastDate, tarifGroupId);
                if (rate != null)
                    _lastRate.Add(rate);

                return rate;
            }
        }

        private List<Models.Rate> _rates = new();
        private List<Models.Rate> getByCostCategoryAndDate(long energyTypeId, long costCategoryId, DateTime startDate, DateTime endDate, long tarifGroupId)
        {
            var rates = _rates.Where(x => x.EnergyType.Id == energyTypeId && x.CostCategory.Id == costCategoryId && x.TariffGroup.Id == tarifGroupId && (x.StartRate.Date <= endDate.Date && x.EndRate.Date >= startDate.Date)).ToList();
            if (rates != null && rates.Count > 0)
                return rates;

            rates = _rateRepo.SelectByCostCategoryAndDate(energyTypeId, costCategoryId, startDate, endDate, tarifGroupId).ToList();
            _rates.AddRange(rates);
            return rates;
        }

        #endregion

        #region CostCategory

        private Dictionary<string, Models.CostCategory> _costCategories = new();

        private Models.CostCategory getCostCategory(long energyTypeId, Common.Enums.SubEnergyType subEnergyType)
        {
            var key = $"{energyTypeId}{subEnergyType}";
            if (!_costCategories.ContainsKey(key))
            {
                var costCategory = _costCategoryRepo.SelectByEnergyTypeAndSubType(energyTypeId, subEnergyType);
                if (costCategory != null)
                    _costCategories.Add(key, costCategory);
            }

            if (_costCategories.ContainsKey(key))
                return _costCategories[key];
            else
                return new Models.CostCategory();
        }

        #endregion
    }
}