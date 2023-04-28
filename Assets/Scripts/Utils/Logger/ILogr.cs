public interface ILogr
{
    public void Log(string message, LogLevel level);

    public void Log(string message);

    public void Trace(string message);

    public void Debug(string message);

    public void Info(string message);

    public void Warn(string message);

    public void Error(string message);

    public void Critical(string message);
}