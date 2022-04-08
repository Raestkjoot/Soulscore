using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerGameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        // Variables to control the gameplay
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float DashSpeed { get; private set; }
        [field: SerializeField] public float DashDuration { get; private set; }
        [field: SerializeField] public float AttackDuration { get; private set; }
        [field: SerializeField] public float AttackHitDelay { get; private set; }
        [field: SerializeField] public float AttackDamage { get; private set; }
        [field: SerializeField] public float AttackingMovementSpeed { get; private set; }

        public bool IsDashing { get; set; }

        // States
        public MoveState moveState;
        public DashState dashState;
        public IdleState idleState;
        public AttackState attackState;

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private StateMachine _movementStateMachine;
        private StateMachine _actionStateMachine;
        private PlayerInputHandler _inputHandler;
        private string _currentAnimState;

        private Vector2 _moveDirection;
        private Vector2 _dashDir;
        private float _curMoveSpeed;

        /// <summary>
        /// Change the player's current animation state to some specific animation state.
        /// </summary>
        /// <param name="newState"> The name of the animation state we want to change to. </param>
        public void ChangeAnimationState(string newState)
        {
            if (_currentAnimState == newState) return;

            _animator.Play(newState);

            _currentAnimState = newState;
        }

        public void SetSpeed(float speed)
        {
            _curMoveSpeed = speed;
        }

        /// <summary>
        /// Move the player according to the direction vector and speed.
        /// </summary>
        /// <param name="direction"> direction of the move. </param>
        /// <param name="speed"> the speed of the move. </param>
        /// <remarks>
        /// Be sure to call this inside the FixedUpdate / PhysicsUpdate to avoid stuttering.
        /// </remarks>
        private void Move(Vector2 direction, float speed)
        {
            _rigidbody.MovePosition(_rigidbody.position + direction.normalized * speed * Time.fixedDeltaTime);
        }

        private void Awake()
        {
            if (MovementSpeed == 0)
                Debug.LogWarning("Movement speed is set to 0, the player will not move. Remember to set stats in the inspector.");
            else
                _curMoveSpeed = MovementSpeed;
        }

        private void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponent<Animator>();

            _movementStateMachine = new StateMachine();
            _actionStateMachine = new StateMachine();
            _inputHandler = gameObject.AddComponent<PlayerInputHandler>();

            moveState = new MoveState(this, _movementStateMachine, _inputHandler);
            dashState = new DashState(this, _movementStateMachine, _inputHandler);

            idleState = new IdleState(this, _actionStateMachine, _inputHandler);
            attackState = new AttackState(this, _actionStateMachine, _inputHandler);
            //TODO: abilityState = new AbilityState(this, _actionStateMachine, _inputHandler);

            //Callbacks
            _movementStateMachine.Initialize(moveState);
            _actionStateMachine.Initialize(idleState);
        }

        private void Update()
        {
            _moveDirection = _inputHandler.GetMoveDirection();

            _movementStateMachine.CurrentState.HandleInput();
            _movementStateMachine.CurrentState.LogicUpdate();

            _actionStateMachine.CurrentState.HandleInput();
            _actionStateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            if (IsDashing)
            {
                Move(_dashDir, DashSpeed);
            }
            else if (_moveDirection != Vector2.zero)
            {
                Move(_moveDirection, _curMoveSpeed);
                _dashDir = _moveDirection;
            }

            _movementStateMachine.CurrentState.PhysicsUpdate();
            _actionStateMachine.CurrentState.PhysicsUpdate();

        }
    }
}
