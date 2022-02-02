using UnityEngine;

namespace PlayerAnimStates
{
    public class CharacterStateBase : StateMachineBehaviour
    {
        private CharacterControl characterControl;

        public CharacterControl GetCharacterControl(Animator animator)
        {
            if (!characterControl)
            {
                characterControl = animator.GetComponentInParent<CharacterControl>(); 
            }
            return characterControl;
        }
    }
}