using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Logic.UI
{   
    /// <summary>
    /// Script used for friendsbook display
    /// </summary>
    public class FriendsBook_Display : Displays
    {
        /// <summary>
        /// Get instance of this display
        /// </summary>
        public static FriendsBook_Display Instance
        {
            get;
            protected set;
        }

        #region FIELDS     
        [Header("Post Display")]
        [SerializeField]
        private Button m_NextPost = null;
        [SerializeField]
        private Button m_PreviousPost = null;
        [SerializeField]
        private Image m_PostPhoto;
        [SerializeField]
        private Text m_PostDescription;
        [SerializeField]
        private CommentCard[] m_PostComments;
        [Header("Events Settings")]
        [SerializeField]
        private PostInfo[] m_Posts;
        [Header("Chat")]
        [SerializeField]
        private Transform m_ChatBubblesPivot = null;
        [SerializeField]
        private CommentCard m_ChatBubbleCardPrefab = null;
        #endregion

        #region PROPERTIES
        private int m_CurrentPostIndex = 0;
        public int CurrentPostIndex
        {
            get { return m_CurrentPostIndex; }
            set
            {
                m_CurrentPostIndex = value;
                SetNotificationInfo(m_Posts[m_CurrentPostIndex]);
                m_PreviousPost.interactable = m_CurrentPostIndex > 0;
                m_NextPost.interactable = m_CurrentPostIndex < m_Posts.Length - 1;
            }
        }
        #endregion

        /// <summary>
        /// Go to next Post
        /// </summary>
        public void NextPost()
        {
            CurrentPostIndex++;
        }

        /// <summary>
        /// Go to previous post
        /// </summary>
        public void PreviousPost()
        {
            CurrentPostIndex--;
        }

        /// <summary>
        /// Sets notification info
        /// </summary>        
        private void SetNotificationInfo(PostInfo info)
        {
            m_PostPhoto.sprite = info.notificationImage;
            m_PostDescription.text = info.notification;
            ResetComments();
            for (int cIndex=0; cIndex < info.comments.Length; cIndex++)
            {
                m_PostComments[cIndex].SetCommentInfo(info.comments[cIndex]);
            }
            // check whether there's an event to fire
            if(info.OnNotificationViewed != null)
            {
                info.OnNotificationViewed.Enter();
                info.OnNotificationViewed.Execute();
            }
        }

        private void ResetComments()
        {
            for (int cIndex = 0; cIndex < m_PostComments.Length; cIndex++)
            {
                m_PostComments[cIndex].Reset();
            }
        }

        /// <summary>
        /// Send message on chat
        /// </summary>
        /// <param name="comment"></param>
        public void SendMessage(UserComment message)
        {
            CommentCard newCard = GameObject.Instantiate(m_ChatBubbleCardPrefab, m_ChatBubblesPivot, false);
            newCard.SetCommentInfo(message);
        }

        #region UNITY FUNCTIONS
        /// <summary>
        /// Initialize posts
        /// </summary>
        protected override void Start()
        {
            base.Start();
            CurrentPostIndex = 0;
            m_NextPost.onClick.AddListener(NextPost);
            m_PreviousPost.onClick.AddListener(PreviousPost);
        }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        #endregion
    }
}
