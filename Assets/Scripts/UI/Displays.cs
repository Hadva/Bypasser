using UnityEngine;
using UnityEngine.UI;
namespace Logic.UI
{
    public class Displays : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] m_Displays = null;

        [SerializeField]
        private Button m_BackButton = null;

        private GameObject m_CurrentDisplay = null;
        private int m_PreviousDisplayIndex = 0;
        private int m_CurrentDisplayIndex = 0;

        /// <summary>
        /// Go to Display at index
        /// </summary>      
        public void GoToDisplay(int displayIndex)
        {
            if(m_CurrentDisplayIndex == displayIndex)
            {
                return;
            }
            if(m_CurrentDisplay != null)
            {
                m_CurrentDisplay.SetActive(false);
            }
            m_PreviousDisplayIndex = m_CurrentDisplayIndex;
            m_CurrentDisplayIndex = displayIndex;
            m_CurrentDisplay = m_Displays[m_CurrentDisplayIndex];
            m_CurrentDisplay.SetActive(true);
        }

        /// <summary>
        /// Go To Previous Display
        /// </summary>
        private void GoToPreviousDisplay()
        {            
            GoToDisplay(m_PreviousDisplayIndex);
        }

        protected virtual void Start()
        {
            m_BackButton.onClick.AddListener(GoToPreviousDisplay);
        }
    }
}
