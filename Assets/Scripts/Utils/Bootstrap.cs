using UnityEngine;

public delegate void BootstrapEventHandler();
public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);

        GameHandler gameHandler = new GameHandler();
        gameHandler.Initialize(this);
        ServiceLocator.SetGameHandler(gameHandler);
    }

    /* TODO: Get GameHandler
     *   Call level functions when keys:
     *     R - resetMap
     *     N - LoadNextLevel
     *     P - LoadPreviousLevel
     */

#pragma warning disable CS0067
    public event BootstrapEventHandler OnDestroy;
    public event BootstrapEventHandler OnApplicationQuit;
#pragma warning restore CS0067
}