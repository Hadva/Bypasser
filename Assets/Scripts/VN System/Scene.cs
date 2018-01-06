using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Script in charge of handling commands for one scene
    /// </summary>
    public class Scene : MonoBehaviour
    {
        /// <summary>
        /// List of characters on this scene
        /// </summary>
        [SerializeField]
        private Character[] m_Characters = null;   

        /// <summary>
        /// Commands on this scene
        /// </summary>
        [SerializeField]
        private Command[] m_Commands = null;

        private DisplayManager m_DisplayManager = null;

        /// <summary>
        /// Set current scene on display manager
        /// </summary>
        private void Start()
        {
            m_DisplayManager = DisplayManager.instance;
            m_DisplayManager.currentScene = this;
            StartCoroutine("RunCommands");
        }

        /// <summary>
        /// Get characters in this scene
        /// </summary>
        public List<Character> GetCharacters(RectTransform charactersPivot)
        {          
            // create new list
            List<Character> characterInstances = new List<Character>();
            Character newCharacter = null;
            for(int cIndex = 0; cIndex < m_Characters.Length; cIndex++)
            {
                newCharacter = GameObject.Instantiate(m_Characters[cIndex]);
                newCharacter.rectTransform.SetParent(charactersPivot);
                newCharacter.rectTransform.localScale = new Vector3(0, 0, 0);
                newCharacter.gameObject.SetActive(false);
                characterInstances.Add(newCharacter);
            }
            return characterInstances;            
        }

        private IEnumerator RunCommands()
        {
            Status commandStatus = Status.Error;
            // iterate through commands
            for(int comIndex =0; comIndex < m_Commands.Length; comIndex++)
            {
                m_Commands[comIndex].Enter();
                m_Commands[comIndex].Execute(ref commandStatus);
                // check if it should continue running
                while(commandStatus == Status.Continue)
                {
                    yield return null;
                    m_Commands[comIndex].Execute(ref commandStatus);
                }
                m_Commands[comIndex].Exit();
            }
        }
        
    }
}
 