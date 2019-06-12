/******************************************************************************************
 * Name: TextScanner.cs
 * Created by: Wesley Cats
 * Created on: 11/04/2019
 * Last Modified: 14/04/2019
 * Description:
 * This script serves 2 purposes. First, it reads the text from the text asset and will 
 * split is according to the given indicators. Second, it applies the chosen text to the 
 * text element.
 ******************************************************************************************/

using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scans and splits the text from the text asset according to the given indicators and applies it to the text element
/// </summary>
public class TextScanner : MonoBehaviour
{
	public Action<bool> TextAvailable;

	private const char resetIndicator = '|';
	private const char endTextIndicator = ';';
	private const char titleIndicator = '#';
	private const char unfocusIndicator = '^';

	[SerializeField] private int currentTextIndex;
	[SerializeField] private List<string> texts = new List<string>();

	[Header("References")]
	public TutorialManager tutorialManager;
	public InformationMenu informationMenu;
	public Text textElement;

	private void Start()
	{
		Reset();
	}

	public void Reset()
	{
		currentTextIndex = 0;
		textElement.text = "";
		texts.Clear();
	}

	/// <summary>
	/// Aquires text from the asset and splits it into a list
	/// </summary>
	/// <param name="textFile"></param>
	public void AquireTexts(TextAsset textFile)
	{
		texts = SplitTextFile(textFile);
	}

	/// <summary>
	/// Advances the text index
	/// </summary>
	public void IncreaseTextIndex()
	{
		currentTextIndex++;

		if (currentTextIndex >= texts.Count)
		{
			Reset();
			TextAvailable(false);
			return;
		}

		ApplyText(texts[currentTextIndex]);
	}

	/// <summary>
	/// Applies the given text to the text element
	/// </summary>
	/// <param name="textToApply"></param>
	public void ApplyText(string textToApply)
	{
		if (textToApply == unfocusIndicator.ToString() && tutorialManager)
		{
			tutorialManager.UnFocusTutorial();
			return;
		}

		textElement.text = textToApply;
	}

	/// <summary>
	/// Splits the different texts based on given indicators
	/// </summary>
	/// <param name="textFile"></param>
	/// <returns></returns>
	public List<string> SplitTextFile(TextAsset textFile)
	{
		List<string> texts = new List<string>();
		string text = textFile.text;

		string textBuffer = "";

		for (int i = 0; i < text.Length; i++)
		{
			char c = text[i];
			switch (c)
			{
				case endTextIndicator:
					texts.Add(textBuffer);
					textBuffer = "";
					continue;

				case resetIndicator:
					textBuffer = "";
					continue;
			}

			textBuffer += text[i];
		}

		return texts;
	}

	/// <summary>
	/// Returns the title of the text file based on given indicator
	/// </summary>
	/// <param name="textFile"></param>
	/// <returns></returns>
	public string GetFileTitle(TextAsset textFile)
	{
		string text = textFile.text;
		string title = "";

		for (int i = 0; i < text.Length; i++)
		{
			char c = text[i];
			switch (c)
			{
				case resetIndicator:
					title = "";
					continue;

				case titleIndicator:
					int index = i + 1;

					while (text[index] != titleIndicator)
					{
						if (text[index] == resetIndicator)
							break;

						title += text[index];
						index++;
					}

					return title;
			}
		}

		return "No title found";
	}

	public int GetCurrentTextIndex { get { return currentTextIndex; } }
	public List<string> GetTexts { get { return texts; } }
}
