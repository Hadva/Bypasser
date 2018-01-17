using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Logic
{
    /// <summary>
    /// New line of text to display. Status is set to continue during enter, it will change whenever input has been detected.
    /// </summary>
    public class DisplayLine : Command
    {
        /// <summary>
        /// New line of text to display
        /// </summary>
        [SerializeField]
        private string m_NewLine = string.Empty;

        public eVariableFieldType variableFieldType = eVariableFieldType.Local;
    
        /// <summary>
        /// Name to display
        /// </summary>
        [SerializeField, HideInInspector]
        public StringVar nameVar = null;

        [SerializeField, HideInInspector]
        public string characterName = null;

        public override void Enter()
        {
            if(variableFieldType != eVariableFieldType.Constant)
            {
                if (nameVar != null)
                {
                    characterName = nameVar.value;
                }
                else
                {
                    characterName = string.Empty;
                }
            }  
            // set name display      
            if (characterName.Length > 0)
            {
                DisplayManager.instance.ToggleNameDisplay(true);
                DisplayManager.instance.DisplayName(characterName);
            }
            else
            {
                DisplayManager.instance.ToggleNameDisplay(false);
            }
            // set new line
            DisplayManager.instance.DisplayNewLine(m_NewLine);
            m_Status = Status.Continue;
            DisplayManager.instance.onNextLine = Continue;
        }

        protected override Status UpdateNode()
        {           
            // check if it should go to next line            
            return m_Status;
        }       
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(DisplayLine))]
    public class DisplayLineEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DisplayLine self = target as DisplayLine;
            if(self.variableFieldType != eVariableFieldType.Constant)
            {
                self.nameVar = EditorGUILayout.ObjectField("Character Name", self.nameVar, typeof(StringVar), true) as StringVar;
            } 
            else
            {
                self.characterName = EditorGUILayout.TextField("Character Name", self.characterName);
            }
        }
    }
#endif
}
