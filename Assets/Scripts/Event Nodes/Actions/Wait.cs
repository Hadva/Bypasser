using UnityEngine;
using System.Collections;
using Logic;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// Used to add a delay in a sequence
/// </summary>
public class Wait : Action
{
    /// <summary>
    /// Enum used to define timer mode
    /// </summary>
    private enum TimerMode
    {
        Default = 0,
        Unscaled
    }

    /// <summary>
    /// Scaling mode of timer
    /// </summary>
    [SerializeField]
    private TimerMode m_TimerMode = TimerMode.Default;
    
    /// <summary>
    /// Stores delay for when mode is constant
    /// </summary>
	[SerializeField]
    private float m_Delay = 3.0f;
   
    /// <summary>
    /// Current time 
    /// </summary>
    private float m_CurrentTime = 0;

    /// <summary>
    /// Set delay
    /// </summary>
    public override void Enter()
    {
        m_CurrentTime = m_Delay;
    }

    protected override Status UpdateNode()
    {
        if (m_CurrentTime <= 0)
        {
            return Status.Success;
        }
        if (m_TimerMode == TimerMode.Default)
        {
            m_CurrentTime -= Time.deltaTime;
        }
        else
        {
            m_CurrentTime -= Time.unscaledDeltaTime;
        }
        return Status.Continue;
    }
}