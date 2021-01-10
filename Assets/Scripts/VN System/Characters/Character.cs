using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Class in charge of handling character animations and events.
    /// </summary>
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// Name of this character
        /// </summary>
        [SerializeField]
        public string CharacterName;

        /// <summary>
        /// Name of entry animation
        /// </summary>
        [SerializeField]
        private string m_EntryAnimationName = "";

        /// <summary>
        /// Name of exit animation
        /// </summary>
        [SerializeField]
        private string m_ExitAnimationName = "";

        [SerializeField]
        private string m_CloseUpAnimationName = "Close Up";

        private Animator m_Animator = null;
        /// <summary>
        /// Instance of rect transform of this character
        /// </summary>
        private RectTransform m_RectTransform = null;
        /// <summary>
        /// Get the rect transform of this character
        /// </summary>
        public RectTransform rectTransform
        {
            get
            {
                return m_RectTransform;
            }
        }


        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_RectTransform = GetComponent<RectTransform>();
        }

        public void Enter()
        {
            SetAnimationTrigger(m_EntryAnimationName);
        }

        public void Exit()
        {
            SetAnimationTrigger(m_ExitAnimationName);
        }

        public void CloseUp()
        {
            SetAnimationTrigger(m_CloseUpAnimationName);
        }

        public void SetPivot(RectTransform newPivot)
        {
            m_RectTransform.SetParent(newPivot);
            m_RectTransform.localScale = Vector3.one;
        }           

        /// <summary>
        /// Set trigger for character animation
        /// </summary>
        /// <param name="triggerName"></param>
        public void SetAnimationTrigger(string triggerName)
        {
            if(m_Animator != null)
            {
                m_Animator.SetTrigger(triggerName);
            }
        }

        /// <summary>
        /// Sets character animation bool
        /// </summary>
        /// <param name="paramName">Name of bool parameter</param>
        /// <param name="value">Value of bool parameter</param>
        public void SetAnimationBool(string paramName, bool value)
        {
            if (m_Animator != null)
            {
                m_Animator.SetBool(paramName, value);
            }
        }            

        public void CharacterAnimationEnd()
        {
            DisplayManager.instance.AnimationEnd();
        }
    }
}
