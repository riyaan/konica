using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Diagnostics
{
    public class Logger
    {
        #region Singleton Pattern

        private static Logger logger;

        private Logger()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        public static Logger Instance
        {
            get
            {
                if (logger == null)
                    logger = new Logger();

                return logger;
            }
        }

        #endregion

        #region Properties

        NLog.Logger log;

        public NLog.Logger Log
        {
            get
            {
                if (log == null)
                    log = LogManager.GetCurrentClassLogger();

                return log;
            }
        }

        #endregion
    }
}
