#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Fridge))]
class FridgeEditor : Editor {
    public Fridge m_Fridge;
    private SerializedProperty m_OnDoorOpenSE;
    private SerializedProperty m_OnDoorCloseSE;
    private bool m_Events = true;

    private void OnEnable() {
        this.m_Fridge = this.target as Fridge;
        this.m_OnDoorOpenSE = this.serializedObject.FindProperty("OnDoorOpen");
        this.m_OnDoorCloseSE = this.serializedObject.FindProperty("OnDoorClose");
    }

    public override void OnInspectorGUI() {
        this.m_Fridge.DoorTransform = (Transform) EditorGUILayout.ObjectField("Door Transform", this.m_Fridge.DoorTransform, typeof(Transform), true);
        this.m_Fridge.DoorOpenAngle = EditorGUILayout.FloatField("Door Open Angle", this.m_Fridge.DoorOpenAngle);

        this.m_Fridge.SmoothTime = EditorGUILayout.FloatField("Door Smooth Time", this.m_Fridge.SmoothTime);
        this.m_Fridge.AngleEpsilon = EditorGUILayout.FloatField("Door Angle Epsilon", this.m_Fridge.AngleEpsilon);

        this.m_Events = EditorGUILayout.Foldout(this.m_Events, "Events");
        if(this.m_Events) {
            EditorGUILayout.PropertyField(this.m_OnDoorOpenSE);
            EditorGUILayout.PropertyField(this.m_OnDoorCloseSE);
        }

        this.serializedObject.ApplyModifiedProperties();
    }
}
#endif
