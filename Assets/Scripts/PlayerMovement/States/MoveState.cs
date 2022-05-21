using UnityEngine;

namespace PlayerGameplay
{
    public class MoveState : PlayerState
    {
        public PlayerState SubState { get; private set; }

        private Vector2 _moveDirection;
        private Action _action;

        public MoveState(PlayerStateMachine stateMachine, PlayerController playerController, PlayerInputHandler inputHandler)
                    : base(stateMachine, playerController, inputHandler) { }

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

            // TODO: bool canDash - Used by other actions to set wether the player can dash during the ability.
            // TODO: Add some cooldown / max consecutive dashes.
            if (_action == Action.Dash)
            {
                _stateMachine.ChangeState(_playerController.dashState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            Animate();
        }

        private void Animate()
        {
            if (_playerController.IsDoingAction) return;

            if (_moveDirection != Vector2.zero)
            {
                _playerController.ChangeAnimationState("PlayerRunRight");
            }
            else
            {
                _playerController.ChangeAnimationState("PlayerIdle");
            }
        }
    }
}
