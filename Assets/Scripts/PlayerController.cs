using AbilitySystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject StartingUnit;

    private Unit _curUnit;
    private AbilityCaster _abilityCaster;

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
        if (!newUnit.TryGetComponent<AbilityCaster>(out _abilityCaster))
        {
            _logger.Info("No ability caster on Unit");
        }

        _curUnit = newUnit;
        _logger.Trace("possessed " + _curUnit);
    }

    public void Possess(GameObject newUnit)
    {
        if (newUnit.TryGetComponent(out Unit unit))
        {
            Possess(unit);
        }
        else
        {
            _logger.Error("No Unit component on new unit gameObject: " + newUnit.name);
        }
    }

    private void AttackActivate(InputAction.CallbackContext context) =>
        _abilityCaster.TryActivateAbility((int)AbilityType.BasicAttack);
    private void AttackRelease(InputAction.CallbackContext context) =>
        _abilityCaster.ReleaseAbility((int)AbilityType.BasicAttack);

    private void DashActivate(InputAction.CallbackContext context) =>
        _abilityCaster.TryActivateAbility((int)AbilityType.DashAbility);
    private void DashReleased(InputAction.CallbackContext context) =>
        _abilityCaster.ReleaseAbility((int)AbilityType.DashAbility);

    private void Ability1Activate(InputAction.CallbackContext context) =>
        _abilityCaster.TryActivateAbility((int)AbilityType.SpecialAbility1);
    private void Ability1Execute(InputAction.CallbackContext context) =>
        _abilityCaster.ReleaseAbility((int)AbilityType.SpecialAbility1);

    private void Ability2Activate(InputAction.CallbackContext context) =>
        _abilityCaster.TryActivateAbility((int)AbilityType.SpecialAbility2);
    private void Ability2Execute(InputAction.CallbackContext context) =>
        _abilityCaster.ReleaseAbility((int)AbilityType.SpecialAbility2);

    private void InteractActivate(InputAction.CallbackContext context) =>
        _abilityCaster.TryActivateAbility((int)AbilityType.Interact);
    private void InteractReleased(InputAction.CallbackContext context) =>
        _abilityCaster.ReleaseAbility((int)AbilityType.Interact);


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
        _attack.performed += AttackActivate;
        _attack.canceled += AttackRelease;

        _dash = _playerControls.Player.Dash;
        _dash.Enable();
        _dash.performed += DashActivate;
        _dash.canceled += DashReleased;

        _ability1 = _playerControls.Player.Ability1;
        _ability1.Enable();
        _ability1.performed += Ability1Activate;
        _ability1.canceled += Ability1Execute;

        _ability2 = _playerControls.Player.Ability2;
        _ability2.Enable();
        _ability2.performed += Ability2Activate;
        _ability2.canceled += Ability2Execute;

        _interact = _playerControls.Player.Interact;
        _interact.Enable();
        _interact.performed += InteractActivate;
        _interact.canceled += InteractReleased;
    }

    private void OnDisable()
    {
        _move.Disable();
        _dash.Disable();
    }
}