using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Delay time
    /// </summary>
    public class Delay : Command
    {
        [SerializeField]
        private float m_DelayTime = 2f;

        private float m_SuccessTime = 0;

        public override void Enter()
        {
            m_SuccessTime = Time.time + m_DelayTime;
        }

        protected override Status UpdateNode()
        {
            if (m_SuccessTime > Time.time) return Status.Continue;

            return Status.Success;
        }
    }
}
