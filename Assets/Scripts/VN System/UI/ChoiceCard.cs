using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Logic.UI
{
    /// <summary>
    /// Used to display a choice
    /// </summary>
    public class ChoiceCard : MonoBehaviour
    {
        /// <summary>
        /// Background image
        /// </summary>
        [SerializeField]
        public Image choiceButtonBackground = null;

        /// <summary>
        /// Button that will be pressed to select choice
        /// </summary>
        [SerializeField]
        public Button choiceButton = null;

        /// <summary>
        /// Label that will display text
        /// </summary>
        [SerializeField]
        public Text choiceLabel = null;

        public bool initialized = false;

    }
}
