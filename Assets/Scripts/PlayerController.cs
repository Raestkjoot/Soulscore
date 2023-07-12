using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject StartingUnit;

    private Unit _curUnit;

    private PlayerControls _playerControls;
    private InputAction _move;
    private InputAction _dash;

    private ILogr _logger;

    public void Possess(Unit newUnit)
    {
        if (newUnit == null)
        {
            _logger.Debug("Tried to possess " + newUnit);
            _logger.Error("possessed null");
            return;
        }

        _curUnit = newUnit;
        _logger.Trace("possessed " + _curUnit);
    }

    public void Possess(GameObject newUnit)
    {
        if (StartingUnit.TryGetComponent(out Unit unit))
        {
            Possess(unit);
        }
        else
        {
            _logger.Error("No Unit component on new unit gameObject: " + newUnit.name);
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        _logger.Trace("Dash");
        // TODO: Dash Ability
        //_curUnit?.ApplyForceInMoveDirection(15f, .2f, 15f);
        _curUnit.TryActivateAbility((int)AbilityType.DashAbility);
    }

    private void Awake()
    {
        _logger = new ConsoleLogger();
        _playerControls = new PlayerControls();

        // TODO: The call to possess should probably be moved to a GameHandler
        Possess(StartingUnit);
    }

    private void FixedUpdate()
    {
        _curUnit?.Move(_move.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _move = _playerControls.Player.Move;
        _move.Enable();

        _dash = _playerControls.Player.Dash;
        _dash.Enable();
        _dash.performed += Dash;
    }
    private void OnDisable()
    {
        _move.Disable();
        _dash.Disable();
    }
}