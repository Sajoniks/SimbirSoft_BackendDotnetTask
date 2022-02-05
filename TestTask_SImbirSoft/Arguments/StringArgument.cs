using System;

namespace TestTask_SimbirSoft
{
    /// <summary>
    /// String command line argument
    /// </summary>
    public class StringArgument : CommandLineArgument<string>
    {
        public override string Eval()
        {
            return StringValue;
        }

        protected override string DefaultValue { get => String.Empty; }
    }
}