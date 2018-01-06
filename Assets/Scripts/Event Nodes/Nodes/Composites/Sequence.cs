using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Used to execute a collection of events one after another. 
    /// If the current event node does not return success, execution will stop until this node is updated again.
    /// </summary>
    public class Sequence : EventNode
    {
        /// <summary>
        /// List of event nodes to execute
        /// </summary>
        [SerializeField]
        private EventNode[] m_Nodes = null;

        /// <summary>
        /// Index of current node
        /// </summary>
        private int m_CurrentNodeIndex = 0;

        /// <summary>
        /// Reset and enter first node
        /// </summary>
        public override void Enter()
        {
            m_CurrentNodeIndex = 0;
            m_Nodes[m_CurrentNodeIndex].Enter();
        }

        protected override Status UpdateNode()
        {
            Status nodeStatus = Status.Error;
            while(m_CurrentNodeIndex < m_Nodes.Length)
            {
                m_Nodes[m_CurrentNodeIndex].Execute(ref nodeStatus);
                // handle node status
                switch(nodeStatus)
                {
                    case Status.Success:
                        m_Nodes[m_CurrentNodeIndex].Exit();
                        m_CurrentNodeIndex++;
                        if (m_CurrentNodeIndex < m_Nodes.Length)
                        {
                            m_Nodes[m_CurrentNodeIndex].Enter();
                        }
                        break;

                    case Status.Continue:
                        return Status.Continue;

                    case Status.Fail:
                        return Status.Fail;

                    default:
                        Debug.LogError("Something went wrong when updating node " + m_CurrentNodeIndex);
                        break;                    
                }
            }
            return Status.Success;
        }
    }
}
