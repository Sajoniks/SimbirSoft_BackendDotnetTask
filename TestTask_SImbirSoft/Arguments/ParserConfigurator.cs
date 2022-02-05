using TestTask_SimbirSoft.Init;

namespace TestTask_SimbirSoft
{
    /// <summary>
    /// Base interface for all CommandLineParser configurator objects
    /// </summary>
    public interface IParserConfigurator : IInitializer<CommandLineParser.ParsingConfiguration>
    {
    }
}