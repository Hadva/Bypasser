using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Behaviour used to execute a node at start.
    /// </summary>
    [RequireComponent(typeof(EventNode))]
    public class ExecuteNodeAtStart : MonoBehaviour
    {       
        private void Start()
        {
            EventNode eventNode = GetComponent<EventNode>();
            eventNode.Enter();
            eventNode.Execute();
            eventNode.Exit();
        }
    }
}
