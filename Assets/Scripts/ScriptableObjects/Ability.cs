using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "AbilitySystem/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private float castTime;
    [SerializeField] private float cooldown;

    // array of effects (scriptable objects?)
    [SerializeField] private EffectType effectType;
}

public enum EffectType
{
    Damage,
    Heal,
    Buff,
    Projectile,
    AreaOfEffect,
    Movement
}