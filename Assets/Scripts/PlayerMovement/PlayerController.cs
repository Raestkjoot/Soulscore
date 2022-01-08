using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerGameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody => gameObject.GetComponent<Rigidbody2D>();

        // Variables to control the gameplay
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float DashSpeed { get; private set; }
        [field: SerializeField] public float DashDuration { get; private set; }
        [field: SerializeField] public float AttackDuration { get; private set; }
        [field: SerializeField] public float AttackDamage { get; private set; }


        public StateMachine stateMachine;
        // States
        public MoveAndIdleState moveAndIdleState;
        public DashState dashState;
        public AttackState attackState;

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

        #region State Callbacks
        private void Start()
        {
            stateMachine = new StateMachine();

            moveAndIdleState = new MoveAndIdleState(this, stateMachine);
            dashState = new DashState(this, stateMachine);
            attackState = new AttackState(this, stateMachine);

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
