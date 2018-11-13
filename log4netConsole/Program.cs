using SimpleLog4net;

namespace log4netConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog _fileLogger = new FileLogger();
            _fileLogger.LogWithTime("日志测试",ELogLevel.Debug);
            System.Console.WriteLine("日志所在的位置 "+System.AppDomain.CurrentDomain.BaseDirectory);
            System.Console.ReadKey();
        }
    }
}
