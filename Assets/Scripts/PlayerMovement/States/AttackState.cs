using UnityEngine;

namespace PlayerGameplay
{
    public class AttackState : PlayerState
    {
        public LayerMask enemyLayers = LayerMask.GetMask("Default");

        // New attack hit
        private Transform _attackPoint;
        private float _attackRange = 1f;

        private float _attackDuration;
        private float _attackHitDelay;
        private float _deltaTime;
        private float _fixedDeltaTime;
        private bool _hasAttacked;

        private Vector2 _moveDirection;
        private Vector2 _aimDirection;

        //TODO: private GameObject _attackVFX;
        private Transform _playerAim;

        public AttackState(PlayerStateMachine stateMachine, PlayerController playerController, PlayerInputHandler inputHandler)
                        : base(stateMachine, playerController, inputHandler)
        {
            _attackPoint = GameObject.Find("AttackPoint").transform;
            if (!_attackPoint)
                Debug.LogWarning("AttackPoint game object not found!");

            _attackDuration = playerController.AttackDuration;
            _attackHitDelay = playerController.AttackHitDelay;
            //TODO: _attackVFX = GameObject.Find("PlayerAim");
            //TODO: _attackVFX.SetActive(false);
            _playerAim = GameObject.Find("PlayerAim").transform;
        }

        public override void Enter()
        {
            base.Enter();

            _playerController.SetSpeed(_playerController.AttackingMovementSpeed);

            _deltaTime = 0f;
            _fixedDeltaTime = 0f;
            _hasAttacked = false;

            _aimDirection = _inputHandler.GetAimDirection();

            // The attack hit box & vfx gets rotated towards the aimDirection.
            Quaternion aimRotation = Quaternion.LookRotation(_aimDirection);
            _playerAim.rotation = aimRotation;
            
            _playerController.ChangeAnimationState("PlayerAttack");
            //TODO: _attackVFX.SetActive(true);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            _moveDirection = _inputHandler.GetMoveDirection();

            // TODO: When abilities are added, add transitions to them.
            //Action newAction = _inputHandler.GetActionInput();
            //if (newAction != Action.None)
            //    _action = newAction;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _deltaTime += Time.deltaTime;

            if (_deltaTime >= _attackDuration)
            {
                //TODO: _attackVFX.SetActive(false);

                _stateMachine.ChangeState(_playerController.idleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // TODO: Maybe we'll wanna remove the bool_hasAttacked and check that we don't damage the same enemy multiple times.
            //       That way we can have a damage window instead of just a damage frame.
            //       Maybe use a dictionary and give each enemy an enemy-ID?

            // Hit detection
            _fixedDeltaTime += Time.fixedDeltaTime;

            if (_fixedDeltaTime >= _attackHitDelay && !_hasAttacked)
            {
                _hasAttacked = true;

                Collider2D[] hitEnemies =
                    Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit " + enemy.name);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();

            _playerController.SetSpeed(_playerController.MovementSpeed);
        }
    }
}
