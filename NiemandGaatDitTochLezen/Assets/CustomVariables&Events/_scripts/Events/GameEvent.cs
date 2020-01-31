using System.Collections.Generic;
using UnityEngine;

namespace SjorsGielen.Events
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        [SerializeField]
        protected List<GameEventListener> eventListeners =
            new List<GameEventListener>();

        public void Raise()
        {

            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                try
                {
                    eventListeners[i].OnEventRaised();
                }
                catch (System.IndexOutOfRangeException e)
                {
                    Debug.LogErrorFormat("Event raise crashed on index {0}, does your event destory other listeners? \n{1}", i, e.StackTrace);
                }
            }
        }



        public void RegisterListener(GameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}