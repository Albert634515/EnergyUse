using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyUse.Core.Manager
{
    public class LibGeneral
    {
        public static int MonthDiff(DateTime startDate, DateTime endDate)
        {
            return endDate.Month - startDate.Month;
        }
    }
}