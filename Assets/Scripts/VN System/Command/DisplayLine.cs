using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// New line of text to display. Status is set to continue during enter, it will change whenever input has been detected.
    /// </summary>
    public class DisplayLine : Command
    {      
        /// <summary>
        /// Name to display
        /// </summary>
        [SerializeField]
        private StringVar nameVar = null;
         
        /// <summary>
        /// New line of text to display
        /// </summary>
        [SerializeField]
        private string m_NewLine = string.Empty;

        private Status m_Status = Status.Continue;

        public override void Enter()
        {
            base.Enter();
            if (nameVar != null)
            {
                DisplayManager.instance.ToggleNameDisplay(true);
                DisplayManager.instance.DisplayName(nameVar.value);
            }
            else
            {
                DisplayManager.instance.ToggleNameDisplay(false);
            }
            DisplayManager.instance.DisplayNewLine(m_NewLine);
            m_Status = Status.Continue;
            DisplayManager.instance.onNextLine = Continue;
        }

        protected override Status UpdateNode()
        {           
            // check if it should go to next line            
            return m_Status;
        }

        /// <summary>
        /// Continue status
        /// </summary>
        private void Continue()
        {
            m_Status = Status.Success;
        }
    }
}
