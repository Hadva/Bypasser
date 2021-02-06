using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Removes character from display
    /// </summary>
    public class RemoveCharacter : Command
    {
        /// <summary>
        /// Name of character to display
        /// </summary>
        [SerializeField] private string m_CharacterName = "";
        [SerializeField] private bool m_Async = false;

        public override void Enter()
        {
            base.Enter();
            DisplayManager.instance.RemoveCharacter(m_CharacterName);
            if (m_Async)
            {
                m_Status = Status.Success;
                return;
            }
            DisplayManager.instance.onLastAnimationEnd = Continue;
            m_Status = Status.Continue;
        }

        protected override Status UpdateNode()
        {
           
            return m_Status;
        }
    }
}
