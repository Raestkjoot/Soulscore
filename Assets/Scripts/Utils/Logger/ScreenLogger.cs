using UnityEditor;
using UnityEngine;

public class ScreenLogger : ILogr
{
    private string _loggerName;

    public ScreenLogger (string loggerName = "default")
    {
        _loggerName = loggerName;
    }

    public void Log(string message, LogLevel level)
    {
        if ((int)level < EditorPrefs.GetInt("MinLogLevel"))
            return;

        string logLine = $"[{_loggerName}] [{level}] {message}";

        switch (level)
        {
            case LogLevel.Trace:
            case LogLevel.Debug:
            case LogLevel.Info:
                ScreenLogText.Log(logLine, Color.white);
                break;
            case LogLevel.Warn:
                ScreenLogText.Log(logLine, Color.yellow);
                break;
            case LogLevel.Error:
            case LogLevel.Critical:
                ScreenLogText.Log(logLine, Color.red);
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