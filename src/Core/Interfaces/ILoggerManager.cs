using System;
namespace Core.Interfaces
{
    /// <summary>
    /// Logger manager
    /// </summary>
    public interface ILoggerManager
    {
        /// <summary>
        /// Logger Information
        /// </summary>
        /// <param name="message">Logger information message</param>
        void LogInfo(string message);

        /// <summary>
        /// Logger Warn
        /// </summary>
        /// <param name="message">Logger warn message</param>
        void LogWarn(string message);

        /// <summary>
        /// Logger Debug
        /// </summary>
        /// <param name="message">Logger debug message</param>
        void LogDebug(string message);

        /// <summary>
        /// Logger Error
        /// </summary>
        /// <param name="message">Logger error message</param> 
        void LogError(string message);
    }
}

