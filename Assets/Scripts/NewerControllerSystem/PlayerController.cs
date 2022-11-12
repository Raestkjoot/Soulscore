using Common;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject StartingUnit;

    private Unit _curUnit;
    // TODO: Look into how the inputSystem works and refactor
    private PlayerControls _playerControls;

    private Vector2 _moveDirection;

    public void Possess(Unit newUnit)
    {
        if (newUnit == null)
        {
            this.LogLog("Tried to possess " + newUnit);
            this.LogError("possessed null");
        }
        else
        {
            _curUnit = newUnit;
            this.LogLog("possessed " + _curUnit);
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
        if (StartingUnit.TryGetComponent(out Unit unit))
        {
            Possess(unit);
        }
        else
        {
            this.LogError("No Unit component on StartingUnit GameObject");
        }
        //Possess(StartingUnit);
    }

    private void FixedUpdate()
    {
        _curUnit?.Move(_moveDirection);
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