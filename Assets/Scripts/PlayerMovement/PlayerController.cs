using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerGameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        // Higher speeds can move us through objects (about 15+)
        [field: SerializeField] public float movementSpeed { get; private set; }
        [field: SerializeField] public float dashSpeed { get; private set; }
        [field: SerializeField] public float dashDuration { get; private set; }
        // This is particularly going to be a problem for the dash
        // TODO: Look into how to prevent this and how to know if we can dash over some dashable obstacle (dash all the way over or not at all)
        // https://answers.unity.com/questions/55179/cheapest-way-to-catch-collisions-on-very-fast-movi.html

        private Rigidbody _rigidbody => gameObject.GetComponent<Rigidbody>();

        public StateMachine stateMachine;
        public MoveAndIdleState moveAndIdleState;
        public DashState dashState;

        /// <summary>
        /// Move the player according to the inputDirection vector and speed
        /// </summary>
        public void Move(Vector3 direction, float speed)
        {
            _rigidbody.MovePosition(transform.position + direction.normalized * speed * Time.fixedDeltaTime);
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
