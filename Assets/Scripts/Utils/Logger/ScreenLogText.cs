using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

// TODO: add text into the scene. maybe modify awake function, take insp from state machine

public class ScreenLogText : Singleton<ScreenLogText>
{
    [SerializeField] private GameObject textPrefab;

    private Text logText;
    private int maxLogLines = 20;

    public static void Log(string message, float duration) =>
        Instance.AppendLog(message, duration);

    public static void Log(string message) => 
        Instance.AppendLog(message);

    private async void AppendLog(string message, float duration = 3.0f)
    {

        if (logText.text.Length > 0)
        {
            logText.text += "\n";
        }

        logText.text += message;

        // Trim the log text if it gets too long
        string[] lines = logText.text.Split('\n');
        if (lines.Length > maxLogLines)
        {
            logText.text = string.Join("\n", lines, lines.Length - maxLogLines, maxLogLines);
        }

        await Task.Delay((int)(duration * 1000));
        RemoveLog(message);
    }

    private void RemoveLog(string message)
    {
        string newText = logText.text.Replace(message, "");
        logText.text = newText.Trim('\n');
    }

}
