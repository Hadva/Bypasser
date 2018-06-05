using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic.Gameplay
{
    /// <summary>
    /// Execute given action
    /// </summary>
    public class RunEventNode : Command
    {
        [SerializeField]
        private EventNode m_EventNode = null;

        protected override Status UpdateNode()
        {
            if(m_EventNode == null)
            {
                return Status.Error;
            }
            m_EventNode.Enter();
            m_EventNode.Execute();
            m_EventNode.Exit();
            return Status.Success;
        }
    }
}
