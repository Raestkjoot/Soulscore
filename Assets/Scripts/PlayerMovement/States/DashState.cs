using UnityEngine;

namespace PlayerGameplay
{
    public class DashState : State
    {
        // TODO: Perhaps deltaTime could be public so dash attack can use it? Also moveDirection?
        private float _dashDuration;
        private float _deltaTime;

        private Vector2 _moveDirection;

        private Action _action;

        public DashState(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler) 
                    : base(playerController, stateMachine, inputHandler)
        {
            _dashDuration = playerController.DashDuration;
        }

        public override void Enter()
        {
            base.Enter();

            _moveDirection = _inputHandler.GetMoveDirection();
            _action = Action.None;
        }

        // TODO: get action
        // public override void HandleInput()
        // action = _inputHandler.GetAction;
        public override void HandleInput()
        {
            base.HandleInput();

            Action action = _inputHandler.GetActionInput();
            if (action != Action.None)
                _action = action;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _deltaTime += Time.deltaTime;

            if (_deltaTime >= _dashDuration)
            {
                _deltaTime = 0f;

                switch (_action)
                {
                    case Action.Attack:
                        _stateMachine.ChangeState(_playerController.attackState);
                        break;
                    default:
                        _stateMachine.ChangeState(_playerController.moveAndIdleState);
                        break;
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            _playerController.Move(_moveDirection, _playerController.DashSpeed);
        }
    } 
}
