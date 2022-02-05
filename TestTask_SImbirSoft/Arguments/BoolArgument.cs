using System;
using NLog.Time;

namespace TestTask_SimbirSoft
{
    /// <summary>
    /// Bool command line argument
    /// </summary>
    public class BoolArgument : CommandLineArgument<bool>
    {
        public override bool Eval()
        {
            bool parse = false;
            Boolean.TryParse(StringValue, out parse);
            return parse;
        }

        /// <summary>
        /// Default value
        /// </summary>
        /// <remarks>
        /// Default value is true because it represents that value PRESENTS in argument list.
        /// </remarks>
        protected override string DefaultValue
        {
            get => "true";
        }
    }
}