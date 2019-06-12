using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationMenu : MonoBehaviour
{
	[Header("References")]
	public GameObject window;
	public GameObject overlay;
	public Text title;
	public Text message;
	public TextScanner textScanner;
	public List<TextAsset> textFiles = new List<TextAsset>();

	private void Start()
	{
		textScanner.TextAvailable += SetActive;
	}

	public void Setup(string type)
	{
		title.text = type;
		SetActive(true);
		Enum.TryParse(type, out InfoType infoType);
		Setup(infoType);
	}

	private void Setup(InfoType i)
	{
		foreach (TextAsset a in textFiles)
		{
			if (IsInfoTypeTextAsset(i, a))
			{
				textScanner.AquireTexts(a);
				textScanner.ApplyText(textScanner.GetTexts[textScanner.GetCurrentTextIndex]);
			}
		}
	}

	private void SetActive(bool activate)
	{
		window.SetActive(activate);
		overlay.SetActive(activate);
	}

	private bool IsInfoTypeTextAsset(InfoType type, TextAsset asset)
	{
		return type.ToString() == textScanner.GetFileTitle(asset);
	}

	public enum InfoType { None, Needs, Skills, Work, Battle, Stream, Contest, Progress, Calender, Goal, Items, Accommodations }
}
