using System;
using UnityEngine;

[Serializable]
public class Attribute
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private float _baseValue;

    private float _curValue;

    public void SetCurValue(float newValue)
    {
        _curValue = newValue;
    }

    public void SetCurValueToBaseValue()
    {
        _curValue = _baseValue;
    }

    public float GetCurValue()
    {
        return _curValue;
    }

    public void SetBaseValue(float newValue)
    {
        _baseValue = newValue;
    }
}