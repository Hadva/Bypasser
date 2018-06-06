using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Logic.UI
{
    /// <summary>
    /// Set whether a button is interactable
    /// </summary>
    public class SetButtonInteractable : Action
    {
        [SerializeField] private Button m_Button = null;

        [SerializeField] private bool m_IsInteractable = false;

        protected override Status UpdateNode()
        {
            if (m_Button == null) return Status.Error;

            m_Button.interactable = m_IsInteractable;
            return Status.Success;
        }
    }
}
