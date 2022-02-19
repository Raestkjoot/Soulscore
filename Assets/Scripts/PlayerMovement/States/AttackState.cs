using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerGameplay
{
    public class AttackState : State
    {
        // New mouse input aim
        private bool isUsingController;
        private Camera cam => GameObject.Find("Camera").GetComponent<Camera>();
        private Action _action;

        // New attack hit
        private Transform attackPoint => GameObject.Find("AttackPoint").transform;
        private float attackRange = 1f;
        public LayerMask enemyLayers = LayerMask.GetMask("Default");

        private float attackDuration;
        private float attackHitDelay;
        private float deltaTime;
        // New attack hit
        private float fixedDeltaTime;
        private bool hasAttacked;

        private Vector2 moveDirection;
        // New mouse input aim
        private Vector2 aimDirection;

        GameObject attackDirVisual;

        public AttackState(PlayerController playerController, StateMachine stateMachine, PlayerInputHandler inputHandler)
                        : base(playerController, stateMachine, inputHandler)
        {
            attackDuration = playerController.AttackDuration;
            attackHitDelay = playerController.AttackHitDelay;
            attackDirVisual = GameObject.Find("PlayerAim");
            attackDirVisual.SetActive(false);
        }

        public override void Enter()
        {
            base.Enter();

            deltaTime = 0f;
            fixedDeltaTime = 0f;
            hasAttacked = false;

            aimDirection = inputHandler.GetAimDirection();

            _action = Action.None;

            // The attack hit box & vfx gets rotated around the player's axis of rotation towards the aimDirection.
            attackDirVisual.transform.rotation = Quaternion.LookRotation(aimDirection, Vector3.up);
            attackDirVisual.SetActive(true);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            moveDirection = inputHandler.GetMoveDirection();

            Action newAction = inputHandler.GetActionInput();
            if (newAction != Action.None)
                _action = newAction;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            deltaTime += Time.deltaTime;

            if (deltaTime >= attackDuration)
            {
                attackDirVisual.SetActive(false);

                switch (_action)
                {
                    case Action.Attack:
                        stateMachine.ChangeState(playerController.attackState);
                        break;
                    case Action.Dash:
                        stateMachine.ChangeState(playerController.dashState);
                        break;
                    default:
                        stateMachine.ChangeState(playerController.moveAndIdleState);
                        break;
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // We also want to move a little while attacking
            playerController.Move(moveDirection, playerController.AttackingMovementSpeed);

            // Hit detection
            fixedDeltaTime += Time.fixedDeltaTime;

            if (fixedDeltaTime >= attackHitDelay && !hasAttacked)
            {
                hasAttacked = true;

                Collider2D[] hitEnemies =
                    Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit " + enemy.name);
                } 
            }
        }
    }
}
