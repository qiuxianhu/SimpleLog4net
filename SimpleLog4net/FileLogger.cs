using System;
using System.Diagnostics;
using System.IO;

namespace SimpleLog4net
{
    public class FileLogger:ILog
    {
        private string _configLevel = null;
        private bool _enabled = true;
        private log4net.ILog _log=null;
        public FileLogger()
        {
            this._configLevel = GetAppSettingValue("LogLevel");
            string logName = GetAppSettingValue("LogName");
            string configPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"log4net.xml");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
            this._log = log4net.LogManager.GetLogger(logName);
        }

        public static string GetAppSettingValue(string key)
        {
            string value = null;
            foreach (string item in System.Configuration.ConfigurationManager.AppSettings)
            {
                if (item.Equals(key,System.StringComparison.CurrentCultureIgnoreCase))
                {
                    value = System.Configuration.ConfigurationManager.AppSettings[key];
                    break;
                }
            }
            return value;
        }

        #region ILog成员
        public void LogWithTime(string msg,ELogLevel logLevel)
        {
            if (string.IsNullOrWhiteSpace(msg)||!this._enabled)
            {
                return;
            }
#if DEBUG
            Trace.TraceInformation(msg);
#endif
            if (string.IsNullOrWhiteSpace(this._configLevel))
            {
                this._configLevel = ((int)ELogLevel.Error).ToString();
            }
            int configLevel = Convert.ToInt32(this._configLevel);
            if ((int)logLevel<configLevel)
            {
                try
                {
                    switch (logLevel)
                    {
                        case ELogLevel.Error:
                            this._log.Error(msg);
                            break;
                        case ELogLevel.Trace:
                            this._log.Warn(msg);
                            break;
                        case ELogLevel.Debug:
                            this._log.Debug(msg);
                            break;
                        case ELogLevel.Info:
                            this._log.Info(msg);
                            break;
                        default:
                            break;
                    }
                }
                catch 
                {
                }
            }
        }
        public bool Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; }
        }
        #endregion

        #region IDisposable
        public void Dispose()
        { }
        #endregion 
    }
}
