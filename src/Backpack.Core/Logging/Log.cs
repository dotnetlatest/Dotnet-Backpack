using System;
using System.Diagnostics;
using System.IO;
using log4net;
using log4net.Config;

namespace Backpack.Core.Logging
{
    /// <summary>
    /// Wrapper for Log4net
    /// </summary>
    public class Log
    {
        private ILog _log;
        private Log(ILog log)
        {
            _log = log;
        }

        #region Properties
        public bool IsDebugEnabled { get { return _log.IsDebugEnabled; } }
        public bool IsErrorEnabled { get { return _log.IsErrorEnabled; } }
        public bool IsFatalEnabled { get { return _log.IsFatalEnabled; } }
        public bool IsInfoEnabled { get { return _log.IsInfoEnabled; } }
        public bool IsWarnEnabled { get { return _log.IsWarnEnabled; } }
        #endregion


        /// <summary>
        /// Gets an instance of Log4net using stackframe to retrieve class type
        /// </summary>
        /// <returns></returns>
        public static Log Get()
        {
            var frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;
            return Get(type);
        }

        /// <summary>
        /// Gets an instance of Log4net 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Log Get(Type type)
        {
            ILog log4net = LogManager.GetLogger(type);
            return new Log(log4net);
        }

        /// <summary>
        /// Gets an instance of Log4net
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Log Get(string name)
        {
            ILog log4net = LogManager.GetLogger(name);
            return new Log(log4net);
        }

        public static void ConfigureAndWatch(FileInfo info)
        {
            XmlConfigurator.ConfigureAndWatch(info);
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            _log.Debug(message, exception);
        }

        public void DebugFormat(string format, object arg0)
        {
            _log.DebugFormat(format, arg0);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.DebugFormat(provider, format, args);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void ErrorFormat(string format, object arg0)
        {
            _log.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.ErrorFormat(provider, format, args);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            _log.Fatal(message, exception);
        }

        public void FatalFormat(string format, object arg0)
        {
            _log.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.FatalFormat(provider, format, args);
        }

        public void Info(object message)
        {
            _log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            _log.Info(message, exception);
        }

        public void InfoFormat(string format, object arg0)
        {
            _log.InfoFormat(format, arg0);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.InfoFormat(provider, format, args);
        }

        public void Warn(object message)
        {
            _log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        public void WarnFormat(string format, object arg0)
        {
            _log.WarnFormat(format, arg0);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.WarnFormat(provider, format, args);
        }

    }
}
