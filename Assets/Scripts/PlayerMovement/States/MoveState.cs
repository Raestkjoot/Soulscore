using UnityEngine;

namespace PlayerGameplay
{
    public class MoveState : State
    {
        private Vector2 inputDirection;

        public MoveState(PlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine) { }

        public override void HandleInput()
        {
            base.HandleInput();

            inputDirection.x = Input.GetAxisRaw("Horizontal");
            inputDirection.y = Input.GetAxisRaw("Vertical");
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            playerController.Move(inputDirection, playerController.MovementSpeed);
        }
    }
}
