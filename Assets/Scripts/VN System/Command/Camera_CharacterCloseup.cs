using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    public class Camera_CharacterCloseup : Command
    {
        [SerializeField]
        private string m_CharacterName = "";

        public override void Enter()
        {
            base.Enter();
            DisplayManager.instance.CameraCloseUp(m_CharacterName);
            DisplayManager.instance.onLastCharacterAnimationEnd = Continue;
            m_Status = Status.Continue;
        }

        protected override Status UpdateNode()
        {
            return m_Status;
        }
    }
}
