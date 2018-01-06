using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Used to define different types of number conditions
    /// </summary>
    public abstract class NumberCondition : Condition
    {
        /// <summary>
        /// Enum that defines number conditons
        /// </summary>
        public enum Condition
        {
            Equals = 0,
            Higher,
            Lower,
        }

        /// <summary>
        /// Condition to check
        /// </summary>
        [SerializeField]
        protected Condition m_Condition = Condition.Equals;
     
        protected override Status UpdateNode()
        {
            bool result = false;
            switch(m_Condition)
            {
                case Condition.Equals:
                    result = Equals();
                    break;
                case Condition.Higher:
                    result = Higher();
                    break;
                case Condition.Lower:
                    result = Lower();
                    break;                           
            }         
            // check if result is positive  
            if(result)
            {
                return Status.Success;
            }
            return Status.Fail;
        }

        /// <summary>
        /// Check if both numbers are equal
        /// </summary>
        protected abstract bool Equals();

        /// <summary>
        /// Check if left side is higher than right side
        /// </summary>
        protected abstract bool Higher();

        /// <summary>
        /// Check if left side is lower than right side
        /// </summary>
        protected abstract bool Lower();
    }
}
