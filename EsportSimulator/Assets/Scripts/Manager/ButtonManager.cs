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

    public void DisEnableAllButtons(bool enable, string exceptionTag)
    {
        foreach (Button b in allButtons)
        {
            if (b.tag == exceptionTag) continue;

            b.interactable = enable;
        }
    }

    public void DisEnableAllButtons(bool enable)
    {
        foreach (Button b in allButtons)
        {
            b.interactable = enable;
        }
    }
}
