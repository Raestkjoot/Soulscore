//using UnityEngine;

//namespace AbilitySystem
//{
//    [CreateAssetMenu(fileName = "NewAbility", menuName = "AbilitySystem/Ability")]
//    public class Ability : ScriptableObject
//    {
//        [SerializeField] private new string name;
//        [SerializeField] private string description;
//        [SerializeField] private float castTime;
//        [SerializeField] private float cooldown;
//        [SerializeField] private bool isImmediate;
//        public bool IsImmediate() { return isImmediate; }
//        [SerializeField] private float range;
//        public float Range() { return range; }
//        [SerializeField] private float width;
//        public float Width() { return width; }

//        [SerializeField] private TargetType targetType;
//        public TargetType GetTargetType()
//        {
//            return targetType;
//        }

//        [SerializeField] private Effect[] effects;

//        public void Execute(Unit source, Unit target)
//        {
//            foreach (Effect effect in effects)
//                effect.Execute(source, target);
//        }

//        public void Execute(Unit source, Unit[] targets)
//        {
//            foreach (Unit target in targets)
//                foreach (Effect effect in effects)
//                    effect.Execute(source, target);
//        }

//        public void Execute(Unit source, Vector3 target)
//        {
//        }

//        //public void Cancel()
//        //{
//        //    foreach (Effect effect in effects)
//        //        effect.Cancel();
//        //}

//        //public void End()
//        //{
//        //    foreach (Effect effect in effects)
//        //        effect.End();
//        //}
//    }
//}