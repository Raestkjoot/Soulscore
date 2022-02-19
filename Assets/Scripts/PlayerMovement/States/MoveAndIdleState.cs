using UnityEngine;

namespace PlayerGameplay
{
    public class MoveAndIdleState : State
    {
        private Vector2 moveDirection;
        private Action _action;

        public MoveAndIdleState(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler)
                    : base(playerController, stateMachine, inputHandler) { }

        public override void Enter()
        {
            base.Enter();
            _action = Action.None;
        }

        public override void HandleInput()
        {
            base.HandleInput();

            moveDirection = inputHandler.GetMoveDirection();

            _action = inputHandler.GetActionInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            switch (_action)
            {
                case Action.Dash:
                    stateMachine.ChangeState(playerController.dashState);
                    break;
                case Action.Attack:
                    stateMachine.ChangeState(playerController.attackState);
                    break;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (moveDirection != Vector2.zero)
            {
                playerController.Move(moveDirection, playerController.MovementSpeed);
                // Play run animation
                playerController.ChangeAnimationState("PlayerRunRight");
                // TODO: Set direction (for animation)
            }
            else
            {
                // Play idle animation
                playerController.ChangeAnimationState("PlayerIdle");
            }
        }
    }
}
