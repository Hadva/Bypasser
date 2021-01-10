using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Logic
{
    public class End_SpecialScene : Command
    {
        [SerializeField] private string m_SpecialSceneName = "";
        protected override Status UpdateNode()
        {
            DisplayManager.instance.EndSpecialScene(m_SpecialSceneName);
            return Status.Success;
        }
    }
}
