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
    /// Struct used to define a choice in the game
    /// </summary>
    [System.Serializable]
    public struct Choice
    {
        /// <summary>
        /// Line of text to display
        /// </summary>
        public string line;
        /// <summary>
        /// Decision command
        /// </summary>
        public Command decisionCommand;
    }

    /// <summary>
    /// Script in charge of handling display of vn
    /// </summary>
    public class DisplayManager : MonoBehaviour
    {
        public System.Action onNextLine = null;
        public System.Action<int> onChoiceSelected = null;
        public System.Action onBackgroundFadeEnd = null;
        public System.Action onLastAnimationEnd = null;
        /// <summary>
        /// Single reference of Display Manager
        /// </summary>
        public static DisplayManager instance
        {
            get;
            protected set;
        }
        public Vector2 OriginalScreenSize
        {
            get { return m_OriginalScreenSize; }
        }
        [SerializeField] private Vector2 m_OriginalScreenSize = new Vector2(1920, 1080);
        [SerializeField] private float m_CurrentWrapperHeight = 350f;
        [SerializeField] private RectTransform m_DialogueWrapper = null;
        [SerializeField] private RectTransform m_ChoicesWrapper = null;
        [Header("Display Fields")]     
        [SerializeField] private Image m_MainBackground = null;
        [SerializeField] private RectTransform m_SceneTransition = null;
        [SerializeField] private RectTransform m_OverlapLayer = null;
        [SerializeField] private RectTransform m_CharacterLayer = null;
        [SerializeField] private RectTransform[] m_CharacterScreenPivots;
        private StringVar m_PlayerName = null;
        [Header("Dialogue Display")]
        [SerializeField] private GameObject m_DialogueDisplay = null;
        [SerializeField] private Image m_BoxBackground = null;
        [SerializeField] private Text m_TextDisplay = null;
        [SerializeField] private GameObject m_NameDisplay = null;
        [SerializeField] private Image m_NameBackground = null;
        [SerializeField] private Text m_NameLabel = null;
        [SerializeField] private float m_TextDisplayTime = 0.016f;

        [Header("Choices Display")]
        [SerializeField] private UI.ChoiceCard m_ChoiceCardPrefab = null;
        [SerializeField] private Text m_ChoiceDescription = null;
        [SerializeField] private RectTransform m_ChoiceCardsPanel = null;
        private List<UI.ChoiceCard> m_ChoiceCardsAvailable = null;

        [Header("Change Scene Settings")]
        [SerializeField] private float m_FadeTime = 2f;
        [SerializeField] private Image m_FadeImage = null;

        [Header("Input")]
        [SerializeField] private string m_NextLineInput = "Space";
        private float m_DisplaySpeed = 0.016f;
        /// <summary>
        /// Current scene 
        /// </summary>
        public Scene currentScene
        {
            protected set;
            get;
        }

        private Transform m_CameraTransform = null;
        private Vector3 m_InitialLocalPosition = Vector3.zero;
        private Animator m_CharacterDisplayanimator = null;
        private bool m_IsReading = false;
        /// <summary>
        /// List of characters displays
        /// </summary>
        private Dictionary<string, Character> m_CharactersDisplay = null;
        /// <summary>
        /// List of special scenes 
        /// </summary>
        private Dictionary<string, SpecialScene> m_SpecialScenes = null;

        /// <summary>
        /// Create instance of singleton
        /// </summary>
        private void Awake()
        {
            // ensure there's only one instance
            if (instance == null)
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
            m_PlayerName = (StringVar)GlobalVariables.GetVariable<string>(GameManager.Instance.playerNameId);
            m_CameraTransform = this.transform;
            m_InitialLocalPosition = m_CameraTransform.localPosition;
            if (Screen.height > m_OriginalScreenSize.y)
            {
                float newHeight = (Screen.height / m_OriginalScreenSize.y) * m_CurrentWrapperHeight;
                Vector2 newSize = m_DialogueWrapper.sizeDelta;
                newSize.y = newHeight;
                m_DialogueWrapper.sizeDelta = newSize;
                m_ChoicesWrapper.sizeDelta = newSize;
            }
        }

        /// <summary>
        /// Check for input
        /// </summary>
        private void Update()
        {
            if (Input.GetButtonDown(m_NextLineInput) || Input.GetMouseButtonDown(0))
            {
                if (m_IsReading)
                {
                    m_DisplaySpeed = 0;
                }
                else
                {
                    NextLine();
                }
            }
        }

        /// <summary>
        /// Fire on next line event
        /// </summary>
        public void NextLine()
        {
            if (onNextLine != null)
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
            // check if there's any previous characters
            if (m_CharactersDisplay != null && m_CharactersDisplay.Count != 0)
            {
                foreach (KeyValuePair<string, Character> charDisplay in m_CharactersDisplay)
                {
                    Destroy(charDisplay.Value.gameObject);
                }
            }
            // check if there's any previous scenes
            if(m_SpecialScenes != null && m_SpecialScenes.Count > 0)
            {
                foreach(KeyValuePair<string, SpecialScene> specialScene in m_SpecialScenes)
                {
                    Destroy(specialScene.Value.gameObject);
                }
            }
            // obtain instances of characters in this scene
            m_CharactersDisplay = newScene.GetCharacters(m_CharacterLayer);
            // get instances of special scenes for this scene
            m_SpecialScenes = newScene.GetSpecialScenes(m_OverlapLayer);
            currentScene = newScene;
        }

        /// <summary>
        /// Set main background of display
        /// </summary>     
        public void SetMainBackground(Sprite newBackground, float fadeTime)
        {
            if (fadeTime > 0)
            {
                StartCoroutine(FadeToNewBackground(newBackground, fadeTime));
            }
            else
            {
                m_MainBackground.sprite = newBackground;
            }
        }

        public void SetBackgroundColor(Color newColor, float fadeTime, bool clearImage)
        {
            if(fadeTime > 0)
            {
                StartCoroutine(FadeToNewBackground(newColor, fadeTime, clearImage));
            }
            else
            {
                if (clearImage) m_MainBackground.sprite = null;
                m_MainBackground.color = newColor;
            }
        }

        private IEnumerator FadeToNewBackground(Sprite newBackground, float fadeTime)
        {
            m_SceneTransition.gameObject.SetActive(true);
            Vector2 startTransitionPos = new Vector2(Screen.width * 1.5f, 0);
            Vector3 endTransitionPos = new Vector2(-Screen.width * 1.5f, 0);
            float elapsed = 0;
            bool backGroundSwapped = false;
            m_SceneTransition.anchoredPosition = startTransitionPos;
            // Fade out current background to black
            while (elapsed < fadeTime)
            {
                m_SceneTransition.anchoredPosition = Vector2.Lerp(startTransitionPos, endTransitionPos, elapsed / fadeTime);
                if (!backGroundSwapped && elapsed >= fadeTime * 0.5f)
                {
                    m_MainBackground.sprite = newBackground;
                    backGroundSwapped = true;
                }
                elapsed += Time.fixedDeltaTime;
                yield return null;
            }
            m_SceneTransition.anchoredPosition = endTransitionPos;
            m_SceneTransition.gameObject.SetActive(false);
            // fire on background fade end
            if (onBackgroundFadeEnd != null)
            {
                onBackgroundFadeEnd();
            }
        }

        private IEnumerator FadeToNewBackground(Color newColor, float fadeTime, bool clearImage)
        {
            float elapsed = 0;
            if (clearImage) m_MainBackground.sprite = null;
            Color currentColor = m_MainBackground.color;
            // Fade out current background to black
            while (elapsed < fadeTime)
            {
                m_MainBackground.color = Color.Lerp(currentColor, newColor, elapsed / fadeTime);
                elapsed += Time.fixedDeltaTime;
                yield return null;
            }
            // fire on background fade end
            if (onBackgroundFadeEnd != null)
            {
                onBackgroundFadeEnd();
            }
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
        public void AddCharacter(string characterName, int position)
        {
            m_CharactersDisplay[characterName].SetPivot(m_CharacterScreenPivots[position]);
            m_CharactersDisplay[characterName].Enter();
        }

        /// <summary>
        /// Removes character from screen
        /// </summary>
        public void RemoveCharacter(string characterName)
        {          
            m_CharactersDisplay[characterName].Exit();
        }

        /// <summary>
        /// Move
        /// </summary>
        public void MoveCharacter(string characterName, int positionToMove, float moveTime)
        {
            m_CharactersDisplay[characterName].SetNewPivot(m_CharacterScreenPivots[positionToMove], moveTime);
        }

        public void AnimationEnd()
        {
            if (onLastAnimationEnd != null)
            {
                onLastAnimationEnd();
                onLastAnimationEnd = null;
            }
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
            // make sure to show display
            if (!m_DialogueDisplay.activeSelf) ShowDialogueDisplay();
            m_TextDisplay.text = string.Empty;
            m_DisplaySpeed = 0.016f;
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
            m_IsReading = true;
            int currentCharacter = 0;            
            // replace playername 
            newLine = newLine.Replace(GameManager.PLAYER_NAME_REPLACE, m_PlayerName.value);
            // set character count
            int characterCount = newLine.Length;  
            // display characters
            while (currentCharacter < characterCount)
            {             
                m_TextDisplay.text += newLine[currentCharacter++];
                if (m_DisplaySpeed > 0)
                {
                    yield return new WaitForSecondsRealtime(m_DisplaySpeed);
                }
                               
            }
            m_TextDisplay.text = newLine;
            m_IsReading = false;
        }   

        public void DisplayChoices(string description, Choice[] choices)
        {
            m_ChoiceDescription.text = description;
            m_ChoiceCardsPanel.gameObject.SetActive(true);
            // check if no choice cards have been created
            if (m_ChoiceCardsAvailable == null)
            {
                m_ChoiceCardsAvailable = new List<UI.ChoiceCard>();
            }
            // compare size of list with choices
            int difference = Mathf.Abs(m_ChoiceCardsAvailable.Count - choices.Length);
            UI.ChoiceCard choiceCard = null;
            for (int cIndex = 0; cIndex < difference; cIndex++)
            {
                // instantiate choice card
                choiceCard = GameObject.Instantiate(m_ChoiceCardPrefab);
                // set parent and position
                choiceCard.transform.SetParent(m_ChoiceCardsPanel);
                choiceCard.transform.localPosition = Vector3.zero;
                choiceCard.transform.localRotation = Quaternion.identity;
                choiceCard.transform.localScale = new Vector3(1, 1, 1);      
                // disable choice card
                choiceCard.gameObject.SetActive(false);
                m_ChoiceCardsAvailable.Add(choiceCard);
            }
            // iterate through choices
            for (int cIndex = 0; cIndex < choices.Length; cIndex++)
            {
                // cache choice card
                choiceCard = m_ChoiceCardsAvailable[cIndex];
                // check if it hasn't been init
                if (!choiceCard.initialized)
                {
                    int choiceIndex = cIndex;
                    choiceCard.choiceButton.onClick.AddListener(() => SelectChoice(choiceIndex));
                }
                choiceCard.choiceLabel.text = choices[cIndex].line;
                choiceCard.gameObject.SetActive(true);
            }
        }

        private void SelectChoice(int choiceIndex)
        {
            // hide choice display
            for(int cIndex = 0;cIndex < m_ChoiceCardsAvailable.Count; cIndex++)
            {
                m_ChoiceCardsAvailable[cIndex].gameObject.SetActive(false);
            }
            if(onChoiceSelected != null)
            {
                onChoiceSelected(choiceIndex);
                onChoiceSelected = null;
            }
        }

        
        /// <summary>
        /// Camera shake
        /// </summary>
        public void CameraShake(float magnitude, float duration, ShakeMode shakeMode)
        {
            StartCoroutine(Shake(magnitude, duration, shakeMode));
        }     

        public void CameraCloseUp(string characterName)
        {
            m_CharactersDisplay[characterName].CloseUp();
        }

        public void PlaySpecialScene(string sceneName)
        {
            m_SpecialScenes[sceneName].Play();
        }

        public void EndSpecialScene(string sceneName)
        {
            m_SpecialScenes[sceneName].Hide();
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
