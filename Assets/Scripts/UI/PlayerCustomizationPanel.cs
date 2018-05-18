using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Logic.UI
{
    public class PlayerCustomizationPanel : MonoBehaviour
    {
        private const int PLAYERCHARNUMBER = 3;
        public static System.Action onNameChanged = null;
        public static System.Action onGenderSelected = null;

        public static PlayerCustomizationPanel Instance
        {
            get;
            protected set;
        }    
        #region Fields    

        [Header("Name Input")]
        /// <summary>
        /// Id of variable to store player name
        /// </summary>
        [SerializeField]
        private string m_PlayerNameVarId = "";

        /// <summary>
        /// Panel of player name
        /// </summary>
        [SerializeField]
        private GameObject m_NamePanel = null;

        [SerializeField]
        private InputField m_PlayerNameField = null;

        /// <summary>
        /// Fired when a name entered is not valid
        /// </summary>
        [SerializeField]
        private GameObject m_ConfirmNamePanel = null;

        /// <summary>
        /// Label to display confirmation message
        /// </summary>
        [SerializeField]
        private Text m_ConfirmNameMessage = null;

        /// <summary>
        /// Fired when a name entered is not valid
        /// </summary>
        [SerializeField]
        private GameObject m_InvalidNamePanel = null;

        [Header("Gender Selection")]   
        /// <summary>
        /// Reference to pronoun selection panel
        /// </summary>
        [SerializeField]
        private GameObject m_PronounSelectionPanel = null;
        #endregion

        /// <summary>
        /// Toggle name panel
        /// </summary>
        public void ToggleNamePanel(bool enabled)
        {
            m_NamePanel.SetActive(enabled);
        }

        /// <summary>
        /// Set player name
        /// </summary>
        public void CheckPlayerName()
        {
            // validate player name
            if(m_PlayerNameField.text.Length < PLAYERCHARNUMBER)
            {
                m_InvalidNamePanel.SetActive(true);
                return;
            }
            // update name validation lable
            m_ConfirmNameMessage.text = "Is " + m_PlayerNameField.text + " your name?";
            // validate name
            m_ConfirmNamePanel.SetActive(true);
        }

        /// <summary>
        /// Set player name after being validated
        /// </summary>  
        public void UpdatePlayerName()
        {
            m_ConfirmNamePanel.SetActive(false);
            m_NamePanel.SetActive(false);
            GlobalVariables.GetVariable<string>(m_PlayerNameVarId).value = m_PlayerNameField.text;
            GameManager.instance.SetPlayerName(m_PlayerNameField.text);  
            if(onNameChanged != null)
            {
                onNameChanged();
                onNameChanged = null;
            }
        }

        /// <summary>
        /// Toggle gender option panel
        /// </summary>
        public void ToggleGenderOptionPanel(bool enabled)
        {
            m_PronounSelectionPanel.SetActive(enabled);
        }

        /// <summary>
        /// Set player's gender option
        /// </summary>
        public void SetGenderOption(int genderOption)
        {
            GameManager.instance.SetPlayerGender((GenderOption)genderOption);
            ToggleGenderOptionPanel(false);
            if(onGenderSelected != null)
            {
                onGenderSelected();
            }
        }

        #region UNITY_FUNCTIONS
        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
        }
        #endregion
    }
}
