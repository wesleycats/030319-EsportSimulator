/******************************************************************************************
 * Name: TextApplier.cs
 * Created by: Wesley Cats
 * Created on: 11/04/2019
 * Last Modified: 11/04/2019
 * Description:
 * This script serves 2 purposes. First, it reads the text from the text asset and will 
 * split is according to the given indicators. Second, it applies the chosen text to the 
 * text element.
 ******************************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Splits the text from the text asset according to the given indicators and applies it to the text element
/// </summary>
public class TextApplier : MonoBehaviour
{
	private const char resetIndicator = '|';
	private const char endTextIndicator = ';';

	[SerializeField] private int currentTextIndex;
	[SerializeField] private List<string> texts = new List<string>();

	[Header("References")]
	public TutorialManager tutorialManager;
	public Text textElement;
	public TextAsset textFile;

	private void Start()
	{
		texts = SplitTextFile(textFile);
		currentTextIndex = 0;
		ApplyText(texts[currentTextIndex]);

		tutorialManager.OnPress += IncreaseTextIndex;
	}

	public void IncreaseTextIndex()
	{
		Debug.Log(currentTextIndex);
		if (currentTextIndex >= texts.Count)
		{
			Debug.LogWarning("Exceeds the amount of texts");
			return;
		}
		ApplyText(texts[currentTextIndex]);
		currentTextIndex++;
	}

	public void ApplyText(string textToApply)
	{
		textElement.text = textToApply;
	}

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
}
