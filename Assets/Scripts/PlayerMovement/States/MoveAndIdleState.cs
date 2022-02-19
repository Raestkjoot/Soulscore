using UnityEngine;

namespace PlayerGameplay
{
    public class MoveAndIdleState : State
    {
        private Vector2 moveDirection;
        private bool dash;
        private bool attack;

        public MoveAndIdleState(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler)
                    : base(playerController, stateMachine, inputHandler) { }

        public override void Enter()
        {
            base.Enter();
            dash = false;
            attack = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();

            // moveDirection = _inputHandler.GetMoveDirection;
            moveDirection = inputHandler.GetMoveDirection();

            // TODO: action = _inputHandler.GetAction;
            dash = Input.GetButtonDown("Dash");
            attack = Input.GetButtonDown("Attack");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // TODO: switch (action) ...
            if (dash)
            {
                stateMachine.ChangeState(playerController.dashState);
            }
            if (attack)
            {
                stateMachine.ChangeState(playerController.attackState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (moveDirection != Vector2.zero)
            {
                playerController.Move(moveDirection, playerController.MovementSpeed);
                // Play run
                playerController.ChangeAnimationState("PlayerRunRight");
                // TODO: Set direction (for animation)
            }
            else
            {
                // Play idle
                playerController.ChangeAnimationState("PlayerIdle");
            }
        }
    }
}
