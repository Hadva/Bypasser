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
        [SerializeField]
        private string m_CharacterName = "";

        protected override Status UpdateNode()
        {
            DisplayManager.instance.RemoveCharacter(m_CharacterName);
            return Status.Success;
        }
    }
}
