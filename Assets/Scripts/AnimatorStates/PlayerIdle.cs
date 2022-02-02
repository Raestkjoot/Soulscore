using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerAnimStates
{
    public class PlayerIdle : CharacterStateBase
    {
        private Vector2 inputDirection;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Im idle");
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            inputDirection.x = Input.GetAxisRaw("Horizontal");
            inputDirection.y = Input.GetAxisRaw("Vertical");

            Debug.Log("InputDir = " + inputDirection);

            GetCharacterControl(animator).Move(inputDirection, GetCharacterControl(animator).MovementSpeed);

            if (inputDirection != Vector2.zero)
                animator.SetInteger("State", 1);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    } 
}
