using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerGameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public float movementSpeed { get; private set; }
        [field: SerializeField] public float dashSpeed { get; private set; }
        [field: SerializeField] public float dashDuration { get; private set; }

        private Rigidbody2D _rigidbody => gameObject.GetComponent<Rigidbody2D>();

        public StateMachine stateMachine;
        public MoveAndIdleState moveAndIdleState;
        public DashState dashState;

        /// <summary>
        /// Move the player according to the inputDirection vector and speed
        /// </summary>
        public void Move(Vector2 direction, float speed)
        {
            _rigidbody.MovePosition(_rigidbody.position + direction.normalized * speed * Time.fixedDeltaTime);
        }

        #region Callbacks
        private void Start()
        {
            stateMachine = new StateMachine();

            moveAndIdleState = new MoveAndIdleState(this, stateMachine);
            dashState = new DashState(this, stateMachine);

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
