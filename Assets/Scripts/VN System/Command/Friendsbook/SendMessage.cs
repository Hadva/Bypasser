using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic.Gameplay
{
    /// <summary>
    /// Send message on friendsbook
    /// </summary>
    public class SendMessage : Command
    {
        [SerializeField]
        private UI.UserComment m_Message;

        protected override Status UpdateNode()
        {
            UI.FriendsBook_Display.Instance.SendMessage(m_Message);
            return Status.Success;
        }
    }
}
