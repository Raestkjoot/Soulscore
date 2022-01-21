using UnityEngine;

namespace PlayerGameplay
{
    public class MoveAndIdleState : State
    {
        private Vector2 inputDirection;
        private bool dash;
        private bool attack;

        public MoveAndIdleState(PlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            dash = false;
            attack = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            inputDirection.x = Input.GetAxisRaw("Horizontal");
            inputDirection.y = Input.GetAxisRaw("Vertical");

            dash = Input.GetButtonDown("Dash");
            attack = Input.GetButtonDown("Attack");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
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
            playerController.Move(inputDirection, playerController.MovementSpeed);
        }
    }
}
