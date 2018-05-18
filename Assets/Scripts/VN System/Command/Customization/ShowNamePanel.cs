using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Shows name customization panel
    /// </summary>
    public class ShowNamePanel : Command
    {
        public override void Enter()
        {
            UI.PlayerCustomizationPanel.onNameChanged += Continue;
            UI.PlayerCustomizationPanel.Instance.ToggleNamePanel(true);
            m_Status = Status.Continue;
        }

        protected override Status UpdateNode()
        {
            return m_Status;
        }
    }
}
