using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "NewAbility", menuName = "AbilitySystem/Ability")]
    public class Ability : ScriptableObject
    {
        [SerializeField] private new string name;
        [SerializeField] private string description;
        [SerializeField] private float castTime;
        [SerializeField] private float cooldown;

        [SerializeField] private TargetType targetType;

        [SerializeField] private Effect[] effects;

        public void Execute(Unit source)
        {
            foreach (Effect effect in effects)
                effect.Execute(source);
        }

        //public void Cancel()
        //{
        //    foreach (Effect effect in effects)
        //        effect.Cancel();
        //}

        //public void End()
        //{
        //    foreach (Effect effect in effects)
        //        effect.End();
        //}
    }
}