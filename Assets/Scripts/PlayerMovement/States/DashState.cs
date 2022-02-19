using UnityEngine;

namespace PlayerGameplay
{
    public class DashState : State
    {
        private float dashDuration;
        private float deltaTime;

        private Vector2 moveDirection;

        public DashState(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler) 
                    : base(playerController, stateMachine, inputHandler) 
        {
            dashDuration = playerController.DashDuration;
        }

        public override void Enter()
        {
            base.Enter();

            moveDirection = inputHandler.GetMoveDirection();
        }

        // TODO: get action
        // public override void HandleInput()
        // action = _inputHandler.GetAction;

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            deltaTime += Time.deltaTime;

            if (deltaTime >= dashDuration)
            {
                deltaTime = 0f;
                // TODO: switch (action)
                stateMachine.ChangeState(playerController.moveAndIdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            playerController.Move(moveDirection, playerController.DashSpeed);
        }
    } 
}
