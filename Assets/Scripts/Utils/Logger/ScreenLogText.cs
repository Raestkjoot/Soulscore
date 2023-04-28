using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

public class ScreenLogText : Singleton<ScreenLogText>
{
    private Text _logText;
    private int _maxLogLines = 19;
    private List<string> _logMessages = new List<string>();

    public static void Log(string message, Color color, float duration) =>
        Instance.AppendLog(message, color, duration);

    public static void Log(string message, Color color) => 
        Instance.AppendLog(message, color);

    protected override void Awake()
    {
        base.Awake();

        if (_logText == null)
        {
            InitializeTextObject();
        }
    }

    private async void AppendLog(string message, Color color, float duration = 2.0f)
    {
        if (_logText.text.Length > 0)
        {
            _logText.text += "\n";
        }

        // Add color tag to message
        string colorHex = ColorUtility.ToHtmlStringRGBA(color);
        string colorTag = $"<color=#{colorHex}>";
        string endColorTag = "</color>";
        string formattedMessage = $"{colorTag}{message}{endColorTag}";

        _logMessages.Add(formattedMessage);
        UpdateLogText();

        await Task.Delay((int)(duration * 1000));
        RemoveLog(_logMessages.IndexOf(formattedMessage));
    }

    private void RemoveLog(int index)
    {
        if (index >= 0 && index < _logMessages.Count)
        {
            _logMessages.RemoveAt(index);
            UpdateLogText();
        }
    }

    private void UpdateLogText()
    {
        _logText.text = string.Join("\n", _logMessages.ToArray());

        // Trim the log text if it gets too long
        string[] lines = _logText.text.Split('\n');
        if (lines.Length > _maxLogLines)
        {
            _logMessages.RemoveRange(0, lines.Length - _maxLogLines);
            UpdateLogText();
        }
    }

    private void InitializeTextObject()
    {
        GameObject canvasGO = new GameObject("Canvas_ScreenLogger");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        GameObject textGO = new GameObject("logText");
        textGO.transform.SetParent(canvasGO.transform);
        _logText = textGO.AddComponent<Text>();
        _logText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        _logText.fontSize = 20;
        _logText.color = Color.white;
        _logText.alignment = TextAnchor.UpperLeft;
        _logText.horizontalOverflow = HorizontalWrapMode.Overflow;
        _logText.verticalOverflow = VerticalWrapMode.Overflow;
        RectTransform textTransform = _logText.gameObject.GetComponent<RectTransform>();
        textTransform.anchorMin = new Vector2(0, 1);
        textTransform.anchorMax = new Vector2(0, 1);
        textTransform.pivot = new Vector2(0, 1);
        textTransform.anchoredPosition = Vector2.zero;
    }
}