using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public Button[] allButtons;

    private void Awake()
    {
        allButtons = Resources.FindObjectsOfTypeAll<Button>();
    }

    public void EnableAllButtons(string exceptionTag)
    {
		//Debug.Log("all except + " + exceptionTag);
        foreach (Button b in allButtons)
        {
            if (b.tag == exceptionTag) continue;

            b.interactable = true;
        }
    }

    public void EnableAllButtons()
    {
		//Debug.Log("all");
		foreach (Button b in allButtons)
        {
            b.interactable = true;
        }
    }

    public void EnableAllButtonsOf(string tag)
    {
		//Debug.Log("all of " + tag);
		foreach (Button b in allButtons)
        {
            if (b.tag != tag) continue;

            b.interactable = true;
        }
    }

    public void DisableAllButtons(string exceptionTag)
    {
        foreach (Button b in allButtons)
        {
            if (b.tag == exceptionTag) continue;

            b.interactable = false;
        }
	}

	public void DisableAllButtons()
    {
        foreach (Button b in allButtons)
        {
            b.interactable = false;
        }
	}

	public void DisableAllButtonsOf(string tag)
    {
        foreach (Button b in allButtons)
        {
            if (b.tag != tag) continue;

            b.interactable = false;
        }
		Debug.Log("off");
	}
}
