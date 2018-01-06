using UnityEngine;
using System.Collections;
using Logic;
/// <summary>
/// Used to activate or deactivate an object 
/// </summary>
public class ToggleObject : Action 
{
    // enum defines mode to be used when toggling object
    protected enum Mode 
	{
		Enable=0,
		Disable,
        Toggle,
        BoolVar
	}
	// List of objects to toggle
	[SerializeField]
	protected GameObject[] m_Objects;
    [SerializeField]
    protected float m_DelayBetweenObjects = 0;
	// Toggle mode selected
	[SerializeField]
	protected Mode m_Mode;
    [SerializeField]
    protected BoolVar m_ToggleValue = null;
	protected override Status UpdateNode ()
	{
		switch(m_Mode)
		{
		case Mode.Enable:
			StartCoroutine(ToggleObjects(true));
			break;

		case Mode.Disable:
            StartCoroutine(ToggleObjects(false));
            break;
        case Mode.Toggle:
            StartCoroutine("ToggleObjects");
            break;

         default:
            if(m_ToggleValue == null)
            {
                return Status.Fail;
            }
            StartCoroutine("ToggleObjects", m_ToggleValue.value);
            break;
		}
		return Status.Success;
	}

    protected IEnumerator ToggleObjects()
    {
        foreach(GameObject o in m_Objects)
        {
            o.SetActive(!o.activeSelf);
        }
        yield return null;
    }

	/// <summary>
	/// Activates the objects.
	/// </summary>
	protected IEnumerator ToggleObjects(bool value)
	{
		foreach(GameObject o in m_Objects)
		{            
            o.SetActive(value);
            if (m_DelayBetweenObjects != 0)
            {
                yield return new WaitForSeconds(m_DelayBetweenObjects);
            }
        }
        yield return null;
	}  
}
