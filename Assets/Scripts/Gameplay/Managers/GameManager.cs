using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Utilities;
using UnityEngine.SceneManagement;
namespace Logic
{
    /// <summary>
    /// Used to define gender option
    /// </summary>
    public enum GenderOption
    {
        He = 0,
        She,
        They,
        Custom
    }

    /// <summary>
    /// Script in charge of handling major tasks of game. Loading/Saving/Transitions
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public const string PLAYER_NAME_REPLACE = "$playerName";
        public const string PLAYER_GENDER_REPLACE = "$pronoun";

        /// <summary>
        /// Obtain instance of game manager
        /// </summary>
        public static GameManager Instance
        {
            get;
            protected set;
        }

        /// <summary>
        /// Instance of player save file
        /// </summary>
        public static SaveSlot saveSlot
        {
            get;
            protected set;
        }

        /// <summary>
        /// player save file 
        /// </summary>
        private const string PLAYER_SAVE_FILE = "/SAVE.dat";

        /// <summary>
        /// Save file isntance
        /// </summary>
        private PlayerSave m_Save = null;

        /// <summary>
        /// Main camera in game
        /// </summary>
        public Camera mainCamera = null;

        /// <summary>
        /// Id of player name
        /// </summary>
        [SerializeField]
        public string playerNameId = "PlayerName";

        /// <summary>
        /// Reference to player name var
        /// </summary>
        private StringVar m_PlayerNameVar = null;
           
        /// <summary>
        /// Set player name
        /// </summary>
        public void SetPlayerName(string playerName)
        {
            m_Save.chaptersSave[m_Save.lastSaveSlotPlayed].playerName = playerName;
        }

        /// <summary>
        /// Get player name
        /// </summary>
        public string GetPlayerName()
        {
            return m_Save.chaptersSave[m_Save.lastSaveSlotPlayed].playerName;
        }

        public void SetPlayerGender(GenderOption genderSelected)
        {
            m_Save.chaptersSave[m_Save.lastSaveSlotPlayed].gender = genderSelected;
        }

        /// <summary>
        /// Saves game
        /// </summary>
        public void SaveGame()
        {           
            // update save            
            GameSave.Save(Application.dataPath + PLAYER_SAVE_FILE, m_Save);
        }

        /// <summary>
        /// Load game session at slot index
        /// </summary>
        public void LoadGame(int savedSlotIndex)
        {
            saveSlot = m_Save.chaptersSave[savedSlotIndex];
            m_Save.lastSaveSlotPlayed = savedSlotIndex;
        }

        /// <summary>
        /// Load last game session
        /// </summary>
        public void Continue()
        {
            saveSlot = m_Save.chaptersSave[m_Save.lastSaveSlotPlayed];
        }

        #region UNITY FUNCTIONS
        /// <summary>
        /// Initialize instance of this Game Manager
        /// </summary>
        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
            // load player save file
            m_Save = GameSave.Load(Application.dataPath + PLAYER_SAVE_FILE);
            if (m_Save == null)
            {
                m_Save = new PlayerSave();
                // add first chapter save
                SaveSlot newSaveSlot = new SaveSlot();
                m_Save.chaptersSave = new List<SaveSlot>();
                m_Save.chaptersSave.Add(newSaveSlot);
            }           
        }

        /// <summary>
        /// Initialize properties of Game
        /// </summary>
        protected void Start()
        {
            // obtain player name var
            m_PlayerNameVar = GlobalVariables.GetVariable<string>(playerNameId) as StringVar;
            if (m_PlayerNameVar == null)
                return;
            m_PlayerNameVar.value = m_Save.chaptersSave[m_Save.lastSaveSlotPlayed].playerName;            
        }

        /// <summary>
        /// Loads scene with provided name
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadScene(string sceneName)
        {
            // load scene additive
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// Save game
        /// </summary>
        private void OnApplicationQuit()
        {
            SaveGame();
        }
        #endregion
    }
}
