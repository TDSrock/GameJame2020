using UnityEditor;
using UnityEngine;
using SjorsGielen.Helpers;

namespace SjorsGielen.Events.Editors
{
    [CustomEditor(typeof(GameEvent))]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUI.enabled = Application.isPlaying;

            GameEvent e = target as GameEvent;
            if (GUILayout.Button("Raise"))
                e.Raise();

            EditorList.Show(serializedObject.FindProperty("eventListeners"), EditorListOption.ListLabel | EditorListOption.NamedElementLabels | EditorListOption.ElementLabels);

        }
    }
}