using System.Diagnostics.CodeAnalysis;
using TestTask_SimbirSoft.Init;

namespace TestTask_SimbirSoft
{
    /// <summary>
    /// Base class for parser configuration
    /// </summary>
    public class ParserInit : InitHandler<IParserConfigurator>
    {
        public override void Configure([NotNull] IParserConfigurator initializer)
        {
            CommandLineParser.Configuration = initializer.Configure(new CommandLineParser.ParsingConfiguration());
        }
    }
}