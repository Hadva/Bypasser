using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Struct used to define an animation event
    /// </summary>
    [System.Serializable]
    public struct AnimationEvent
    {
        public string eventId;
        public EventNode animEvent;
    }

    /// <summary>
    /// Script in charge of firing animatino events
    /// </summary>
    public class AnimationEventsController : MonoBehaviour
    {
        private Dictionary<string, EventNode> m_RegisteredAnimEvents = null;

        [SerializeField]
        private AnimationEvent[] m_AnimEvents;

        private void Start()
        {
            if(m_AnimEvents.Length == 0)
            {
                return;
            }
            m_RegisteredAnimEvents = new Dictionary<string, EventNode>();
            // register animation events
            for (int eIndex=0; eIndex< m_AnimEvents.Length; eIndex++)
            {
                m_RegisteredAnimEvents.Add(m_AnimEvents[eIndex].eventId, m_AnimEvents[eIndex].animEvent);
            }
        }

        /// <summary>
        /// Fire animation event with given id
        /// </summary>
        public bool FireAnimationEvent(string eventId)
        {
            if(m_RegisteredAnimEvents == null || !m_RegisteredAnimEvents.ContainsKey(eventId))
            {
                return false;
            }
            m_RegisteredAnimEvents[eventId].Enter();
            m_RegisteredAnimEvents[eventId].Execute();
            return true;
        }
    }
}
