using NLog;
using NLog.Config;

namespace TestTask_SimbirSoft.Logging
{
    /// <summary>
    /// Default NLog.Logger configuration. Uses file and console targets
    /// </summary>
    public class DefaultLogConfig : ILogConfigurator
    {
        public LoggingConfiguration Configure(LoggingConfiguration config)
        {
            var logConsole = new NLog.Targets.ConsoleTarget("logConsole");
            var logFile = new NLog.Targets.FileTarget("logFile");
            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logFile);

            return config;
        }
    }
}