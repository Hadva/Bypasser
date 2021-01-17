using UnityEngine;
using System.Collections;
namespace Logic
{
    /// <summary>
    /// Class in charge of handling special scenes animations
    /// </summary>
    public class SpecialScene : MonoBehaviour
    {
        public string SceneName;
        [SerializeField] private string m_SpecialAnimationName = "";
        [SerializeField] private RectTransform m_ImageTransform = null;
        [SerializeField] private Vector2 m_EndPosition = new Vector2(0,-1067);
        [SerializeField] private float m_SceneDuration = 5f;
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
            m_RectTransform = GetComponent<RectTransform>();
            Vector2 imageSize = m_ImageTransform.sizeDelta;
            imageSize.y *= (Screen.height / DisplayManager.instance.OriginalScreenSize.y);
            m_ImageTransform.sizeDelta = imageSize;
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
            StartCoroutine(SceneRoutine());
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

        private IEnumerator SceneRoutine()
        {
            float elapsedTime = 0;
            Vector2 initialPosition = m_ImageTransform.anchoredPosition;
            m_EndPosition *= (Screen.height / DisplayManager.instance.OriginalScreenSize.y);
            while (elapsedTime < m_SceneDuration)
            {
                m_ImageTransform.anchoredPosition = Vector3.Lerp(initialPosition, m_EndPosition, elapsedTime / m_SceneDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            m_ImageTransform.anchoredPosition = m_EndPosition;
            SceneAnimationEnd();
        }
             
        public void SceneAnimationEnd()
        {
            DisplayManager.instance.AnimationEnd();
        }
    }

}