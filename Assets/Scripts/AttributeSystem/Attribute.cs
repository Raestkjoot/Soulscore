using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attribute
{
    [SerializeField]
    private EAttribute _name;
    [SerializeField]
    private float _baseValue;

    private float _curValue;
    private List<AttributeModifier> _modifiers;

    public EAttribute GetName()
    {
        return _name;
    }

    public void SetCurValueToBaseValue()
    {
        _curValue = _baseValue;
    }

    public float GetCurValue()
    {
        return _curValue;
    }

    public int AddModifier(AttributeModifier modifier)
    {
        _modifiers.Add(modifier);
        CalculateModifiers();

        return _modifiers.Count;
    }

    public void RemoveModifier(int id)
    {
        _modifiers.RemoveAt(id);
        CalculateModifiers();
    }

    private void CalculateModifiers()
    {
        float newValue = _baseValue;
        foreach(AttributeModifier modifier in _modifiers)
        {
            modifier.Apply(ref newValue);
        }

        _curValue = newValue;
    }
}