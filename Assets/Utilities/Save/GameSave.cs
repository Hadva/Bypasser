using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Logic.Utilities
{
    /// <summary>
    /// Utilities script used to save info in files for game
    /// </summary>
    public class GameSave
    {
        /// <summary>
        /// Save file at provided directory
        /// </summary>
        public static void Save(string fileDirectory, PlayerSave data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = null;
            if (File.Exists(fileDirectory))
            {
                file = File.Open(fileDirectory, FileMode.Open);
            }
            else
            {
                file = File.Create(fileDirectory);
            }
            formatter.Serialize(file, data);
            file.Close();            
        }        

        /// <summary>
        /// Load file at provided directory
        /// </summary>
        public static PlayerSave Load(string fileDirectory)
        {
            // check if file exists
            if(File.Exists(fileDirectory))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.OpenRead(fileDirectory);
                PlayerSave data = formatter.Deserialize(file) as PlayerSave;
                file.Close();
                return data;
            }
            return null;
        }
    }
}
