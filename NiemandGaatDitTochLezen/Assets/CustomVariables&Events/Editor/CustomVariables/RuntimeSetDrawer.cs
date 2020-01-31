using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SjorsGielen.CustomVariables.RuntimeSets;
using SjorsGielen.Helpers;

namespace SjorsGielen.CustomVariables.RuntimeSets.Editors
{
    [CustomEditor(typeof(GameObjectRuntimeSet))]
    public class RuntimeSetDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorList.Show(serializedObject.FindProperty("Items"), EditorListOption.NamedElementLabels | EditorListOption.ElementLabels);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
