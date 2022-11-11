using Common;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject DefaultPawn;

    private IPawn _pawn;
    private PlayerControls _playerControls;

    private Vector2 _moveDirection;

    public void Possess(IPawn newPawn)
    {
        if (newPawn == null)
        {
            this.LogError("possessed null");
        }
        else
        {
            _pawn = newPawn;
            this.LogLog("possessed " + _pawn);
        }
    }

    private void Awake()
    {
        _playerControls = new PlayerControls();

        // Set move direction using "Move" input action.
        _playerControls.Player.Move.performed += ctx =>
            _moveDirection = ctx.ReadValue<Vector2>();
        _playerControls.Player.Move.canceled += ctx =>
            _moveDirection = Vector2.zero;

        // TODO: The call to possess should probably be moved to a GameHandler
        //IPawn pawn = DefaultPawn.GetComponent(typeof(IPawn)) as IPawn;

        if (DefaultPawn.TryGetComponent(out IPawn pawn))
        {
            Possess(pawn);
        }
        else
        {
            this.LogError("No component implementing IPawn on DefaultPawn");
        }
        
    }

    private void FixedUpdate()
    {
        _pawn?.Move(_moveDirection);
    }

    private void OnEnable()
    {
        _playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Player.Disable();
    }
}