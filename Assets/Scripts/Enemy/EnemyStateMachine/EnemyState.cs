using UnityEngine;

namespace Enemy
{
    public abstract class EnemyState
    {
        protected EnemyController _enemyController;
        protected EnemyStateMachine _stateMachine;
        //protected PlayerInputHandler _inputHandler;

        protected EnemyState(EnemyStateMachine stateMachine, EnemyController enemyController)
        {
            _enemyController = enemyController;
            _stateMachine = stateMachine;
            //_inputHandler = inputHandler;
        }

        public virtual void Enter() { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() { }

        public virtual void Exit() { }
    }
}