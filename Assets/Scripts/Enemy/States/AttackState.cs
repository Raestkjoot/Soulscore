namespace Enemy
{
    public class AttackState : EnemyState
    {
        public AttackState(EnemyStateMachine stateMachine, EnemyController enemyController) 
            : base(stateMachine, enemyController)
        {
        }
    }
}