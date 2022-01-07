using UnityEngine;

namespace PlayerGameplay
{
    public class MoveAndIdleState : State
    {
        private Vector3 inputDirection;
        private bool dash;

        public MoveAndIdleState(PlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            dash = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            inputDirection = Vector3.zero;
            inputDirection.x = Input.GetAxisRaw("Horizontal");
            inputDirection.z = Input.GetAxisRaw("Vertical");

            dash = Input.GetButtonDown("Dash");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (dash)
            {
                _stateMachine.ChangeState(_playerController.dashState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            _playerController.Move(inputDirection, _playerController.movementSpeed);
        }
    }
}
