using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Logic
{
    /// <summary>
    /// Obtains value from input field and stores it in provided variable
    /// </summary>
    public class GetInputFieldValue : VariableAction
    {
        /// <summary>
        /// Input field reference
        /// </summary>
        [SerializeField]
        private InputField m_InputField = null;

        /// <summary>
        /// String var where value of input field will be stored
        /// </summary>
        [SerializeField, HideInInspector]
        public StringVar variableStore = null;

        public override void Enter()
        {
            if(variableFieldType == eVariableFieldType.Global)
            {
                variableStore = (StringVar) GlobalVariables.GetVariable<string>(variableId);
            }
        }

        protected override Status UpdateNode()
        {
            if(m_InputField == null || variableStore == null)
            {
                return Status.Error;
            }
            variableStore.value = m_InputField.text;
            return Status.Success;   
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GetInputFieldValue))]
    public class GetInputFieldValueEditor: Editor
    {
        GetInputFieldValue self;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            self = target as GetInputFieldValue;
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
