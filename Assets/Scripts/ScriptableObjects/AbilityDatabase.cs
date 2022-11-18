using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityDatabase", menuName = "AbilitySystem/AbilityDatabase")]
public class AbilityDatabase : ScriptableObject
{
    [SerializeField] private Ability[] _abilities;
}