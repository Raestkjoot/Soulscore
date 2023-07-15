using AbilitySystem;
using System;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private GameObject directionIndicator;
    [SerializeField] private GameObject coneIndicator;
    [SerializeField] private GameObject AOEIndicator;

    public Unit GetTargetOfCurrentActiveTargeting()
    {
        throw new NotImplementedException();
    }

    public Unit GetTargetFromTargetingType(TargetType targetType)
    {
        throw new NotImplementedException();
    }

    public void Target (Material vfxMaterial, float range)
    { }

    public void Direction (Material vfxMaterial, float range, float width)
    { }

    public void Cone (Material vfxMaterial, float range, float width) 
    { }

    public void selfAOE (Material material, float range, float rangeOfAOE)
    { }
}