namespace TestTask_SimbirSoft
{
    public class TestTaskParserConfigurator : IParserConfigurator
    {
        public CommandLineParser.ParsingConfiguration Configure(CommandLineParser.ParsingConfiguration config)
        {
            config.AddArgument("web", "w");
            config.AddArgument("silent", "s");
            
            return config;
        }
    }
}