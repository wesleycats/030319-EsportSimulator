using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	[SerializeField] private TutorialForm tutorialForm;
	[SerializeField] private Tutorial currentTutorial;
	[SerializeField] private int currentIndex;

	public System.Action OnPress;

	[Header("References")]
	public TextApplier textApplier;
	public Text tutorialTitle;

	public void StartTutorial(Tutorial tutorial)
	{
		if (tutorial.done) return;

		//TODO set all tutorial parts
		//TODO create all tutorial text
		//TODO change sort order of some game images according to the tutorial part
	}

	public void FinishTutorialPart(Tutorial tutorial)
	{
		tutorial.done = true;
		currentIndex++;
		currentTutorial = GetCurrentTutorial(tutorialForm, currentIndex);
	}

	public void NextText()
	{
		OnPress();
	}

	public Tutorial GetCurrentTutorial(TutorialForm tutorials, int currentIndex)
	{
		if (tutorials.customOrder)
		{
			foreach (Tutorial t in tutorials.tutorialParts)
				if (t.orderIndex == currentIndex)
					return t;
		}
		else
		{
			foreach (Tutorial tu in tutorials.tutorialParts)
				if (tutorials.tutorialParts.IndexOf(tu) == currentIndex)
					return tu;
		}

		return null;
	}
}
