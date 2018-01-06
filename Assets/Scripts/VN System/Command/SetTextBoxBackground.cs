using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Set text box background
    /// </summary>
    public class SetTextBoxBackground : Command
    {
        [SerializeField]
        private Sprite m_NewBackground;

        protected override Status UpdateNode()
        {
            DisplayManager.instance.SetTextBackground(m_NewBackground);
            return Status.Success;
        }
    }
}
