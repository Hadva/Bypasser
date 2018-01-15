using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Logic
{
    public class SetStringVar : VariableAction
    {
        /// <summary>
        /// New value to store in variable
        /// </summary>
        [SerializeField]
        private string m_NewStringValue = "";

        /// <summary>
        /// Instance where variable instance will be stored
        /// </summary>
        [SerializeField,HideInInspector]
        public StringVar variableStore = null;

        public override void Enter()
        {
            if(variableFieldType == eVariableFieldType.Global)
            {
                variableStore = (StringVar)GlobalVariables.GetVariable<string>(variableId);
            }    
        }

        protected override Status UpdateNode()
        {
            if(variableStore == null)
            {
                return Status.Error;
            }
            variableStore.value = m_NewStringValue;
            return Status.Success;
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(SetStringVar))]
    public class SetStringVarEditor : Editor
    {
        SetStringVar self;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            self = target as SetStringVar;
            if (self.variableFieldType == eVariableFieldType.Local)
            {
                self.variableStore = EditorGUILayout.ObjectField("Variable Store", self.variableStore, typeof(StringVar), true) as StringVar;
            }
            else
            {
                self.variableId = EditorGUILayout.TextField("Variable Id", self.variableId);
            }
        }
    }
#endif
}
