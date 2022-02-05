using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TestTask_SimbirSoft
{
    /// <summary>
    /// Compiles application command line arguments to be easily accessible in a type-safe way.
    /// Arguments can be "fully-qualified" (example: --arg=x) or can have an alias (example: -a=x)
    /// </summary>
    public class CommandLineParser
    {
        /// <summary>
        /// Configuration object of the application CommandLineParser
        /// Contains arguments that must be recognizable by parser
        /// </summary>
        public class ParsingConfiguration
        {
            private HashSet<string> _arguments;             // Fully-qualified arguments (example: --app, --info ...)
            private Dictionary<string, string> _aliases;    // Aliases of defined fully-qualified arguments (example: -a for --app, ...)

            public ParsingConfiguration()
            {
                _aliases = new Dictionary<string, string>();
                _arguments = new HashSet<string>();
            }
            
            /// <summary>
            /// Add recognizable argument
            /// </summary>
            /// <param name="form">Full form of the argument</param>
            public void AddArgument([NotNull] string form)
            {
                AddArgument(form, null);
            }

            /// <summary>
            /// Add recognizable argument with an alias
            /// </summary>
            /// <param name="baseForm">Fully-qualified form of the argument</param>
            /// <param name="shortForm">Alias</param>
            public void AddArgument([NotNull] string baseForm, string shortForm)
            {
                _arguments.Add("--" + baseForm.Replace("-", String.Empty));
                if (shortForm != null)
                    _aliases.Add("-" + shortForm.Replace("-", String.Empty), baseForm);
            }

            /// <summary>
            /// Check if passed argument can be parsed by parser
            /// </summary>
            /// <param name="form">Fully-qualified -or- alias of the argument</param>
            /// <returns>True, if parser can recognize argument</returns>
            /// <exception cref="Exception">Illformed argument format (must be --arg for fully-qualified or -arg for aliases)</exception>
            public bool IsValidArgument(string form)
            {
                bool isFull = form.StartsWith("--");
                if (!isFull && !form.StartsWith("-")) 
                    throw new Exception("Invalid form passed");

                if (!isFull)
                    form = _aliases[form];

                return _arguments.Contains(form);
            }


        }

        /// <summary>
        /// Current parser configuration
        /// </summary>
        public static ParsingConfiguration Configuration { get; set; }
        private static readonly ParsingConfiguration DefaultConfiguration = new ParsingConfiguration();

        /// <summary>
        /// Parsed arguments
        /// </summary>
        private readonly Dictionary<string, string> _parsedArgs;

        public CommandLineParser()
        {
            // Fallback to default configuration if was not set
            if (Configuration == null)
                Configuration = DefaultConfiguration;

            _parsedArgs = new Dictionary<string, string>();
            
            // Get args
            var args = Environment.GetCommandLineArgs();
           
            foreach (var arg in args)
            {
                // Skip illformed arguments
                if (!arg.StartsWith('-')) continue;
                
                // Arguments is a key=value pair
                var keyvalue = arg.Split('=');
                
                string parsed = null;
                string value = null;
                
                // Must have a key
                if (keyvalue.Length >= 1)
                {
                    parsed = keyvalue[0];
                }
                
                // Can have a value
                if (keyvalue.Length == 2)
                {
                    value = keyvalue[1];
                }
                
                _parsedArgs.Add(parsed, value);
            }
        }

        /// <summary>
        /// Get parsed command line argument of given type
        /// </summary>
        /// <param name="argument">Fully-qualified or alias form of an argument</param>
        /// <typeparam name="TCmd">Type of the command line argument</typeparam>
        /// <returns>Argument</returns>
        /// <exception cref="Exception">Illformed argument form</exception>
        public TCmd Get<TCmd>(string argument) where TCmd : CommandLineArgument, new()
        {
            if (!Configuration.IsValidArgument(argument)) 
                throw new Exception("Unknown command line argument");
            
            CommandLineArgument cmd = new TCmd() as CommandLineArgument;
            cmd.Value = _parsedArgs[argument];
            
            return (TCmd) cmd;
        }
    }
}