using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Class used to define player data
    /// </summary>
    [System.Serializable]
    public class PlayerSave 
    {
        /// <summary>
        /// Save slot
        /// </summary>
        public List<SaveSlot> chaptersSave = new List<SaveSlot>();

        /// <summary>
        /// Last save slot played
        /// </summary>
        public int lastSaveSlotPlayed = 0;
    }

    /// <summary>
    /// Class used to define chapter saved data
    /// </summary>
    [System.Serializable]
    public class SaveSlot
    {
        /// <summary>
        /// player name saved
        /// </summary>
        public string playerName;

        /// <summary>
        /// Name of chapter saved
        /// </summary>
        public string chapterName;

        /// <summary>
        /// Name of command saved
        /// </summary>
        public int command;

        /// <summary>
        /// Chapters unlocked
        /// </summary>
        public List<int> chaptersUnlocked = new List<int>();
    }
}
