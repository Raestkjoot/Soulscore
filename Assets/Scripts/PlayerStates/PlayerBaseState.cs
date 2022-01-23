using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uhhhhRandomForNow
{
    public class PlayerBaseState : StateMachineBehaviour
    {
        private CharacterController characterController;
        public CharacterController GetCharacterController(Animator animator)
        {
            if (!characterController)
                characterController = animator.GetComponentInParent<CharacterController>();

            return characterController;
        }
    } 
}
