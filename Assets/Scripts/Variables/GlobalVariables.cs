using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    /// <summary>
    /// Singleton class in charge of storing global variables
    /// </summary>
    public class GlobalVariables
    {      
        /// <summary>
        /// Dictionary used to store variables by their id
        /// </summary>
        [SerializeField]
        private static Dictionary<string, object> m_Variables = new Dictionary<string, object>();
      
        /// <summary>
        /// Add variable to dictionary
        /// </summary>
        /// <typeparam name="T">Type of variable</typeparam>
        /// <param name="variableId">id of the variable</param>
        /// <param name="value">variable to store in dictionary</param>
        public static void Add<T>(string variableId, Variable<T> value)
        {
            m_Variables.Add(variableId, value);
        }

        /// <summary>
        /// Get variable with provided id
        /// </summary>
        /// <typeparam name="T">Type of variable</typeparam>
        /// <param name="variableId">id of variable</param>
        /// <returns>Variable of provided id if one is found, otherwise it returns null</returns>
        public static Variable<T> GetVariable<T>(string variableId)
        {
            // make sure dictionary is initialized and that it contains id
            if(m_Variables == null || m_Variables.Count == 0 || !m_Variables.ContainsKey(variableId))
            {
                return null;
            }
            return m_Variables[variableId] as Variable<T>;
        }

        /// <summary>
        /// Obtain variable
        /// </summary>
        /// <param name="variableId"></param>
        public static void Remove(string variableId)
        {
            if(m_Variables == null || m_Variables.Count == 0)
            {
                return;
            }
            m_Variables.Remove(variableId);
        }
    }
}
