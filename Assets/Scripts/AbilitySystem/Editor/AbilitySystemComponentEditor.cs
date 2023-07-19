using AbilitySystem;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

[CustomEditor(typeof(AbilityCaster))]
public class AbilitySystemComponentEditor : Editor
{
    SerializedProperty abilities;

    void OnEnable()
    {
        abilities = serializedObject.FindProperty("_abilities");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(
            abilities.GetArrayElementAtIndex((int)AbilityType.BasicAttack),
            new GUIContent("Basic Ability"));
        EditorGUILayout.PropertyField(
            abilities.GetArrayElementAtIndex((int)AbilityType.DashAbility),
            new GUIContent("Dash Ability"));
        EditorGUILayout.PropertyField(
            abilities.GetArrayElementAtIndex((int)AbilityType.SpecialAbility1),
            new GUIContent("Special Ability 1"));
        EditorGUILayout.PropertyField(
            abilities.GetArrayElementAtIndex((int)AbilityType.SpecialAbility2),
            new GUIContent("Special Ability 2"));

        serializedObject.ApplyModifiedProperties();
    }
}