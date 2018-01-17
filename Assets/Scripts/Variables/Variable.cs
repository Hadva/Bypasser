using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Enum used to define variable field types
    /// </summary>
    public enum eVariableFieldType
    {
        Local = 0,
        Global = 1,
        Constant
    }

    /// <summary>
    /// Class used to define a variable
    /// </summary>
    public abstract class Variable<T> : MonoBehaviour
    {
        /// <summary>
        /// Event fired when value is changed
        /// </summary>
        public System.Action<T> onValueChanged = null;

        /// <summary>
        /// Id of this variable
        /// </summary>
        [SerializeField]
        private string m_Id;

        /// <summary>
        /// Get id of this variable
        /// </summary>
        public string id
        {
            get { return m_Id; }
        }

        /// <summary>
        /// Value to store in variable
        /// </summary>
        [SerializeField]
        protected T m_Value;

        [SerializeField]
        protected bool m_GlobalVariable = false;

        /// <summary>
        /// Value of this variable
        /// </summary>
        public virtual T value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
                if(onValueChanged != null)
                {
                    onValueChanged(m_Value);
                }
            }
        }        

        /// <summary>
        /// Register if its a global variable
        /// </summary>
        private void Awake()
        {
            if(m_GlobalVariable)
            {
                GlobalVariables.Add(id, this);              
            }
        }

        /// <summary>
        /// Make sure variable is removed
        /// </summary>
        private void OnDestroy()
        {
            if (m_GlobalVariable)
            {
                GlobalVariables.Remove(id);
            }
        }
    }
}
