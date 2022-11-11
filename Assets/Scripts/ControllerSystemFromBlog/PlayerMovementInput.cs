using UnityEngine;
using UnityEngine.Events;


public class PlayerMovementInput : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> onMoveInputUpdate;

    private Vector2 _inputRecord;
    private Vector2 _currentInput;

    private void Update()
    {
        _currentInput.x = UpdateHorizontal();
        _currentInput.y = UpdateVertical();

        if (_currentInput != _inputRecord)
        {
            onMoveInputUpdate.Invoke(_currentInput);
            _inputRecord = _currentInput;
        }
    }

    private sbyte UpdateHorizontal()
    {
        if (Input.GetKey(KeyCode.A)) return -1;
        if (Input.GetKey(KeyCode.D)) return 1;
        return 0;
    }

    private sbyte UpdateVertical()
    {
        if (Input.GetKey(KeyCode.W)) return -1;
        if (Input.GetKey(KeyCode.S)) return 1;
        return 0;
    }
}