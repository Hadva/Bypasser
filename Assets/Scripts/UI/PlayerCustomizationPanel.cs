using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Logic.Gameplay;
namespace Logic.UI
{
    public class PlayerCustomizationPanel : MonoBehaviour
    {
        private const int PLAYERCHARNUMBER = 3;
        /// <summary>
        /// Used to define gender option
        /// </summary>
        public enum eGenderOption
        {
            He = 0,
            She,
            They,
            Custom
        }
        #region Fields
        /// <summary>
        /// Scene to load after customization is done
        /// </summary>
        [SerializeField]
        private string m_LoadSceneAfterCustomization = null;

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
        /// Id of variabel to store gender 
        /// </summary>
        [SerializeField]
        private string m_GenderNameVarId = "";
   
        /// <summary>
        /// Reference to pronoun selection panel
        /// </summary>
        [SerializeField]
        private GameObject m_PronounSelectionPanel = null;

        /// <summary>
        /// Reference to custom pronoun panel
        /// </summary>
        [SerializeField]
        private GameObject m_CustomPronounPanel = null;
        #endregion

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
            GameManager.LoadScene(m_LoadSceneAfterCustomization);
        }

        /// <summary>
        /// Set player's gender option
        /// </summary>
        public void SetGenderOption(eGenderOption genderOption)
        {
            switch(genderOption)
            {
                case eGenderOption.He:
                    SetCustomGender("He");
                    break;

                case eGenderOption.She:
                    SetCustomGender("She");
                    break;

                case eGenderOption.They:
                    SetCustomGender("They");
                    break;

                default:
                    SetCustomGender("He");
                    break;
            }
        }

        public void SetCustomGender(string gender)
        {

        }       
    }
}
