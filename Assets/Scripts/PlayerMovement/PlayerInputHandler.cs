using UnityEngine;

namespace PlayerGameplay
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private Vector2 _moveDirection;
        private Vector2 _aimDirection;
        private Action _actionInput = Action.None;
        private Camera _cam;
        private bool _isUsingController;

        public Vector2 GetMoveDirection()
        {
            return _moveDirection;
        }

        public Vector2 GetAimDirection()
        {
            return _aimDirection;
        }

        public Action GetActionInput()
        {
            return _actionInput;
        }

        private void Start()
        {
            _cam = GameObject.Find("Camera").GetComponent<Camera>();

            if (!_cam)
                Debug.LogWarning("Camera not found!");
        }

        private void Update()
        {
            // TODO: listen for controller/PC input and change isUsingController variable based on this
            if (Input.GetKeyUp(KeyCode.C))
            {
                _isUsingController = !_isUsingController;
                Debug.Log("Is using controller = " + _isUsingController);
            }
                

            // Get move direction
            _moveDirection.x = Input.GetAxisRaw("Horizontal");
            _moveDirection.y = Input.GetAxisRaw("Vertical");

            // Get aim direction
            if (_isUsingController) 
            {
                _aimDirection = _moveDirection;
            }
            else
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 1.0f;
                Vector2 mouseOnScreenPosition = _cam.ScreenToWorldPoint(mousePosition);
                Debug.Log("input mouse pos  = " + Input.mousePosition);
                Debug.Log("screen mouse pos = " + _cam.ScreenToWorldPoint(Input.mousePosition));
                Debug.Log("mouse pos        = " + mousePosition);
                Debug.Log("transform pos    = " + transform.position);
                _aimDirection = (mouseOnScreenPosition -
                                    new Vector2(
                                        transform.position.x,
                                        transform.position.y))
                                    .normalized;
            }

            // Get action input
            if (Input.GetButtonDown("Dash")) { _actionInput = Action.Dash; }
            else if (Input.GetButtonDown("Attack")) { _actionInput = Action.Attack; }
            else { _actionInput = Action.None; }

        }
    }
}