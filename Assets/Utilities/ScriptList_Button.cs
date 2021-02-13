using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Utilities
{
    public class ScriptList_Button : MonoBehaviour
    {
        [SerializeField] private Text m_ButtonLabel;

        private string m_SceneName;

        public void SetSceneName(string sceneName)
        {
            m_SceneName = sceneName;
            m_ButtonLabel.text = m_SceneName;
        }

        public void LoadScene()
        {
            GameManager.Instance.LoadScene(m_SceneName);
        }
    }
}

