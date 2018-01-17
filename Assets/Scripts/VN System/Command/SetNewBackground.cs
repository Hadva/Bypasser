using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// command used to set a new background on display
    /// </summary>
    public class SetNewBackground : Command
    {
        /// <summary>
        /// new background 
        /// </summary>
        [SerializeField]
        private Sprite m_NewBackground = null;

        /// <summary>
        /// time it will take to fade into new image
        /// </summary>
        [SerializeField]
        private float m_FadeTime = 5f;

        public override void Enter()
        {           
            DisplayManager.instance.SetMainBackground(m_NewBackground, m_FadeTime);
            if (m_FadeTime > 0)
            {
                m_Status = Status.Continue;
                DisplayManager.instance.onBackgroundFadeEnd = Continue;
            }
        }
        protected override Status UpdateNode()
        {
            return m_Status;
        }       
    }
}
