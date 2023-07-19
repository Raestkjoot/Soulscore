using UnityEngine;

namespace AbilitySystem
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] protected string _name;
        [SerializeField] protected string _description;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected float _castTime;
        [SerializeField] protected bool _isImmediate;
        [SerializeField] protected Effect[] _effects;
        [SerializeField] protected bool _hasSecondaryEffects;
        [SerializeField] protected float _waitUntilSecondaryEffects;
        [SerializeField] protected Effect[] _secondaryEffects;

        public bool IsImmediate() { return _isImmediate; }
    }
}