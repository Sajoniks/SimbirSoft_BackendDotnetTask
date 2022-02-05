using System;
using NLog;

namespace TestTask_SimbirSoft.Stat
{
    /// <summary>
    /// Simple class that can be used for task time consummation tracking.
    /// Must be correctly disposed via 'using' construction in order to work properly.
    /// </summary>
    public class ScopedStatCounter : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private readonly DateTime _scopeStart;    // When the scope was created
        private readonly string _scopeCtx;        // Task name for human-readability purposes
        
        /// <summary>
        /// Create scope counter.
        /// </summary>
        /// <remarks>Must be disposed correctly in order to work properly</remarks>
        /// <param name="scopeCtx">Name of the task context. Usually it is just a human readable name.</param>
        public ScopedStatCounter(string scopeCtx)
        {
            _scopeCtx = scopeCtx;
            _scopeStart = DateTime.UtcNow;
        }
        
        public void Dispose()
        {
            // Calculate time and push to log
            var scopeEnd = DateTime.UtcNow;
            Logger.Info($"Task \"{_scopeCtx}\" done. Took {(scopeEnd - _scopeStart).TotalMilliseconds} ms.");
        }
    }
}