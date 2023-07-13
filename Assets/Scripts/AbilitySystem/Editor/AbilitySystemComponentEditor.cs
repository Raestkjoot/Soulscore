using AbilitySystem;
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
        CreateAbilityObjectField("Basic Ability", (int)AbilityType.BasicAttack);
        CreateAbilityObjectField("Dash Ability", (int)AbilityType.DashAbility);
        CreateAbilityObjectField("Special Ability 1", (int)AbilityType.SpecialAbility1);
        CreateAbilityObjectField("Special Ability 2", (int)AbilityType.SpecialAbility2);
    }

    private void CreateAbilityObjectField(string abilityLabel, int abilityID)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(abilityLabel);

        abilities.GetArrayElementAtIndex(abilityID).objectReferenceValue = 
            (Ability)EditorGUILayout.ObjectField(
                abilities.GetArrayElementAtIndex(abilityID).objectReferenceValue,
                typeof(Ability),
                false);

        EditorGUILayout.EndHorizontal();
    }
}