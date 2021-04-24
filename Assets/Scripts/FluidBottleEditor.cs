#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FluidBottle))]
class FluidBottleEditor : Editor {
    public FluidBottle m_fluidBottle;
    private SerializedProperty m_OnBottleEmptySE;
    private SerializedProperty m_OnFluidSpraySE;
    private bool m_Events = true;

    private void OnEnable() {
        this.m_fluidBottle = this.target as FluidBottle;
        this.m_OnBottleEmptySE = this.serializedObject.FindProperty("OnBottleEmpty");
        this.m_OnFluidSpraySE = this.serializedObject.FindProperty("OnFluidSpray");
    }

    public override void OnInspectorGUI() {
        this.m_fluidBottle.FluidLevel = EditorGUILayout.Slider("Fluid Level", this.m_fluidBottle.FluidLevel, 0, 1);
        this.m_fluidBottle.SprayRatio = EditorGUILayout.Slider("Spray Ratio", this.m_fluidBottle.SprayRatio, 0, 1);

        this.m_Events = EditorGUILayout.Foldout(this.m_Events, "Events");
        if(this.m_Events) {
            EditorGUILayout.PropertyField(this.m_OnFluidSpraySE);
            EditorGUILayout.PropertyField(this.m_OnBottleEmptySE);
        }

        this.serializedObject.ApplyModifiedProperties();
    }
}
#endif