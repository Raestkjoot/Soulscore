using UnityEngine;

namespace PlayerGameplay
{
    public class DashState : PlayerState
    {
        // TODO: bool - iFrames?
        private float _dashDuration;
        private float _deltaTime;

        private Vector2 _moveDirection;

        public DashState(PlayerStateMachine stateMachine, PlayerController playerController, PlayerInputHandler inputHandler) 
                    : base(stateMachine, playerController, inputHandler)
        {
            _dashDuration = playerController.DashDuration;
        }

        public override void Enter()
        {
            base.Enter();

            //_moveDirection = _inputHandler.GetMoveDirection();
            _playerController.IsDashing = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _deltaTime += Time.deltaTime;

            if (_deltaTime >= _dashDuration)
            {
                _deltaTime = 0f;
                _stateMachine.ChangeState(_playerController.moveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            //_playerController.Move(_moveDirection, _playerController.DashSpeed);
        }

        public override void Exit()
        {
            base.Exit();

            _playerController.IsDashing = false;
        }
    } 
}
