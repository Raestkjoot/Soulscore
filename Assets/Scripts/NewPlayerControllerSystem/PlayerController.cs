using Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
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

        // TODO: This is just for testing.
        //       The call to possess should be moved to a GameHandler
        Pawn testPawn = gameObject.GetComponent<Pawn>();
        Possess(testPawn);
    }

    private void FixedUpdate()
    {
        _pawn.Move(_moveDirection);
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