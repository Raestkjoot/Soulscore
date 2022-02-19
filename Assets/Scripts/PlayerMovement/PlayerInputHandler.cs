using UnityEngine;

namespace PlayerGameplay
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private Vector2 _moveDirection;
        private Vector2 _aimDirection;
        private Camera cam;
        private bool isUsingController;

        public Vector2 GetMoveDirection()
        {
            return _moveDirection;
        }

        public Vector2 GetAimDirection()
        {
            return _aimDirection;
        }

        private void Start()
        {
            cam = GameObject.Find("Camera").GetComponent<Camera>();
        }

        private void Update()
        {
            // TODO: listen for controller/PC input and change isUsingController variable based on this

            // Get move direction
            _moveDirection.x = Input.GetAxisRaw("Horizontal");
            _moveDirection.y = Input.GetAxisRaw("Vertical");

            // Get aim direction
            if (isUsingController) 
            {
                _aimDirection = _moveDirection;
            }
            else
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                _aimDirection = (mousePosition -
                                    new Vector2(
                                        transform.position.x,
                                        transform.position.y))
                                    .normalized;
            }

            // Get action input
        }
    }
}