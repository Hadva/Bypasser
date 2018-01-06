using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Command used to hide dialogue display
    /// </summary>
    public class HideDialogueDisplay : Command
    {
        protected override Status UpdateNode()
        {
            DisplayManager.instance.HideDialogueDisplay();
            return Status.Success;
        }
    }
}
