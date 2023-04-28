using UnityEngine;

public class ConsoleLogger : ILogr
{
    private string _loggerName;

    public ConsoleLogger(string loggerName = "default")
    {
        _loggerName = loggerName;
    }

    public void Log(string message, LogLevel level = LogLevel.Info)
    {
        string logLine = $"[{_loggerName}] [{level}] {message}";


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
    }

    public void Log(string message)
    {
        Log(message, LogLevel.Info);
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
}