using UnityEngine;

namespace PlayerGameplay
{
    public class MoveAndIdleState : State
    {
        private Vector2 _moveDirection;
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

            _moveDirection = _inputHandler.GetMoveDirection();

            _action = _inputHandler.GetActionInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            switch (_action)
            {
                case Action.Dash:
                    _stateMachine.ChangeState(_playerController.dashState);
                    break;
                case Action.Attack:
                    _stateMachine.ChangeState(_playerController.attackState);
                    break;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (_moveDirection != Vector2.zero)
            {
                _playerController.Move(_moveDirection, _playerController.MovementSpeed);
                // Play run animation
                _playerController.ChangeAnimationState("PlayerRunRight");
                // TODO: Set direction (for animation)
            }
            else
            {
                // Play idle animation
                _playerController.ChangeAnimationState("PlayerIdle");
            }
        }
    }
}
