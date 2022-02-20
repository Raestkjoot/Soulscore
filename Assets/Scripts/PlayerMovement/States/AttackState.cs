using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerGameplay
{
    public class AttackState : State
    {
        public LayerMask enemyLayers = LayerMask.GetMask("Default");

        private Action _action;

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

        private GameObject _attackVFX;

        public AttackState(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler)
                        : base(playerController, stateMachine, inputHandler)
        {
            _attackPoint = GameObject.Find("AttackPoint").transform;
            if (!_attackPoint)
                Debug.LogWarning("AttackPoint game object not found!");

            _attackDuration = playerController.AttackDuration;
            _attackHitDelay = playerController.AttackHitDelay;
            _attackVFX = GameObject.Find("PlayerAim");
            _attackVFX.SetActive(false);
        }

        public override void Enter()
        {
            base.Enter();

            _deltaTime = 0f;
            _fixedDeltaTime = 0f;
            _hasAttacked = false;

            _aimDirection = _inputHandler.GetAimDirection();

            _action = Action.None;

            // The attack hit box & vfx gets rotated around the player's axis of rotation towards the aimDirection.
            _attackVFX.transform.rotation = Quaternion.LookRotation(_aimDirection, Vector3.up);
            _attackVFX.SetActive(true);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            _moveDirection = _inputHandler.GetMoveDirection();

            Action newAction = _inputHandler.GetActionInput();
            if (newAction != Action.None)
                _action = newAction;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _deltaTime += Time.deltaTime;

            if (_deltaTime >= _attackDuration)
            {
                _attackVFX.SetActive(false);

                switch (_action)
                {
                    case Action.Attack:
                        _stateMachine.ChangeState(_playerController.attackState);
                        break;
                    case Action.Dash:
                        _stateMachine.ChangeState(_playerController.dashState);
                        break;
                    default:
                        _stateMachine.ChangeState(_playerController.moveAndIdleState);
                        break;
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // We also want to move a little while attacking
            _playerController.Move(_moveDirection, _playerController.AttackingMovementSpeed);

            // Hit detection
            _fixedDeltaTime += Time.fixedDeltaTime;

            // TODO: Maybe we'll wanna remove the bool_hasAttacked and check that we don't damage the same enemy multiple times.
            //       That way we can have a damage window instead of just a damage frame.
            //       Maybe use a dictionary and give each enemy an enemy-ID?
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
    }
}
