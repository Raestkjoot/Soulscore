using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerGameplay
{
    public class IdleState : State
    {
        private Action _action;
        private Vector2 _moveDirection;

        public IdleState(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler)
                        : base(playerController, stateMachine, inputHandler) { }

        public override void Enter()
        {
            base.Enter();
            _action = Action.None;
            _playerController.IsDoingAction = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();

            Action newAction = _inputHandler.GetActionInput();
            if (newAction != Action.None)
                _action = newAction;

            _moveDirection = _inputHandler.GetMoveDirection();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            switch (_action)
            {
                case Action.Attack:
                    _stateMachine.ChangeState(_playerController.attackState);
                    break;
                //TODO: case Action.Ability...
            }
        }

        public override void Exit()
        {
            base.Exit();
            _playerController.IsDoingAction = true;
        }
    }
}
