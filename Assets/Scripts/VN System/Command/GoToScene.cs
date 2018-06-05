using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Loads scene
    /// </summary>
    public class GoToScene : Command
    {
        [SerializeField]
        private string m_SceneName = "";

        protected override Status UpdateNode()
        {
            GameManager.Instance.LoadScene(m_SceneName);
            return Status.Success;
        }
    }
}
