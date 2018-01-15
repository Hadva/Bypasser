using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Used to define variable actions
    /// </summary>
    public abstract class VariableAction : Action
    {
        /// <summary>
        /// Variable field type
        /// </summary>
        [SerializeField]
        public eVariableFieldType variableFieldType = eVariableFieldType.Local;

        /// <summary>
        /// Variable id
        /// </summary>
        [SerializeField, HideInInspector]
        public string variableId = "";

    }
}
