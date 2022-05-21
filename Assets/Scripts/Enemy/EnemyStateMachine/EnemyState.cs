using UnityEngine;

namespace Enemy
{
    public abstract class EnemyState
    {
        //protected PlayerController _playerController;
        protected EnemyStateMachine _stateMachine;
        //protected PlayerInputHandler _inputHandler;

        protected EnemyState(EnemyStateMachine stateMachine) //, PlayerController playerController, PlayerInputHandler inputHandler)
        {
            //_playerController = playerController;
            _stateMachine = stateMachine;
            //_inputHandler = inputHandler;
        }

        public virtual void Enter() { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() { }

        public virtual void Exit() { }
    }
}