using UnityEngine;

namespace PlayerGameplay
{
    public class DashState : State
    {
        private float dashDuration;
        private float deltaTime;

        private Vector2 inputDirection;

        public DashState(PlayerController playerController, StateMachine stateMachine) 
                    : base(playerController, stateMachine) 
        {
            dashDuration = playerController.DashDuration;
        }

        public override void Enter()
        {
            base.Enter();
            inputDirection.x = Input.GetAxisRaw("Horizontal");
            inputDirection.y = Input.GetAxisRaw("Vertical");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            deltaTime += Time.deltaTime;

            if (deltaTime >= dashDuration)
            {
                deltaTime = 0f;
                stateMachine.ChangeState(playerController.moveAndIdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            playerController.Move(inputDirection, playerController.DashSpeed);
        }
    } 
}
