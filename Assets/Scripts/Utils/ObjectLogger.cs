
// Based on https://github.com/hk1ll3r/UnityObjectLoggerExt

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

namespace Common
{
    // Two classes in a single script, isn't that heathenism? Maybe make a separate loggerConfig script?
    public class LoggerConfig
	{
		public string className;
		public bool logClassName;
		public bool logGameObjectName;
		public bool logThreadId;
		public bool logGameTime;

		public LoggerConfig(string className, bool logClassName = true, bool logGameObjectName = true, bool logThreadId = true, bool logGameTime = true)
		{
			this.className = className;
			this.logClassName = logClassName;
			this.logGameObjectName = logGameObjectName;
			this.logThreadId = logThreadId;
			this.logGameTime = logGameTime;
		}
	}

	public static class ObjectLogger
	{
		public static readonly Dictionary<Type, (Logger, LoggerConfig)> loggers = new Dictionary<Type, (Logger, LoggerConfig)>();

		private static void Log(UnityEngine.Object unityObj, LogType logType, string format, params object[] args)
		{
            (Logger logger, LoggerConfig lc) = GetLoggerByType(unityObj.GetType());
            logger.LogFormat(logType, unityObj, string.Format("{0}{1}{2}{3}{4}",
                lc.logThreadId ? "[" + Thread.CurrentThread.ManagedThreadId.ToString() + "] " : "",
                lc.logGameTime ? "[" + Time.time + "]" : "",
                lc.logClassName ? "(" + lc.className + ")" : "",
                lc.logGameObjectName ? "(" + unityObj.name + ") " : "",
                format),
                args);
        }

        public static void LogLog(this UnityEngine.Object unityObj, string format, params object[] args)
        {
            Log(unityObj, LogType.Log, format, args);
        }
        public static void LogWarn(this UnityEngine.Object unityObj, string format, params object[] args)
        {
            Log(unityObj, LogType.Warning, format, args);
        }
        public static void LogError(this UnityEngine.Object unityObj, string format, params object[] args)
        {
            Log(unityObj, LogType.Error, format, args);
        }

        private static void AddType(Type type)
        {
            if (!loggers.ContainsKey(type))
            {
                LoggerConfig lc = new LoggerConfig(string.Format("{0}", type.Name));
                loggers.Add(type, (new Logger(Debug.unityLogger.logHandler), lc));
            }
        }

        public static (Logger logger, LoggerConfig loggerConfig) GetLoggerByType(Type type)
        {
            AddType(type);
            return loggers[type];
        }

        public static (Logger logger, LoggerConfig loggerConfig) GetLoggerByType<T>()
        {
            return GetLoggerByType(typeof(T));
        }

        private static void LogStatic<T>(LogType logType, string format, params object[] args)
        {
            (Logger logger, LoggerConfig lc) = GetLoggerByType<T>();
            logger.LogFormat(logType, string.Format("{0}{1}{2}{3}{4}",
                lc.logThreadId ? "[" + Thread.CurrentThread.ManagedThreadId.ToString() + "] " : "",
                lc.logGameTime ? "[" + string.Format("{0:0.00}", Time.time) + "]" : "",
                lc.logClassName ? "(" + lc.className + ")" : "",
                lc.logGameObjectName ? "(<static>) " : "",
                format),
                args);
        }

        public static void LogLog<T>(string format, params object[] args)
        {
            LogStatic<T>(LogType.Log, format, args);
        }

        public static void LogWarn<T>(string format, params object[] args)
        {
            LogStatic<T>(LogType.Warning, format, args);
        }

        public static void LogError<T>(string format, params object[] args)
        {
            LogStatic<T>(LogType.Error, format, args);
        }
    }
}