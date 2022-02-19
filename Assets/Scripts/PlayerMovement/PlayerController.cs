using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerGameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        // Variables to control the gameplay
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float DashSpeed { get; private set; }
        [field: SerializeField] public float DashDuration { get; private set; }
        [field: SerializeField] public float AttackDuration { get; private set; }
        [field: SerializeField] public float AttackHitDelay { get; private set; }
        [field: SerializeField] public float AttackDamage { get; private set; }
        [field: SerializeField] public float AttackingMovementSpeed { get; private set; }

        public StateMachine stateMachine;
        public PlayerInputHandler inputHandler;

        // States
        public MoveAndIdleState moveAndIdleState;
        public DashState dashState;
        public AttackState attackState;

        private string currentAnimState;

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
            if (currentAnimState == newState) return;

            _animator.Play(newState);

            currentAnimState = newState;
        }

        private void Awake()
        {
            if (MovementSpeed == 0)
                Debug.Log("WARNING! Movement speed is set to 0, the player will not move. Remember to set stats in the inspector.");
        }

        #region State Callbacks
        private void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponent<Animator>();

            stateMachine = new StateMachine();
            inputHandler = gameObject.AddComponent<PlayerInputHandler>();

            moveAndIdleState = new MoveAndIdleState(this, stateMachine, inputHandler);
            dashState = new DashState(this, stateMachine, inputHandler);
            attackState = new AttackState(this, stateMachine, inputHandler);

            stateMachine.Initialize(moveAndIdleState);
        }

        private void Update()
        {
            stateMachine.CurrentState.HandleInput();

            stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            stateMachine.CurrentState.PhysicsUpdate();
        }
        #endregion
    }
}
