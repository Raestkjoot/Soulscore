namespace Enemy
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentState { get; private set; }

        public void Initialize(EnemyState initialState)
        {
            CurrentState = initialState;
            initialState.Enter();
        }

        public void ChangeState(EnemyState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}