using UnityEngine;
using Common;

public class LogTester : MonoBehaviour
{
    private ILogr _logger;

    private void Awake()
    {
        _logger = new ConsoleLogger("TestLogger");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Debug.Log("Debug.Log");
            //Debug.unityLogger.LogFormat(LogType.Warning, "unityLogger.Log");


            _logger.Trace("testing");

            ScreenLogText.Log("screen text test", 3.0f);
        }
    }
}