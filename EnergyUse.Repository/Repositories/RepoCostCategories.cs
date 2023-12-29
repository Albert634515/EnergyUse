using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories
{
    public class RepoCostCategories : RepoGeneral<Models.CostCategory>
    {
        private readonly EnergyUseContext _context;

        public RepoCostCategories(EnergyUseContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        //@"SELECT costcategory.*, 
        //                            energysubtype.description, 
        //                            calculationtype.description CalculationType 
        //                       FROM costcategory
        //                  LEFT JOIN energysubtype ON energysubtype.id = costcategory.energysubtypeid
        //                  LEFT JOIN calculationtype ON calculationtype.id = costcategory.calculationtypeid
        //                      WHERE costcategory.energyTypeId = $energyTypeId
        //                        AND costcategory.Id IN (SELECT Distinct CostCategoryId
        //                                                  FROM rate 
        //                                                 WHERE rate.energytypeId = $energyTypeId)
        //                        AND unitid = $unitid
        //                   ORDER BY sortorder";

        public IEnumerable<Models.CostCategory> SelectByEnergyTypeAndUntit(long energyTypeId, string unitId)
        {
            var categoryList = _context.Set<Models.Rate>()
                                       .Include(c => c.CostCategory)
                                       .Where(w => w.EnergyTypeId == energyTypeId)
                                       .Select(s => s.CostCategory.Id).ToList();

            if (categoryList.Count == 0)
                categoryList.Add(0);

            return _context.Set<Models.CostCategory>()
                           .Include(e => e.EnergyType)
                           .Include(t => t.TariffGroup)
                           .Include(s => s.EnergySubType)
                           .Include(c => c.CalculationType)
                           .Include(u => u.Unit)
                           .Where(w => w.EnergyType.Id == energyTypeId && w.Unit.Id == unitId && categoryList.Contains(w.Id))
                           .OrderBy(o => o.SortOrder);
        }

        public IEnumerable<Models.CostCategory> SelectByEnergyType(long energytypeid)
        {
            return _context.Set<Models.CostCategory>()
                           .Include(e => e.EnergyType)
                           .Include(t => t.TariffGroup)
                           .Include(s => s.EnergySubType)
                           .Include(c => c.CalculationType)
                           .Where(w => w.EnergyType.Id == energytypeid)
                           .OrderBy(o => o.EnergySubType.Id).ThenBy(t => t.SortOrder);
        }

        public Models.CostCategory? SelectByEnergyTypeAndSubType(long energyTypeId, Common.Enums.SubEnergyType subEnergyType)
        {
            return _context.Set<Models.CostCategory>()
                           .Include(e => e.EnergyType)
                           .Include(t => t.TariffGroup)
                           .Include(s => s.EnergySubType)
                           .Include(c => c.CalculationType)
                           .Where(w => w.EnergyType.Id == energyTypeId && w.EnergySubType.Id == (int)subEnergyType)
                           .OrderBy(o => o.SortOrder)
                           .FirstOrDefault();
        }

        public IEnumerable<Models.CostCategory> SelectByEnergyTypeId(long energyTypeId)
        {
            return _context.Set<Models.CostCategory>()
                           .Include(e => e.EnergyType)
                           .Include(t => t.TariffGroup)
                           .Include(s => s.EnergySubType)
                           .Include(c => c.CalculationType)
                           .Where(w => w.EnergyType.Id == energyTypeId)
                           .OrderBy(o => o.EnergyType.Id).ThenBy(o => o.SortOrder);
        }

        public IEnumerable<Models.CostCategory> SelectByEnergyTypeAndRange(long energyTypeId, DateTime startDate, DateTime endDate)
        {
            return _context.Set<Models.CostCategory>()
                           .Include(e => e.EnergyType)
                           .Include(t => t.TariffGroup)
                           .Include(s => s.EnergySubType)
                           .Include(c => c.CalculationType)
                           .Where(w => w.EnergyType.Id == energyTypeId && (w.Start.Value.Date <= endDate.Date || w.End.Value.Date >= startDate.Date))
                           .OrderBy(o => o.EnergyType.Id).ThenBy(o => o.SortOrder);
        }

        public IEnumerable<Models.CostCategory> SelectByEnergyTypeIdVatCalculated(long energyTypeId)
        {
            return _context.Set<Models.CostCategory>()
                           .Include(e => e.EnergyType)
                           .Include(t => t.TariffGroup)
                           .Include(s => s.EnergySubType)
                           .Include(c => c.CalculationType)
                           .Where(w => w.EnergyTypeId == energyTypeId && w.CalculateVat)
                           .OrderBy(o => o.EnergyType.Id).ThenBy(o => o.SortOrder);
        }

        public Models.CostCategory GetDefault(long energyTypeId)
        {
            Models.CostCategory costCategory = new Models.CostCategory();
            costCategory.Id = 0;
            costCategory.EnergySubType = new Models.EnergySubType();
            costCategory.EnergyType = new Models.EnergyType();
            costCategory.EnergyType.Id = (int)energyTypeId;

            return costCategory;
        }

    }
}
