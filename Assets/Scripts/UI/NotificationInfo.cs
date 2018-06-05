using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic.UI
{
    [System.Serializable]
    public struct UserComment
    {
        [SerializeField]
        public string userName;
        [SerializeField]
        public Sprite userImage;
        [SerializeField, TextArea]
        public string userComment;
    }

    /// <summary>
    /// Class used to setup notifications
    /// </summary>
    [System.Serializable]
    public class PostInfo
    {
        [SerializeField]
        public Sprite notificationImage;
        [SerializeField, TextArea]
        public string notification = "";
        [SerializeField]
        public UserComment[] comments;
        [SerializeField]
        private EventNode m_OnNotificationViewed = null;
        /// <summary>
        /// Check if the notification has been seen
        /// </summary>
        public EventNode OnNotificationViewed
        {
            get { return m_OnNotificationViewed; }
        }
    }
}
