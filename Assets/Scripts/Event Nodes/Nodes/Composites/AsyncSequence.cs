using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    public class AsyncSequence : EventNode
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

        protected override Status UpdateNode()
        {
            StartCoroutine("TickNodes");
            return Status.Success;
        }

        /// <summary>
        /// Coroutine used to execute nodes.
        /// </summary>   
        private IEnumerator TickNodes()
        {
            Status nodeStatus = Status.Error;
            m_CurrentNodeIndex = 0;
            m_Nodes[m_CurrentNodeIndex].Enter();
            while (m_CurrentNodeIndex < m_Nodes.Length)
            {
                m_Nodes[m_CurrentNodeIndex].Execute(ref nodeStatus);
                // handle node status
                switch (nodeStatus)
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
                        yield return null;
                        break;

                    case Status.Fail:
                        StopAllCoroutines();
                        break;

                    default:
                        Debug.LogError("Something went wrong when updating node " + m_CurrentNodeIndex);
                        break;
                }
            }
        }
    }
}
