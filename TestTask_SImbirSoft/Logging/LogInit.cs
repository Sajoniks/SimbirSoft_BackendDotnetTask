using System;
using System.Diagnostics.CodeAnalysis;
using NLog;
using NLog.Config;
using TestTask_SimbirSoft.Init;

namespace TestTask_SimbirSoft.Logging
{
    /// <summary>
    /// Base class for objects that initialize NLog.Logger
    /// </summary>
    public class LogInit : InitHandler<ILogConfigurator>
    {
        public override void Configure([NotNull] ILogConfigurator configurator)
        {
            NLog.LogManager.Configuration = configurator.Configure(new LoggingConfiguration());
        }
    }
}