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

        protected override Status UpdateNode()
        {
            DisplayManager.instance.SetMainBackground(m_NewBackground);
            return Status.Success;
        }
    }
}
