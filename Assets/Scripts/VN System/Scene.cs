using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    [System.Serializable]
    public class CommandBlock
    {
        public string blockTitle = "";
        public Command[] commands;
    }
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
        private CommandBlock[] m_CommandBlock = null;

        [SerializeField]
        private float m_DelayAtStart = 1f;

        private DisplayManager m_DisplayManager = null;

        /// <summary>
        /// Set current scene on display manager
        /// </summary>
        private void Start()
        {
            m_DisplayManager = DisplayManager.instance;
            m_DisplayManager.SetCurrentScene(this);
            StartCoroutine("RunCommands");
        }

        /// <summary>
        /// Get characters in this scene
        /// </summary>
        public Dictionary<string,Character> GetCharacters(RectTransform charactersPivot)
        {
            // create new list
            Dictionary<string, Character> characterInstances = new Dictionary<string, Character>();
            Character newCharacter = null;
            for(int cIndex = 0; cIndex < m_Characters.Length; cIndex++)
            {
                newCharacter = GameObject.Instantiate(m_Characters[cIndex]);
                newCharacter.rectTransform.SetParent(charactersPivot);
                newCharacter.rectTransform.localPosition = Vector3.zero;
                newCharacter.rectTransform.localScale = new Vector3(1, 1, 1);               
                characterInstances.Add(newCharacter.CharacterName,newCharacter);
            }
            return characterInstances;            
        }

        /// <summary>
        /// Runs series of commands
        /// </summary>
        private IEnumerator RunCommands()
        {
            yield return new WaitForSeconds(m_DelayAtStart);
            Status commandStatus = Status.Error;
            // iterate through blocks
            Command[] commands;
            for (int bIndex = 0; bIndex < m_CommandBlock.Length; bIndex++)
            {
                commands = m_CommandBlock[bIndex].commands;
                // iterate through commands
                for (int comIndex = 0; comIndex < commands.Length; comIndex++)
                {
                    commands[comIndex].Enter();
                    commands[comIndex].Execute(ref commandStatus);
                    // check if it should continue running
                    while (commandStatus == Status.Continue)
                    {
                        yield return null;
                        commands[comIndex].Execute(ref commandStatus);
                    }
                    commands[comIndex].Exit();
                }
            }
        }
        
    }
}
 