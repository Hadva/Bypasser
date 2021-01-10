using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Class in charge of handling special scenes animations
    /// </summary>
    [RequireComponent(typeof(Animation))]
    public class SpecialScene : MonoBehaviour
    {
        public string SceneName;
        [SerializeField] private string m_SpecialAnimationName = "";

        private Animation m_SceneAnimation = null;
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
            m_SceneAnimation = GetComponent<Animation>();
            m_RectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {            
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Play special scene animation
        /// </summary>
        public void Play()
        {
            gameObject.SetActive(true);
            m_SceneAnimation.Play(m_SpecialAnimationName);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetPivot(RectTransform newPivot)
        {
            m_RectTransform.SetParent(newPivot);
            m_RectTransform.localScale = Vector3.one;
        }      
             
        public void SceneAnimationEnd()
        {
            DisplayManager.instance.AnimationEnd();
        }
    }

}