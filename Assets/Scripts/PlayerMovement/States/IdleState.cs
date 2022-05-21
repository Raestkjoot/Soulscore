namespace PlayerGameplay
{
    public class IdleState : PlayerState
    {
        private Action _action;

        public IdleState(PlayerStateMachine stateMachine, PlayerController playerController, PlayerInputHandler inputHandler)
                        : base(stateMachine, playerController, inputHandler) { }

        public override void Enter()
        {
            base.Enter();
            _action = Action.None;
            _playerController.IsDoingAction = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();

            Action newAction = _inputHandler.GetActionInput();
            if (newAction != Action.None)
                _action = newAction;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            switch (_action)
            {
                case Action.Attack:
                    _stateMachine.ChangeState(_playerController.attackState);
                    break;
                //TODO: case Action.Ability...
            }
        }

        public override void Exit()
        {
            base.Exit();
            _playerController.IsDoingAction = true;
        }
    }
}
