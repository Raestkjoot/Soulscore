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

        GameObject attackDirVisual;

        public AttackState(PlayerController playerController, StateMachine stateMachine)
                        : base(playerController, stateMachine) 
        {
            attackDuration = _playerController.AttackDuration;
            attackDirVisual = GameObject.Find("PlayerAim");
            attackDirVisual.SetActive(false);
        }

        public override void Enter()
        {
            base.Enter();
            inputDirection.x = Input.GetAxisRaw("Horizontal");
            inputDirection.y = Input.GetAxisRaw("Vertical");

            attackDirVisual.transform.rotation = Quaternion.LookRotation(inputDirection, Vector3.up);
            attackDirVisual.SetActive(true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            deltaTime += Time.deltaTime;

            if (deltaTime >= attackDuration)
            {
                attackDirVisual.SetActive(false);
                deltaTime = 0f;
                _stateMachine.ChangeState(_playerController.moveAndIdleState);
            }
        }
    }
}
