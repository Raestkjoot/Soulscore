using Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject StartingUnit;

    private Unit _curUnit;

    private PlayerControls _playerControls;
    private InputAction _move;
    private InputAction _dash;

    public void Possess(Unit newUnit)
    {
        if (newUnit == null)
        {
            this.LogLog("Tried to possess " + newUnit);
            this.LogError("possessed null");
            return;
        }

        _curUnit = newUnit;
        this.LogLog("possessed " + _curUnit);
    }

    public void Possess(GameObject newUnit)
    {
        if (StartingUnit.TryGetComponent(out Unit unit))
        {
            Possess(unit);
        }
        else
        {
            this.LogError("No Unit component on new unit gameObject: " + newUnit.name);
        }
    }

    private void Dash(InputAction.CallbackContext context)
    {
        this.LogLog("Dash");
    }

    private void Awake()
    {
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