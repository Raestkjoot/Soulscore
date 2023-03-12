using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AttributeModifier : MonoBehaviour
{
    public float Magnitude { get; set; }
    public EModifierType ModifierType { get; set; }

    public void Apply(ref float value)
    {
        switch (ModifierType)
        {
            case EModifierType.Add:
                value += Magnitude;
                break;
            case EModifierType.Multiply:
                value *= Magnitude;
                break;
            case EModifierType.Override:
                value = Magnitude;
                break;
        }
    }
}

public enum EModifierType
{
    Add,
    Multiply,
    Override
}