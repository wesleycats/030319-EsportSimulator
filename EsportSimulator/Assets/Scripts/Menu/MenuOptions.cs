using System.Collections;
using System;
using UnityEngine;

public class MenuOptions : MonoBehaviour
{
    public ButtonManager buttonManager;

    private void OnEnable()
    {
        buttonManager.EnableAllButtonsOf("Navigation");
    }

    public void Quit()
	{
		Application.Quit();
	}
}
