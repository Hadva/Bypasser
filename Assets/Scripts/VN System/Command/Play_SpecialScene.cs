using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    public class Play_SpecialScene : Command
    {
        [SerializeField]
        private string m_SpecialSceneName = "";

        public override void Enter()
        {
            base.Enter();
            DisplayManager.instance.PlaySpecialScene(m_SpecialSceneName);
            DisplayManager.instance.onLastAnimationEnd = Continue;
            m_Status = Status.Continue;
        }

        protected override Status UpdateNode()
        {
            return m_Status;
        }
    }
}
