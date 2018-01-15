using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Utilities;
namespace Logic.Gameplay
{
    /// <summary>
    /// Script in charge of handling major tasks of game. Loading/Saving/Transitions
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Obtain instance of game manager
        /// </summary>
        public static GameManager instance
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
        /// Id of player name
        /// </summary>
        [SerializeField]
        private string m_PlayerNameVarId = "PlayerName";

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
            if (instance == null)
            {
                instance = this;
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
            m_PlayerNameVar = GlobalVariables.GetVariable<string>(m_PlayerNameVarId) as StringVar;
            m_PlayerNameVar.value = m_Save.chaptersSave[m_Save.lastSaveSlotPlayed].playerName;
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
