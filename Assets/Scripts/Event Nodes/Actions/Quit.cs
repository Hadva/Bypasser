using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{ 
    /// <summary>
    /// Quits this application
    /// </summary>
    public class Quit : Action
    {
        protected override Status UpdateNode()
        {
            Application.Quit();
            return Status.Success;
        }
    }
}
