using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AttributeModifier : MonoBehaviour
{
    public float Magnitude { get; set; }
    public EModifierType ModifierType { get; set; }

    public void Apply(Attribute attribute)
    {
        float newValue = attribute.GetCurValue();

        switch (ModifierType)
        {
            case EModifierType.Add:
                newValue += Magnitude;
                break;
            case EModifierType.Multiply:
                newValue *= Magnitude;
                break;
            case EModifierType.Override:
                newValue = Magnitude;
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