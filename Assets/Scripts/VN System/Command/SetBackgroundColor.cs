using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// command used to set a new background on display
    /// </summary>
    public class SetBackgroundColor : Command
    {
        /// <summary>
        /// new background 
        /// </summary>
        [SerializeField]
        private Color m_BackgroundColor = Color.white;

        /// <summary>
        /// time it will take to fade into new image
        /// </summary>
        [SerializeField]
        private float m_FadeTime = 5f;

        [SerializeField]
        private bool m_ClearImage = false;

        public override void Enter()
        {
            DisplayManager.instance.SetBackgroundColor(m_BackgroundColor, m_FadeTime, m_ClearImage);
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
