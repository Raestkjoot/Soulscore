using UnityEngine;

public class LogTester : MonoBehaviour
{
    private ScreenLogger _logger;
    private int n;

    private void Awake()
    {
        _logger = new ScreenLogger();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Debug.Log("Debug.Log");
            //Debug.unityLogger.LogFormat(LogType.Warning, "unityLogger.Log");


            _logger.Debug("testing");
            _logger.Trace("testing");
            _logger.Info("testing");
            _logger.Warn("testing");
            _logger.Error("testing");
            _logger.Critical("testing");
        }
    }
}