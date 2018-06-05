using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Logic
{
    /// <summary>
    /// Action used to set value to an animator parameter
    /// </summary>
    public class SetAnimatorParameter : Action
    {
        [SerializeField]
        private Animator m_Animator = null;

        public enum ParameterType
        {
            Trigger = 0,
            Integer,
            Float,
            Bool
        }
        [SerializeField]
        private string m_ParameterName = "";
        [SerializeField]
        private ParameterType m_ParameterType;
        /// <summary>
        /// Get type of parameter
        /// </summary>
        public ParameterType ParamType
        {
            get { return m_ParameterType; }
        }       
        [SerializeField, HideInInspector]
        public float floatValue;
        [SerializeField, HideInInspector]
        public int integerValue;
        [SerializeField, HideInInspector]
        public bool boolValue;

        protected override Status UpdateNode()
        {
            if(m_Animator == null)
            {
                return Status.Error;
            }
            
            switch(m_ParameterType)
            {
                case ParameterType.Integer:
                    m_Animator.SetInteger(m_ParameterName, integerValue);
                    break;
                case ParameterType.Float:
                    m_Animator.SetFloat(m_ParameterName, floatValue);
                    break;
                case ParameterType.Bool:
                    m_Animator.SetBool(m_ParameterName, boolValue);
                    break;
                default:
                    m_Animator.SetTrigger(m_ParameterName);
                    break;
            }
            return Status.Success;
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(SetAnimatorParameter))]
    public class SetAnimatorParameterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SetAnimatorParameter self = target as SetAnimatorParameter;
            base.OnInspectorGUI();
            switch(self.ParamType)
            {
                case SetAnimatorParameter.ParameterType.Bool:
                    self.boolValue = EditorGUILayout.Toggle("Value", self.boolValue);
                    break;
                case SetAnimatorParameter.ParameterType.Integer:
                    self.integerValue = EditorGUILayout.IntField("Value", self.integerValue);
                    break;
                case SetAnimatorParameter.ParameterType.Float:                   
                    self.floatValue = EditorGUILayout.FloatField("Value", self.floatValue);
                    break;
            }
        }

    }
#endif
}
