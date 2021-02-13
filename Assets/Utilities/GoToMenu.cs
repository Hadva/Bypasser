using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic.Utilities
{
    public class GoToMenu : MonoBehaviour
    {
        [SerializeField] private string m_MenuScene;

        public void LoadMenuScene()
        {
            GameManager.Instance.LoadScene(m_MenuScene);
        }
    }
}
