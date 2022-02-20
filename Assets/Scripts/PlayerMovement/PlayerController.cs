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

        // States
        public MoveAndIdleState moveAndIdleState;
        public DashState dashState;
        public AttackState attackState;

        private Rigidbody2D _rigidbody;
        private StateMachine _stateMachine;
        private PlayerInputHandler _inputHandler;
        private Animator _animator;
        private string _currentAnimState;

        /// <summary>
        /// Move the player according to the direction vector and speed.
        /// </summary>
        /// <param name="direction"> direction of the move. </param>
        /// <param name="speed"> the speed of the move. </param>
        /// <remarks>
        /// Be sure to call this inside the FixedUpdate / PhysicsUpdate to avoid stuttering.
        /// </remarks>
        public void Move(Vector2 direction, float speed)
        {
            _rigidbody.MovePosition(_rigidbody.position + direction.normalized * speed * Time.fixedDeltaTime);
        }

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

        private void Awake()
        {
            if (MovementSpeed == 0)
                Debug.LogWarning("Movement speed is set to 0, the player will not move. Remember to set stats in the inspector.");
        }

        #region State Callbacks
        private void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponent<Animator>();

            _stateMachine = new StateMachine();
            _inputHandler = gameObject.AddComponent<PlayerInputHandler>();

            moveAndIdleState = new MoveAndIdleState(this, _stateMachine, _inputHandler);
            dashState = new DashState(this, _stateMachine, _inputHandler);
            attackState = new AttackState(this, _stateMachine, _inputHandler);

            _stateMachine.Initialize(moveAndIdleState);
        }

        private void Update()
        {
            _stateMachine.CurrentState.HandleInput();

            _stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.PhysicsUpdate();
        }
        #endregion
    }
}
