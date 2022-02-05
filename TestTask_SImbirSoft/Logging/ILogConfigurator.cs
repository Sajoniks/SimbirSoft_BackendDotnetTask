using NLog.Config;
using TestTask_SimbirSoft.Init;

namespace TestTask_SimbirSoft.Logging
{
    /// <summary>
    /// Base interface of NLog.Logger configurator objects
    /// </summary>
    public interface ILogConfigurator : IInitializer<LoggingConfiguration>
    { }
}