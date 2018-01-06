using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Camera shake command
    /// </summary>
    public class ShakeCamera : Command
    {
        /// <summary>
        /// Magnitude of shake
        /// </summary>
        [SerializeField]
        private float m_Magnitude = 1;

        /// <summary>
        /// Magnitude of shake
        /// </summary>
        [SerializeField]
        private float m_Duration = 1;

        [SerializeField]
        private ShakeMode m_Mode = ShakeMode.AllDirections;

        protected override Status UpdateNode()
        {
            DisplayManager.instance.CameraShake(m_Magnitude, m_Duration, m_Mode);
            return Status.Success;
        }
    }
}
