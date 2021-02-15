using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Move character to a new screen position
    /// </summary>
    public class MoveCharacter : Command
    {
        [SerializeField] private string m_CharacterName;
        [SerializeField] private int m_NewScreenPosition;
        [SerializeField] private float m_TransitionTime = 3f;

        public override void Enter()
        {
            base.Enter();
            DisplayManager.instance.MoveCharacter(m_CharacterName, m_NewScreenPosition, m_TransitionTime);           
            DisplayManager.instance.onLastAnimationEnd = Continue;
            m_Status = Status.Continue;
        }

        protected override Status UpdateNode()
        {
            return m_Status;
        }
    }
}
