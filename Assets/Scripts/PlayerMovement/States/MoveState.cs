using UnityEngine;

namespace PlayerGameplay
{
    public class MoveState : State
    {
        public State SubState { get; private set; }

        private Vector2 _moveDirection;
        private Action _action;

        public MoveState(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler)
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

            if (_moveDirection != Vector2.zero)
            {
                _playerController.Move(_moveDirection, _playerController.MovementSpeed);
                // Play run animation
                _playerController.ChangeAnimationState("PlayerRunRight");
                // TODO: Set direction (for animation)
                // TODO: Animator.SetVec2("moveDir", _moveDirection);
            }
            else
            {
                // Play idle animation
                _playerController.ChangeAnimationState("PlayerIdle");
            }
        }
    }
}
