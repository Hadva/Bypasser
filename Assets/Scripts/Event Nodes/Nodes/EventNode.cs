using UnityEngine;
using System.Collections;
namespace Logic
{
    /// <summary>
    /// Enum used to define status of an event node
    /// </summary>
    public enum Status
    {
        None = 0,
        Success,
        Error,
        Fail,
        Continue
    }

    /// <summary>
    /// Abstract class that will be used to make a diversity of event nodes.
    /// </summary>
    public abstract class EventNode : MonoBehaviour
    {       
        /// <summary>
        /// Runs Enter process of this action.
        /// </summary>
        public virtual void Enter() { }

        /// <summary>
        ///Runs Execute process of this action.
        /// </summary>
        public virtual void Execute()
        {
            UpdateNode();
        }

        public virtual void Execute(ref Status result)
        {
            result = UpdateNode();
        }

        /// <summary>
        ///Runs Exit process of this action.
        /// </summary>
        public virtual void Exit() { }

        /// <summary>
        /// Execute event that will run the event/behaviour on this action
        /// </summary>
        protected abstract Status UpdateNode();
    }
}
