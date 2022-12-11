using UnityEngine;

[CreateAssetMenu(fileName = "NewAttributesContainer", menuName = "AttributesSystem/AttributesContainer")]
public class AttributesContainerSO : ScriptableObject
{
    [SerializeField]
    private Attribute[] attributes;

    private void Awake()
    {
        foreach(Attribute attribute in attributes)
        {
            attribute.SetCurValueToBaseValue();
        }
    }
}