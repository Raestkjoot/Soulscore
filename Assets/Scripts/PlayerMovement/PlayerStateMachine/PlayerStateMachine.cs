namespace PlayerGameplay
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }

        public void Initialize(PlayerState initialState)
        {
            CurrentState = initialState;
            initialState.Enter();
        }

        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}