using System;

namespace SimpleLog4net
{
    public interface ILog:IDisposable
    {
        void LogWithTime(string msg, ELogLevel logLevel = ELogLevel.Error);
        bool Enabled { get; set; }
    }
}
