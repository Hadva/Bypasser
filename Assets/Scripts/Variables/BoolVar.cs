using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Used to store a bool value
    /// </summary>
    public class BoolVar : Variable<bool>
    {        
        public override bool value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
                onValueChanged(m_Value);
            }
        }
    }
}
