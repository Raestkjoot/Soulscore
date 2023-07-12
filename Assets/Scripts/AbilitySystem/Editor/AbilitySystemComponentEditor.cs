using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilitySystemComponent))]
public class AbilitySystemComponentEditor : Editor
{
    SerializedProperty abilities;

    void OnEnable()
    {
        abilities = serializedObject.FindProperty("abilities");
    }

    public override void OnInspectorGUI()
    {
        if (target is AbilitySystemComponent abilitySystemComponent)
        {
            CreateAbilityObjectField("Basic Ability", 0, abilitySystemComponent);
            CreateAbilityObjectField("Dash Ability", 1, abilitySystemComponent);
            CreateAbilityObjectField("Special Ability 1", 2, abilitySystemComponent);
            CreateAbilityObjectField("Special Ability 2", 3, abilitySystemComponent);
        }
    }

    private void CreateAbilityObjectField(string abilityLabel, int abilityID, AbilitySystemComponent asc)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(abilityLabel);
        //Ability currentNumber = abilitySystemComponent.abilities[i];
        asc.abilities[abilityID] = (Ability)EditorGUILayout.ObjectField(
            asc.abilities[abilityID],
            typeof(Ability),
            false);
        EditorGUILayout.EndHorizontal();
    }
}