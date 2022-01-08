using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerGameplay
{
    public class AttackState : State
    {
        private float attackDuration;
        private float deltaTime;

        private Vector2 inputDirection;

        public AttackState(PlayerController playerController, StateMachine stateMachine)
                        : base(playerController, stateMachine) 
        {
            attackDuration = _playerController.DashDuration;
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

            if (deltaTime >= attackDuration)
            {
                deltaTime = 0f;
                _stateMachine.ChangeState(_playerController.moveAndIdleState);
            }
        }
    }
}
