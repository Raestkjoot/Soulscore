using AbilitySystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject StartingUnit;

    private Unit _curUnit;

    private PlayerControls _playerControls;
    private InputAction _move;
    private InputAction _attack;
    private InputAction _dash;
    private InputAction _ability1;
    private InputAction _ability2;
    private InputAction _interact;

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

    private void Attack(InputAction.CallbackContext context)
    {
        _logger.Trace("Attack");
        _curUnit.TryActivateAbility((int)AbilityType.BasicAttack);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        _logger.Trace("Dash");
        _curUnit.TryActivateAbility((int)AbilityType.DashAbility);
    }

    private void Ability1(InputAction.CallbackContext context)
    {
        _logger.Trace("Ability1");
        _curUnit.TryActivateAbility((int)AbilityType.SpecialAbility1);
    }

    private void Ability2(InputAction.CallbackContext context)
    {
        _logger.Trace("Ability2");
        _curUnit.TryActivateAbility((int)AbilityType.SpecialAbility2);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        _logger.Trace("Interact");
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

        _attack = _playerControls.Player.Attack;
        _attack.Enable();
        _attack.performed += Attack;

        _dash = _playerControls.Player.Dash;
        _dash.Enable();
        _dash.performed += Dash;

        _ability1 = _playerControls.Player.Ability1;
        _ability1.Enable();
        _ability1.performed += Ability1;

        _ability2 = _playerControls.Player.Ability2;
        _ability2.Enable();
        _ability2.performed += Ability2;

        _interact = _playerControls.Player.Interact;
        _interact.Enable();
        _interact.performed += Interact;
    }
    private void OnDisable()
    {
        _move.Disable();
        _dash.Disable();
    }
}