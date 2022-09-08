using System;
using Core.Interfaces;
using NLog;

namespace Infrastructure
{
    /// <summary>
    /// Logger manager
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        #region Fields

        private static ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion Fields

        #region Constructor

        public LoggerManager() { }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Logger Information
        /// </summary>
        /// <param name="message">Logger information message</param>
        public void LogInfo(string message) => logger.Info(message);

        /// <summary>
        /// Logger Warn
        /// </summary>
        /// <param name="message">Logger warn message</param>
        public void LogWarn(string message) => logger.Warn(message);

        /// <summary>
        /// Logger Debug
        /// </summary>
        /// <param name="message">Logger debug message</param>
        public void LogDebug(string message) => logger.Debug(message);

        /// <summary>
        /// Logger Error
        /// </summary>
        /// <param name="message">Logger error message</param> 
        public void LogError(string message) => logger.Error(message);

        #endregion Methods
    }
}

