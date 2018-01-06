using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Action node used to execute an event chain
    /// </summary>
    public class EventChain_Execute : Action
    {
        /// <summary>
        /// event chain to execute
        /// </summary>
        [SerializeField]
        private EventChain m_Chain = null;

        protected override Status UpdateNode()
        {
            if(m_Chain == null)
            {
                return Status.Error;
            }
            m_Chain.Execute();
            return Status.Success;
        }
    }
}
