namespace PlayerGameplay
{
    public abstract class PlayerState
    {
        protected PlayerController _playerController;
        protected PlayerStateMachine _stateMachine;
        protected PlayerInputHandler _inputHandler;

        protected PlayerState(PlayerStateMachine stateMachine, PlayerController playerController, PlayerInputHandler inputHandler)
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