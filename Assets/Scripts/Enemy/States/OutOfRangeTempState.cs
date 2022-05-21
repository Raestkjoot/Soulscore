namespace Enemy
{
    public class OutOfRangeTempState : EnemyState
    {
        private float _curDist;

        public OutOfRangeTempState(EnemyStateMachine stateMachine, EnemyController enemyController) 
            : base(stateMachine, enemyController)
        {

        }

        public override void LogicUpdate()
        {
            _curDist = _enemyController.GetDistToPlayer();

            if (_curDist < _enemyController.StopAtDist)
            {
                _stateMachine.ChangeState(_enemyController.attackState);
            }
        }
    }
}