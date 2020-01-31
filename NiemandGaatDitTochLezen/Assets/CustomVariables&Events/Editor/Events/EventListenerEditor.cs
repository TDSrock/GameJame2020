using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace SjorsGielen.Events.Editors
{
    [CustomEditor(typeof(GameEventListener))]
    [CanEditMultipleObjects]
    public class EventListenerEditor : Editor
    {
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Dictionary<GameEvent, GameEventListener> events = new Dictionary<GameEvent, GameEventListener>();
            foreach (GameEventListener listener in targets)
            {
                GameEvent e = listener.Event;
                if (!events.ContainsKey(e) && e != null)
                {
                    events.Add(e, listener);
                }

                if (targets.Length == 1)
                {
                    GUI.enabled = Application.isPlaying;
                    if (GUILayout.Button("Raise for this listener only"))
                        listener.Response.Invoke();
                }
            }
            if(targets.Length > 1)
            {
                GUI.skin.button.alignment = TextAnchor.MiddleRight;
                GUILayout.Label("Raiser Per listener (Multi object editing detected)");
                GUILayout.BeginHorizontal();
                for (int targetCount = 0; targetCount < targets.Length; targetCount++)
                {
                    if (targetCount % 5 == 0)
                    {
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                    }
                    
                    GUI.enabled = Application.isPlaying;
                    if (GUILayout.Button(targets[targetCount].name, GUILayout.Width((EditorGUIUtility.currentViewWidth - 60) / (targets.Length < 5 ? targets.Length : 5))))
                        (targets[targetCount] as GameEventListener).Response.Invoke();
                }
                GUILayout.EndHorizontal();
            }
            GUI.skin.button.alignment = TextAnchor.MiddleCenter;
            if (events.Count > 1)
            {
                GUI.enabled = Application.isPlaying;

                if (GUILayout.Button("Raise all events"))
                {
                    foreach (GameEvent e in events.Keys)
                        e.Raise();
                }
            }
            if (events.Count == 1) {
                GUI.enabled = Application.isPlaying;
                if (GUILayout.Button("Raise"))
                        foreach (GameEvent e in events.Keys)
                            e.Raise();
            }



        }
    }
}