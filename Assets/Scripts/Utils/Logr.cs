using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common
{
    public class Logr
    {
        private Dictionary<string, StreamWriter> fileWriters = new Dictionary<string, StreamWriter>();
        private string loggerName;

        public Logr(string loggerName = "default")
        {
            this.loggerName = loggerName;
        }

        public void Log(string message, LogLevel level = LogLevel.Info)
        {
            // string logLine = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] [{loggerName}] [{level}] {message}";
            string logLine = $"[{loggerName}] [{level}] {message}";


            switch (level)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                case LogLevel.Info:
                    UnityEngine.Debug.Log(logLine);
                    break;
                case LogLevel.Warn:
                    UnityEngine.Debug.LogWarning(logLine);
                    break;
                case LogLevel.Error:
                case LogLevel.Critical:
                    UnityEngine.Debug.LogError(logLine);
                    break;
            }

            if (fileWriters.ContainsKey(loggerName))
            {
                fileWriters[loggerName].WriteLine(logLine);
                fileWriters[loggerName].Flush();
            }
        }

        public void Trace(string message)
        {
            Log(message, LogLevel.Trace);
        }

        public void Debug(string message)
        {
            Log(message, LogLevel.Debug);
        }

        public void Info(string message)
        {
            Log(message, LogLevel.Info);
        }

        public void Warn(string message)
        {
            Log(message, LogLevel.Warn);
        }

        public void Error(string message)
        {
            Log(message, LogLevel.Error);
        }

        public void Critical(string message)
        {
            Log(message, LogLevel.Critical);
        }

        public void RegisterFileLogger(string filePath)
        {
            if (!fileWriters.ContainsKey(loggerName))
            {
                StreamWriter fileWriter = new StreamWriter(filePath, true);
                fileWriters.Add(loggerName, fileWriter);
            }
        }

        public void UnregisterFileLogger()
        {
            if (fileWriters.ContainsKey(loggerName))
            {
                fileWriters[loggerName].Close();
                fileWriters.Remove(loggerName);
            }
        }
    }

    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Critical
    }
}