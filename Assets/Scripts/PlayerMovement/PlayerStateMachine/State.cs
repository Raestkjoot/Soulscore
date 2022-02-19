using UnityEngine;

namespace PlayerGameplay
{
    public abstract class State
    {
        protected PlayerController playerController;
        protected StateMachine stateMachine;
        protected PlayerInputHandler inputHandler;

        protected State(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler)
        {
            this.playerController = playerController;
            this.stateMachine = stateMachine;
            this.inputHandler = inputHandler;
        }

        public virtual void Enter() { }

        public virtual void HandleInput() { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() { }

        public virtual void Exit() { }
    }
}