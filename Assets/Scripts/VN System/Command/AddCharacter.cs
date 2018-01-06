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

        protected override Status UpdateNode()
        {
            DisplayManager.instance.AddCharacter(m_CharacterName);
            return Status.Success;
        }
    }
}
