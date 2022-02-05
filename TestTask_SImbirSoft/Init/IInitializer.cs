namespace TestTask_SimbirSoft.Init
{
    /// <summary>
    /// Base interface for objects that can be initialized in some way (for example, configuration objects)
    /// </summary>
    /// <typeparam name="T">Object that can be initialized</typeparam>
    public interface IInitializer<T> where T : class
    {
        /// <summary>
        /// Configure config
        /// </summary>
        /// <param name="config">Configuration object</param>
        /// <returns></returns>
        T Configure(T config);
    }
}