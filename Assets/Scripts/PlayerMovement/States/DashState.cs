using UnityEngine;

namespace PlayerGameplay
{
    public class DashState : State
    {
        private float dashDuration;
        private float deltaTime;

        private Vector2 moveDirection;

        private Action _action;

        public DashState(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler) 
                    : base(playerController, stateMachine, inputHandler) 
        {
            dashDuration = playerController.DashDuration;
        }

        public override void Enter()
        {
            base.Enter();

            moveDirection = inputHandler.GetMoveDirection();
            _action = Action.None;
        }

        // TODO: get action
        // public override void HandleInput()
        // action = _inputHandler.GetAction;
        public override void HandleInput()
        {
            base.HandleInput();

            Action action = inputHandler.GetActionInput();
            if (action != Action.None)
                _action = action;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            deltaTime += Time.deltaTime;

            if (deltaTime >= dashDuration)
            {
                deltaTime = 0f;

                switch (_action)
                {
                    case Action.Attack:
                        stateMachine.ChangeState(playerController.attackState);
                        break;
                    default:
                        stateMachine.ChangeState(playerController.moveAndIdleState);
                        break;
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            playerController.Move(moveDirection, playerController.DashSpeed);
        }
    } 
}
