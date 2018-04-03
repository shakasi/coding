using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using log4net;

namespace WinformTest
{
    /// <summary>
    /// 封装 log4net 测试OK，行号和方法名要自己获得
    /// </summary>
    public class LogDynamicHelper
    {
        private volatile static LogDynamicHelper _instance = null;
        private static readonly object _lockHelper = new object();

        private string _logFile = string.Empty;
        private log4net.Repository.ILoggerRepository _repository = null;
        private log4net.Appender.RollingFileAppender _rollingFileAppender = null;

        private log4net.Layout.PatternLayout _layout = null;

        private LogDynamicHelper()
        {
            string logPath = ConfigurationManager.AppSettings["logPath"];
            string logFileName = ConfigurationManager.AppSettings["logFileName"];
            logPath = logPath.Trim().TrimEnd('\\').TrimEnd('/');
            logFileName = logFileName.Trim().TrimEnd('\\').TrimEnd('/');

            if (!Directory.Exists(logPath))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(logPath);
            }
            _logFile = logPath + '\\' + logFileName;

            //RollingFileAppender
            _rollingFileAppender = new log4net.Appender.RollingFileAppender();
            _rollingFileAppender.AppendToFile = true;
            _rollingFileAppender.ImmediateFlush = true;
            _rollingFileAppender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
            _rollingFileAppender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Composite;
            _rollingFileAppender.MaxSizeRollBackups = 50;
            _rollingFileAppender.MaximumFileSize = "1MB";
            _rollingFileAppender.Encoding = Encoding.UTF8;

            _repository = log4net.LogManager.CreateRepository("MyRepository");

            string patternLayout = null;
            //patternLayout = "%date [%thread] %-5level% [%l]-[%m]newline%message%newline";
            patternLayout = "[%thread]%newline%message%newline";
            _layout = new log4net.Layout.PatternLayout(patternLayout);
        }

        public static LogDynamicHelper CreateInstance()
        {
            if (_instance == null)
            {
                lock (_lockHelper)
                {
                    if (_instance == null)
                    {
                        _instance = new LogDynamicHelper();
                    }
                }
            }
            return _instance;
        }

        //string logSource
        public void WriteLog(Exception ex)
        {
            WriteLog(LogLevel.Exception, LogSeparator.CreateInstance("\r\n"), "", ex.Message);
        }

        public void WriteLog(LogLevel level, object[] param)
        {
            WriteLog(level, LogSeparator.CreateInstance("\r\n"), "", param);
        }

        public void WriteLog(LogLevel level, string remark, object[] param)
        {
            WriteLog(level, LogSeparator.CreateInstance("\r\n"), remark, param);
        }

        public void WriteLog(LogLevel level, LogSeparator separator, object[] messages)
        {
            WriteLog(level, separator, "", messages);
        }

        public void WriteLog(LogLevel level, LogSeparator separator, string remark, params object[] messages)
        {
            lock (_lockHelper)
            {
                string fileName = null;
                if (remark == null)
                {
                    remark = "Null";
                }
                if (remark == "")
                {
                    fileName = _logFile + ".{0}." + DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".log";
                }
                else
                {
                    fileName = _logFile + ".{0}." + remark + '.' + DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".log";
                }

                StringBuilder msgSB = new StringBuilder();
                foreach (object obj in messages)
                {
                    if (obj == null)
                    {
                        continue;
                    }
                    msgSB.Append(obj.ToString() + separator.CurrentSeparator);
                }

                if (level == LogLevel.Exception)
                {
                    WriteLogger("Exception", fileName, msgSB);
                }
                else if (level == LogLevel.Error)
                {
                    WriteLogger("Error", fileName, msgSB);
                }
                else if (level == LogLevel.Warning)
                {
                    WriteLogger("Warning", fileName, msgSB);
                }
                else if (level == LogLevel.Information)
                {
                    WriteLogger("Information", fileName, msgSB);
                }
            }
        }

        private void WriteLogger(string logType, string fileName, StringBuilder msgSB)
        {
            ILog logger = log4net.LogManager.GetLogger(_repository.Name, "MyLog");
            string logHeader = "== {0} ====================== " + DateTime.Now + Environment.NewLine;
            _rollingFileAppender.File = string.Format(fileName, logType);
            _rollingFileAppender.AppendToFile = true;
            logHeader = string.Format(logHeader, logType);
            _layout.Header = logHeader;
            _rollingFileAppender.Layout = _layout;
            //如果设置Appender使用了静态文件名，那么历史日志会被覆盖
            ///_rollingFileAppender.StaticLogFileName = false;
            _rollingFileAppender.ActivateOptions();
            _repository.Threshold = log4net.Core.Level.Info;
            log4net.Config.BasicConfigurator.Configure(_repository, _rollingFileAppender);
            logger.Info(msgSB.ToString().TrimEnd(new char[] { '\r', '\n' }));
        }
    }

    public class LogSeparator
    {
        // Methods
        public LogSeparator(string separator)
        {
            this.CurrentSeparator = separator;
        }

        public static LogSeparator CreateInstance(string separator)
        {
            return new LogSeparator(separator);
        }

        // Properties
        public string CurrentSeparator { get; set; }
    }

    public enum LogLevel
    {
        Exception = 0,
        Error = 1,
        Warning = 2,
        Information = 3
    }
}