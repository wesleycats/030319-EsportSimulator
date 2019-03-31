using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchContent : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject[] contentPages;

    public void SwitchPage()
    {
        SwitchToPage(dropdown.value, contentPages);
    }

    private void SwitchToPage(int index, GameObject[] pages)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            if (i == index)
                pages[i].SetActive(true);
            else
                pages[i].SetActive(false);
        }
    }
}
