using UnityEngine;

namespace AbilitySystem
{
    public abstract class Effect : ScriptableObject
    {
        public abstract void Execute(Unit source);
        public abstract void Cancel();
        public abstract void End();
    }
}