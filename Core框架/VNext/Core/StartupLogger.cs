using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// 系统启动日志-记录器
    /// </summary>
    public class StartupLogger
    {
        public IList<LogRecord> LogRecords { get; } = new List<LogRecord>();

        /// <summary>
        ///  记录Debug日志信息
        /// </summary>
        public void LogDebug(string logName, string message)
        {
            Log(logName, LogLevel.Debug, message);
        }

        /// <summary>
        /// 记录Info日志信息
        /// </summary>
        public void LogInfo(string logName, string message)
        {
            Log(logName, LogLevel.Information, message);
        }

        /// <summary>
        /// 记录错误日志信息
        /// </summary>
        public void LogError(string logName, string message, Exception exception = null)
        {
            Log(logName, LogLevel.Error, message, exception);
        }

        public void Output(IServiceProvider provider)
        {
            IDictionary<string, ILogger> dict = new Dictionary<string, ILogger>();
            foreach (LogRecord info in LogRecords.OrderBy(m => m.CreatedTime))
            {
                if (!dict.TryGetValue(info.LogName, out ILogger logger))
                {
                    logger = provider.GetLogger(info.LogName);
                    dict[info.LogName] = logger;
                }

                switch (info.LogLevel)
                {
                    case LogLevel.Trace:
                        logger.LogTrace(info.Message);
                        break;
                    case LogLevel.Debug:
                        logger.LogDebug(info.Message);
                        break;
                    case LogLevel.Information:
                        logger.LogInformation(info.Message);
                        break;
                    case LogLevel.Warning:
                        logger.LogWarning(info.Message);
                        break;
                    case LogLevel.Error:
                        logger.LogError(info.Exception, info.Message);
                        break;
                    case LogLevel.Critical:
                        logger.LogCritical(info.Exception, info.Message);
                        break;
                }
            }
        }

        private void Log(string logName, LogLevel logLevel, string message, Exception exception = null)
        {
            LogRecord log = new LogRecord() { LogName = logName, LogLevel = logLevel, Message = message, Exception = exception, CreatedTime = DateTime.Now };
            LogRecords.Add(log);
        }

        /// <summary>
        /// 日志信息明细
        /// </summary>
        public class LogRecord
        {
            /// <summary>
            /// 日志Log名称
            /// </summary>
            public string LogName { get; set; }

            /// <summary>
            /// 日志登记
            /// </summary>
            public LogLevel LogLevel { get; set; }

            /// <summary>
            /// 消息
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// 异常信息
            /// </summary>
            public Exception Exception { get; set; }

            /// <summary>
            /// 记录日期
            /// </summary>
            public DateTime CreatedTime { get; set; }
        }
    }
}