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
        [SerializeField]
        private string m_CharacterName;
        [SerializeField]
        private int m_ScreenPosition = 0;

        public override void Enter()
        {
            base.Enter();
            DisplayManager.instance.AddCharacter(m_CharacterName, m_ScreenPosition);
            DisplayManager.instance.onLastCharacterAnimationEnd = Continue;
            m_Status = Status.Continue;
        }

        protected override Status UpdateNode()
        {
            return m_Status;
        }
    }
}
