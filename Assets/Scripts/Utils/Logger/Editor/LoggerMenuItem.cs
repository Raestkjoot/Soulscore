using UnityEngine;
using UnityEditor;

public class LoggerMenuItem
{
    private static LogLevel _logLevel;

    [MenuItem("Tools/SetMinLoggerLevel/Trace")]
    private static void SetMinLoggerLevelToTrace() =>
        SetMinLoggerLevel(LogLevel.Trace);

    [MenuItem("Tools/SetMinLoggerLevel/Debug")]
    private static void SetMinLoggerLevelToDebug() =>
        SetMinLoggerLevel(LogLevel.Debug);

    [MenuItem("Tools/SetMinLoggerLevel/Info")]
    private static void SetMinLoggerLevelToInfo() =>
        SetMinLoggerLevel(LogLevel.Info);

    [MenuItem("Tools/SetMinLoggerLevel/Warn")]
    private static void SetMinLoggerLevelToWarn() =>
        SetMinLoggerLevel(LogLevel.Warn);

    [MenuItem("Tools/SetMinLoggerLevel/Error")]
    private static void SetMinLoggerLevelToError() =>
        SetMinLoggerLevel(LogLevel.Error);

    [MenuItem("Tools/SetMinLoggerLevel/Critical")]
    private static void SetMinLoggerLevelToCritical() =>
        SetMinLoggerLevel(LogLevel.Critical);

    private static void SetMinLoggerLevel(LogLevel logLevel)
    {
        _logLevel = logLevel;
        EditorPrefs.SetInt("MinLogLevel", (int)_logLevel);
        Menu.SetChecked("Tools/SetMinLoggerLevel/Trace", LogLevel.Trace == _logLevel);
        Menu.SetChecked("Tools/SetMinLoggerLevel/Debug", LogLevel.Debug == _logLevel);
        Menu.SetChecked("Tools/SetMinLoggerLevel/Info", LogLevel.Info == _logLevel);
        Menu.SetChecked("Tools/SetMinLoggerLevel/Warn", LogLevel.Warn == _logLevel);
        Menu.SetChecked("Tools/SetMinLoggerLevel/Error", LogLevel.Error == _logLevel);
        Menu.SetChecked("Tools/SetMinLoggerLevel/Critical", LogLevel.Critical == _logLevel);
    }
}