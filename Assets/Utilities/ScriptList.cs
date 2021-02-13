using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Logic.Utilities
{
    public class ScriptList : MonoBehaviour
    {
        [SerializeField] private RectTransform m_OptionsPivot;
        [SerializeField] private ScriptList_Button m_OptionPrefab;
        private string scenesDirectory;
        
        private void Start()
        {
            scenesDirectory = Application.dataPath + "/Scenes";
            var info = new DirectoryInfo(scenesDirectory);
            FileInfo[] fileInfo = info.GetFiles();
            string sceneName = "";
            ScriptList_Button newButton = null;
            for(int fIndex =0; fIndex < fileInfo.Length; fIndex++)
            {
                if (fileInfo[fIndex].Name.Contains("Script") && !fileInfo[fIndex].Name.Contains("meta"))
                {
                    sceneName = fileInfo[fIndex].Name.Split(new string[] { ".unity" },System.StringSplitOptions.None)[0];
                    newButton = GameObject.Instantiate(m_OptionPrefab, m_OptionsPivot);
                    newButton.SetSceneName(sceneName);
                }
            }
        }
    }
}
