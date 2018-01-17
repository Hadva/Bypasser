using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Abstract class used to create multiple commands
    /// </summary>
    public abstract class Command : Action
    {    
        /// <summary>
        /// Reference of status for command
        /// </summary>
        protected Status m_Status = Status.Success;   

        /// <summary>
        /// Continue status
        /// </summary>
        protected virtual void Continue()
        {
            m_Status = Status.Success;
        }
    }
}
