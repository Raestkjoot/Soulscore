using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerGameplay
{
    public class IdleState : State
    {
        private Action _action;

        public IdleState(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler)
                        : base(playerController, stateMachine, inputHandler) { }

        public override void Enter()
        {
            base.Enter();
            _action = Action.None;
        }

        public override void HandleInput()
        {
            base.HandleInput();

            Action newAction = _inputHandler.GetActionInput();
            if (newAction != Action.None)
                _action = newAction;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            switch (_action)
            {
                case Action.Attack:
                    _stateMachine.ChangeState(_playerController.attackState);
                    break;
            }
        }
    }
}
