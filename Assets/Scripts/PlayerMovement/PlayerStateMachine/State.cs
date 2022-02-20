using UnityEngine;

namespace PlayerGameplay
{
    public abstract class State
    {
        protected PlayerController _playerController;
        protected StateMachine _stateMachine;
        protected PlayerInputHandler _inputHandler;

        protected State(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler)
        {
            _playerController = playerController;
            _stateMachine = stateMachine;
            _inputHandler = inputHandler;
        }

        public virtual void Enter() { }

        public virtual void HandleInput() { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() { }

        public virtual void Exit() { }
    }
}