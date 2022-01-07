namespace PlayerGameplay
{
    public abstract class State
    {
        protected PlayerController _playerController;
        protected StateMachine _stateMachine;

        protected State(PlayerController playerController, StateMachine stateMachine)
        {
            _playerController = playerController;
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void HandleInput() { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() { }

        public virtual void Exit() { }
    }
}