/// <summary>
/// 一缕晨光工作室 www.wispdawn.com
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;

namespace WpPCHelper
{
    public class LogHelper
    {
        private static LogHelper logHelper = null;
        public ILog Log
        {
            get;
            set;
        }
        private LogHelper()
        {
            Log = log4net.LogManager.GetLogger(typeof(string));
        }

        public static LogHelper GetInstance()
        {
            if (logHelper == null)
            {
                logHelper = new LogHelper();
            }
            return logHelper;
        }

        public static void Info(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(typeof(String));
            log.Error(msg);
        }
    }
}
