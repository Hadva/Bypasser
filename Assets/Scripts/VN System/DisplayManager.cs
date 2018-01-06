using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// In charge of handling display on screen
/// </summary>
namespace Logic
{
    /// <summary>
    /// Shake camera
    /// </summary>
    public enum ShakeMode
    {
        Horizontal = 0,
        Vertical,
        Forward,
        AllDirections,
    }
    /// <summary>
    /// Script in charge of handling display of vn
    /// </summary>
    public class DisplayManager : MonoBehaviour
    {
        public System.Action onNextLine = null;
        /// <summary>
        /// Single reference of Display Manager
        /// </summary>
        public static DisplayManager instance
        {
            get;
            protected set;
        }

        [Header("Display Fields")]
        [SerializeField]
        private Camera m_MainCamera = null;
        [SerializeField]
        private Image m_MainBackground = null;
        [SerializeField]
        private RectTransform m_CharacterLayer = null;
        [Header("Dialogue Display")]
        [SerializeField]
        private GameObject m_DialogueDisplay = null;
        [SerializeField]
        private Image m_BoxBackground = null;       
        [SerializeField]
        private Text m_TextDisplay = null;
        [SerializeField]
        private GameObject m_NameDisplay = null;
        [SerializeField]
        private Image m_NameBackground = null;
        [SerializeField]
        private Text m_NameLabel = null;
        [SerializeField]
        private float m_TextDisplayTime = 0.016f;
        [Header("Change Scene Settings")]
        [SerializeField]
        private float m_FadeTime = 2f;
        [SerializeField]
        private Image m_FadeImage = null;
        [Header("Input")]
        [SerializeField]
        private string m_NextLineInput = "Space";
        /// <summary>
        /// Current scene 
        /// </summary>
        public Scene currentScene
        {
            set;
            protected get;
        }

        private Transform m_CameraTransform = null;
        private Vector3 m_InitialLocalPosition = Vector3.zero;
        private Animator m_CharacterDisplayanimator = null; 
        
        /// <summary>
        /// List of characters displays
        /// </summary>
        private List<Character> m_CharactersDisplay = null;

        /// <summary>
        /// Create instance of singleton
        /// </summary>
        private void Awake()
        {
            // ensure there's only one instance
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Initialize camera transform
        /// </summary>
        private void Start()
        {
            m_CameraTransform = this.transform;
            m_InitialLocalPosition = m_CameraTransform.localPosition;
        }

        /// <summary>
        /// Check for input
        /// </summary>
        private void Update()
        {
            if(Input.GetButtonDown(m_NextLineInput))
            {
                NextLine();
            }
        }

        /// <summary>
        /// Fire on next line event
        /// </summary>
        public void NextLine()
        {
            if(onNextLine != null)
            {
                onNextLine();
                onNextLine = null;
            }
        }

        /// <summary>
        /// Set new scene
        /// </summary>
        public void SetCurrentScene(Scene newScene)
        {
            // check if list of characters is null
            if(m_CharactersDisplay == null)
            {
                m_CharactersDisplay = new List<Character>();
            }
            // check if there's any previous characters
            else if(m_CharactersDisplay.Count == 0)
            {
                for(int cIndex = 0; cIndex < m_CharactersDisplay.Count; cIndex++)
                {
                    Destroy(m_CharactersDisplay[cIndex].gameObject);
                }
            }
            // obtain instances of characters in this scene
            m_CharactersDisplay = newScene.GetCharacters(m_CharacterLayer);
            currentScene = newScene;
        }

        /// <summary>
        /// Set main background of display
        /// </summary>     
        public void SetMainBackground(Sprite newBackground)
        {
            m_MainBackground.sprite = newBackground;
        }

        /// <summary>
        /// Set text box background
        /// </summary>      
        public void SetTextBackground(Sprite newBackground)
        {
            m_BoxBackground.sprite = newBackground;
        }

        /// <summary>
        /// Adds character on screen
        /// </summary>
        public void AddCharacter(string characterName)
        {

        }

        /// <summary>
        /// Removes character from screen
        /// </summary>
        public void RemoveCharacter(string characterName)
        {

        }

        /// <summary>
        /// Toggle name display
        /// </summary>        
        public void ToggleNameDisplay(bool enabled)
        {
            m_NameDisplay.SetActive(enabled);
        }

        /// <summary>
        /// Show character name on dialogue display
        /// </summary>
        /// <param name="characterName"></param>
        public void DisplayName(string characterName)
        {
            m_NameLabel.text = characterName;
        }     

        /// <summary>
        /// Display ne line of text
        /// </summary>
        public void DisplayNewLine(string newLine)
        {
            m_TextDisplay.text = string.Empty;
            StartCoroutine("DisplayLine", newLine);
        }

        /// <summary>
        /// Show dialogue display
        /// </summary>
        public void ShowDialogueDisplay()
        {
            m_DialogueDisplay.SetActive(true);
        }

        /// <summary>
        /// Hides dialogue display
        /// </summary>
        public void HideDialogueDisplay()
        {
            m_DialogueDisplay.SetActive(false);
        }

        /// <summary>
        /// Display line of text as text typer
        /// </summary>
        private IEnumerator DisplayLine(string newLine)
        {
            int currentCharacter = 0;
            int characterCount = newLine.Length;
            while (currentCharacter < characterCount)
            {
                m_TextDisplay.text += newLine[currentCharacter++];
                yield return new WaitForSeconds(0.016f);
            }            
        }   
        
        /// <summary>
        /// Camera shake
        /// </summary>
        public void CameraShake(float magnitude, float duration, ShakeMode shakeMode)
        {
            StartCoroutine(Shake(magnitude, duration, shakeMode));
        }     

        /// <summary>
        /// Shake camera with given magnitude, duration and shake mode
        /// </summary>
        private IEnumerator Shake(float magnitude, float duration, ShakeMode shakeMode)
        {
            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float percentComplete = elapsed / duration;
                float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
                // map value to [-1, 1]
                float x = Random.value * 2.0f - 1.0f;
                float y = Random.value * 2.0f - 1.0f;
                float z = Random.value * 2.0f - 1.0f;
                x *= magnitude * damper;
                y *= magnitude * damper;
                z *= magnitude * damper;
                if (shakeMode == ShakeMode.Horizontal)
                {
                    m_CameraTransform.localPosition = new Vector3(x, m_InitialLocalPosition.y, z);
                }
                else if (shakeMode == ShakeMode.Vertical)
                {
                    y = Mathf.Abs(y);
                    m_CameraTransform.localPosition = new Vector3(m_InitialLocalPosition.x, y, m_InitialLocalPosition.z);
                }
                else if (shakeMode == ShakeMode.AllDirections)
                {
                    m_CameraTransform.localPosition = new Vector3(x, y, z);
                }
                else
                {
                    m_CameraTransform.localPosition = new Vector3(m_InitialLocalPosition.x, m_InitialLocalPosition.y, z);
                }
                yield return null;
            }
            m_CameraTransform.localPosition = m_InitialLocalPosition;
        }

        /// <summary>
        /// Coroutines that handles scene transition
        /// </summary>
        private IEnumerator SceneTransition()
        {
            yield return null;
        }
    }
}
