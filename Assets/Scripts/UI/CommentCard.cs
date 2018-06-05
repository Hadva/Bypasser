using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Logic.UI
{
    /// <summary>
    /// Class used to define comment card
    /// </summary>
    [System.Serializable]
    public class CommentCard : MonoBehaviour
    {        
        private GameObject m_CardPanel;

        [SerializeField]
        private Text userNameDisplay;

        [SerializeField]
        private Image imageDisplay;

        [SerializeField]
        private Text commentDisplay;

        /// <summary>
        /// Set comment card info
        /// </summary>
        public void SetCommentInfo(UserComment commentInfo)
        {
            if (commentInfo.userName == GameManager.Instance.playerNameId)
            {
                commentInfo.userName = GameManager.Instance.GetPlayerName();
            }
            userNameDisplay.text = commentInfo.userName;
            imageDisplay.sprite = commentInfo.userImage;
            commentDisplay.text = commentInfo.userComment;
            m_CardPanel.SetActive(true);
        }

        /// <summary>
        /// Reset comment card
        /// </summary>
        public void Reset()
        {
            userNameDisplay.text = "";
            imageDisplay.sprite = null;
            commentDisplay.text = "";
            m_CardPanel.SetActive(false);
        }

        private void Start()
        {
            m_CardPanel = this.gameObject;
        }
    }
}