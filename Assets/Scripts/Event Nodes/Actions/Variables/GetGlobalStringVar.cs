using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Action used to get a global variable
    /// </summary>
    public class GetGlobalStringVar : Action
    {
        /// <summary>
        /// Id of variabel
        /// </summary>
        [SerializeField]
        private string m_VariableId = string.Empty;

        /// <summary>
        /// Instance where variable instance will be stored
        /// </summary>
        [SerializeField]
        private StringVar m_Variable = null;

        protected override Status UpdateNode()
        {
            if(m_VariableId.Length == 0 || m_Variable == null)
            {
                return Status.Error;
            }
            // obtain variable
            StringVar globalString = (StringVar) GlobalVariables.GetVariable<string>(m_VariableId);
            m_Variable.value = globalString.value;
            return Status.Success;
        }
    }
}
