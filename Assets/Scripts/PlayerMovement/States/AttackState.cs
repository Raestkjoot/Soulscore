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

        private Vector2 inputDirection;
        // New mouse input aim
        private Vector2 attackDirection;

        GameObject attackDirVisual;

        public AttackState(PlayerController playerController, StateMachine stateMachine, bool isUsingController)
                        : base(playerController, stateMachine) 
        {
            this.isUsingController = isUsingController;

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

            inputDirection.x = Input.GetAxisRaw("Horizontal");
            inputDirection.y = Input.GetAxisRaw("Vertical");

            // Handle aim input from mouse or controller
            // this could be handled by an adapter (https://refactoring.guru/design-patterns/adapter/csharp/example)
            if (isUsingController)
            {
                attackDirection = inputDirection;
            }
            else
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                attackDirection = (mousePosition - 
                                    new Vector2(
                                        playerController.transform.position.x, 
                                        playerController.transform.position.y))
                                    .normalized;
            }

            // This is just a visual for testing. Or is it?
            attackDirVisual.transform.rotation = Quaternion.LookRotation(attackDirection, Vector3.up);
            attackDirVisual.SetActive(true);
        }

        public override void HandleInput()
        {
            base.HandleInput();
            // For moving
            inputDirection.x = Input.GetAxisRaw("Horizontal");
            inputDirection.y = Input.GetAxisRaw("Vertical");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            deltaTime += Time.deltaTime;

            if (deltaTime >= attackDuration)
            {
                attackDirVisual.SetActive(false);
                stateMachine.ChangeState(playerController.moveAndIdleState);
            }

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            // We also want to move a little while attacking
            playerController.Move(inputDirection, playerController.AttackingMovementSpeed);

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
