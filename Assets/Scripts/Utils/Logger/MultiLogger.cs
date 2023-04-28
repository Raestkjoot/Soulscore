using System.Collections.Generic;

public class MultiLogger : ILogr
{
    private readonly List<ILogr> loggers = new List<ILogr>();

    public void AddLogger(ILogr logger)
    {
        loggers.Add(logger);
    }

    public void Log(string message, LogLevel level)
    {
        foreach (var logger in loggers)
        {
            logger.Log(message, level);
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
}
