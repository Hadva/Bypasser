using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    public class DisplayChoices : Command
    {    
        [SerializeField]
        private Choice[] m_Choices;

        private Status m_Status = Status.Continue;

        protected override Status UpdateNode()
        {
            m_Status = Status.Continue;
            DisplayManager.instance.DisplayChoices(m_Choices);
            // hook on choice selected event
            DisplayManager.instance.onChoiceSelected = SelectChoice;
            return m_Status;
        }

        /// <summary>
        /// Continue status
        /// </summary>
        private void SelectChoice(int index)
        {
            m_Status = Status.Success;
        }
    }
}
