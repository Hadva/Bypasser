using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Add character command
    /// </summary>
    public class AddCharacter : Command
    {
        /// <summary>
        /// Character name
        /// </summary>
        [SerializeField] private string m_CharacterName;
        [SerializeField] private int m_ScreenPosition = 0;
        [SerializeField] private bool m_Async = false;

        public override void Enter()
        {
            base.Enter();
            DisplayManager.instance.AddCharacter(m_CharacterName, m_ScreenPosition);
            if(m_Async)
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
