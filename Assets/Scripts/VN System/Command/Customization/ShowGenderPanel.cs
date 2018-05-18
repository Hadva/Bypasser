using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    public class ShowGenderPanel : Command
    {
        public override void Enter()
        {
            UI.PlayerCustomizationPanel.onGenderSelected += Continue;
            UI.PlayerCustomizationPanel.Instance.ToggleGenderOptionPanel(true);
            m_Status = Status.Continue;
        }

        protected override Status UpdateNode()
        {
            return m_Status;
        }

    }
}
