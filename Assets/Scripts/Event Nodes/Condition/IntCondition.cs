using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Condition used to compare two integer variables
    /// </summary>
    public class IntCondition : NumberCondition
    {
        /// <summary>
        /// Left side integer
        /// </summary>
        [SerializeField]
        private IntVar m_LeftSide = null;

        /// <summary>
        /// Right side integer
        /// </summary>
        [SerializeField]
        private IntVar m_RightSide = null;

        protected override bool Equals()
        {
            return m_LeftSide.value == m_RightSide.value;
        }

        protected override bool Higher()
        {
            return m_LeftSide.value > m_RightSide.value;
        }

        protected override bool Lower()
        {
            return m_LeftSide.value < m_RightSide.value;
        }
    }
}
