namespace TestTask_SimbirSoft
{
    /// <summary>
    /// Value of the command line argument
    /// </summary>
    public abstract class CommandLineArgument
    {
        private string _val;    // raw string value
        
        /// <summary>
        /// Set value of the argument
        /// </summary>
        public string Value
        {
            set { _val = value ?? DefaultValue; }
        }
        
        /// <summary>
        /// Get current string value of the argument
        /// </summary>
        protected string StringValue
        {
            get { return _val; }
        }

        /// <summary>
        /// Default value of the argument. Used as fallback when null or invalid value was passed.
        /// </summary>
        protected abstract string DefaultValue { get; }
    }
    
    /// <summary>
    /// Value converter of the command line argument value
    /// </summary>
    /// <typeparam name="T">Type to convert into</typeparam>
    public abstract class CommandLineArgument<T> : CommandLineArgument
    {
        /// <summary>
        /// Evaluates value into T
        /// </summary>
        /// <returns>Argument value as T</returns>
        public abstract T Eval();

        /// <summary>
        /// Implicit conversion into T
        /// </summary>
        /// <param name="x">Argument to convert</param>
        /// <returns>Converted argument</returns>
        public static implicit operator T(CommandLineArgument<T> x)
        {
            return x.Eval();
        }
    }
}