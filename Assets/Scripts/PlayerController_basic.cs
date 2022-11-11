using UnityEngine;
using UnityEngine.InputSystem;
using Common;

namespace PlayerGameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController_basic : MonoBehaviour
    {
        // Variables that control the gameplay
        [field: SerializeField] public float MovementSpeed { get; private set; }

        private PlayerControls _playerControls;
        private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;
        private float _curMoveSpeed;

        /// <summary>
        /// Move the player according to the direction vector.
        /// </summary>
        /// <param name="direction"> direction of the move. </param>
        /// <remarks>
        /// Be sure to call this inside the FixedUpdate to avoid stuttering.
        /// </remarks>
        private void Move(Vector2 direction)
        {
            _rigidbody.MovePosition(_rigidbody.position + 
                                    direction.normalized * 
                                    _curMoveSpeed * 
                                    Time.fixedDeltaTime);
        }

        private void Awake()
        {
            _playerControls = new PlayerControls();
            // Set move direction using "Move" input action.
            _playerControls.Player.Move.performed += ctx =>
                _moveDirection = ctx.ReadValue<Vector2>();
            _playerControls.Player.Move.canceled += ctx =>
                _moveDirection = Vector2.zero;

            _rigidbody = GetComponent<Rigidbody2D>();

            if (MovementSpeed == 0)
                this.LogLog("Movement speed is set to 0, the player will not move. Remember to set stats in the inspector.");
            else
                _curMoveSpeed = MovementSpeed;
        }

        private void OnEnable()
        {
            _playerControls.Player.Enable();
        }
        private void OnDisable()
        {
            _playerControls.Player.Disable();
        }

        private void FixedUpdate()
        {
            Move(_moveDirection);
        }
    } 
}