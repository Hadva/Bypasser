using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Action used to apply force to given rigidbody
    /// </summary>
    public class ApplyForce : Action
    {
        /// <summary>
        /// Enum used to define force direction
        /// </summary>
        private enum ForceVector
        {
            Forward = 0,
            Right,
            Up,
            Random
        }
        /// <summary>
        /// Rigidbody to apply force to
        /// </summary>
        [SerializeField]
        private Rigidbody m_Rigidbody = null;
        /// <summary>
        /// Force mode
        /// </summary>
        [SerializeField]
        private ForceMode m_ForceMode = ForceMode.Acceleration;
        /// <summary>
        /// Force vector
        /// </summary>
        [SerializeField]
        private ForceVector m_Vector = ForceVector.Forward;
        /// <summary>
        /// Speed of force
        /// </summary>
        [SerializeField]
        private float m_Speed = 5f;
        /// <summary>
        /// Spread to be applied to force vector
        /// </summary>
        [SerializeField]
        private float m_SpreadRange = 0.1f;

        protected override Status UpdateNode()
        {
            // validate
            if(m_Rigidbody == null)
            {
                return Status.Error;
            }
            Vector3 direction = Vector3.zero;
            switch(m_Vector)
            {
                case ForceVector.Forward:
                    direction = m_Rigidbody.transform.forward;
                    direction.x += Random.Range(-m_SpreadRange, m_SpreadRange);
                    direction.y += Random.Range(-m_SpreadRange, m_SpreadRange);
                    break;
                case ForceVector.Right:
                    direction = m_Rigidbody.transform.right;
                    direction.z += Random.Range(-m_SpreadRange, m_SpreadRange);
                    direction.y += Random.Range(-m_SpreadRange, m_SpreadRange);
                    break;
                case ForceVector.Up:
                    direction = m_Rigidbody.transform.up;
                    direction.x += Random.Range(-m_SpreadRange, m_SpreadRange);
                    direction.z += Random.Range(-m_SpreadRange, m_SpreadRange);
                    break;
                case ForceVector.Random:
                    direction = Random.insideUnitSphere;
                    direction.x += Random.Range(-m_SpreadRange, m_SpreadRange);
                    direction.y += Random.Range(-m_SpreadRange, m_SpreadRange);
                    break;
            }
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.AddForce(direction * m_Speed, m_ForceMode);
            return Status.Success;         
        }
    }
}
