using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace InstaSync.UOW.Managers
{
    public class LogsManager
    {
        private readonly ILogger logger;

        public LogsManager()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        
        public void Log(string message)
        {
            logger.Debug(message);
        }
    }
}
