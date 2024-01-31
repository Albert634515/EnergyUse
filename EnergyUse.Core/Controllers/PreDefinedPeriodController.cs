using EnergyUse.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyUse.Core.Controllers
{
    public class PreDefinedPeriodController : IController
    {
        #region ControlerProperties

        private string _dbFileName { get; set; } = string.Empty;
        public EnergyUse.Core.UnitOfWork.PreDefinedPeriod? UnitOfWork { get; set; } = null;
        private EnergyUse.Core.Manager.LibSettings? _libSettings { get; set; } = null;

        public bool InitSettings { get; set; } = false;

        #endregion

        #region InitControler

        public PreDefinedPeriodController(string dbFileName)
        {
            _dbFileName = dbFileName;
        }

        public void Initialize()
        {
            setUnitOfWork();
            setSettingsManager();
        }

        private void setUnitOfWork()
        {
            UnitOfWork = new EnergyUse.Core.UnitOfWork.PreDefinedPeriod(_dbFileName);
        }

        private void setSettingsManager()
        {
            _libSettings = new EnergyUse.Core.Manager.LibSettings(_dbFileName);
        }

        public string getDbFileName()
        {
            return _dbFileName;
        }

        #endregion
    }
}
