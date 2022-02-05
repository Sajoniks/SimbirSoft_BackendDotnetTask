using System;
using NLog;
using TestTask_SimbirSoft.Logging;
using TestTask_SimbirSoft.StringUtils;
using TestTask_SimbirSoft.Web;

namespace TestTask_SimbirSoft
{
    class Program
    {
        // Default program logger
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        // Cmd arguments parser
        private static readonly CommandLineParser Parser = new CommandLineParser();
        
        static void Main(string[] args)
        {
            // Init config with default settings
            new LogInit().Configure(new DefaultLogConfig());
            // Init command line parser with project specific settings
            new ParserInit().Configure(new TestTaskParserConfigurator());

            string website = String.Empty;
            bool bFlushStats = true;

            try
            {
                // Get website URI that will be parsed
                website = Parser.Get<StringArgument>("--web");
            }
            catch (Exception e)
            {
                Logger.Error($"Failed to parse needed command line argument --web. Fallback to default");
                website = "https://www.simbirsoft.com";
                Logger.Info($"Setting default --web={website}");
            }

            try
            {
                // Will flush words statistics to console?
                bFlushStats = !Parser.Get<BoolArgument>("--silent");
            }
            catch (Exception e)
            {
                Logger.Error("Failed to parse needed command line argument: --silent");
                bFlushStats = true;
                Logger.Error($"Setting default --silent={bFlushStats}");
            }
            
            
            try
            {
                Uri uri = new Uri(website);
                Logger.Info($"Preparing {uri} for parsing.");

                using (HtmlTextStreamReader streamReader = new HtmlTextStreamReader(uri))
                {
                    // Receive content
                    streamReader.Receive().Wait();

                    var content = streamReader.ReadToEnd().Result;
                    Logger.Info($"Received content size: {content.Length}");
                    
                    // Prepare word extractor
                    var extractor = new WordExtractor(content)
                    {
                        // We can define our delimiters here
                        Delimiters = new [] {' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t'}
                    };
                    
                    // Calculate statistics (histogram)
                    var words = extractor.GetHistogram();
                    Logger.Info($"Parsed statistics for {words.Count} words");   
                    
                    // Put stats to logs
                    if (bFlushStats)
                        foreach (var word in words)
                        {
                            Logger.Info($"{word.Key} : {word.Value}");
                        }
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Error occured while parsing uri: {e}");
            }
        }
    }
}