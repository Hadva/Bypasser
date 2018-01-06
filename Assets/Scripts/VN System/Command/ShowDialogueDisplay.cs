using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Command used to show dialogue display
    /// </summary>
    public class ShowDialogueDisplay : Command
    {
        protected override Status UpdateNode()
        {
            DisplayManager.instance.ShowDialogueDisplay();
            return Status.Success;
        }
    }
}
