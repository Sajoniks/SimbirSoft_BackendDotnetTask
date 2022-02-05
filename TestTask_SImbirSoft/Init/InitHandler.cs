using System.Diagnostics.CodeAnalysis;

namespace TestTask_SimbirSoft.Init    
{
    /// <summary>
    /// Base class for objects that somehow control object configuration using IInitializer
    /// </summary>
    /// <typeparam name="TBase">Initializer class</typeparam>
    public abstract class InitHandler<TBase>
    {
        /// <summary>
        /// Configure object using configurator
        /// </summary>
        /// <param name="initializer">Configurator object</param>
        public abstract void Configure([NotNull] TBase initializer);
    }
}