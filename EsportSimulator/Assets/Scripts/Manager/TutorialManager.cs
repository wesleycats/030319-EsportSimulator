using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
	public enum LerpType { None, Intro, Outro }

	[SerializeField] private TutorialForm tutorialForm;
	[SerializeField] private Tutorial currentTutorial;
	[SerializeField] private int currentTutorialIndex;
	[SerializeField] private int needsValueGoal;
	[SerializeField] private int skillsValueGoal;

	public System.Action OnPress;

	private Vector3 originalWindowPos;
	private Vector2 originalWindowSize;

	[Header("References")]
	public TextScanner textScanner;
	public Text tutorialTitle;
	public GameObject tutorialWindow;
	public Canvas overlay;
	public Object mainScene;
	public LerpColor switchOverlay;
	public ButtonManager buttonManager;
	public GameData gameData;

	private void Start()
	{
		buttonManager.DisableAllButtons();
		switchOverlay.Lerp(1);
		StartCoroutine(LerpDelayer(LerpType.Intro));

		originalWindowPos = tutorialWindow.GetComponent<RectTransform>().localPosition;
		originalWindowSize = new Vector2(tutorialWindow.GetComponent<RectTransform>().rect.width, tutorialWindow.GetComponent<RectTransform>().rect.height);

		ResetTutorialIndex();
		StartTutorial(tutorialForm.tutorialParts[currentTutorialIndex]);
	}

	private IEnumerator LerpDelayer(LerpType lerpType)
	{
		yield return new WaitForSeconds(1f);

		if (!switchOverlay.LerpActivated)
		{
			switch (lerpType)
			{
				case LerpType.Intro:
					buttonManager.EnableAllButtonsOf("Tutorial");
					break;

				case LerpType.Outro:
					SceneManager.LoadScene(mainScene.name);
					break;

				default:
					Debug.LogWarning("No lerp type given");
					break;
			}
		}
		else
			StartCoroutine(LerpDelayer(lerpType));
	}

	public void StartTutorial(Tutorial tutorial)
	{
		if (tutorial == null || tutorial.done) return;

		if (tutorialForm.tutorialParts.Count == 1) buttonManager.DisableAllButtons("Tutorial");

		currentTutorial = tutorial;
		tutorialWindow.SetActive(true);
		overlay.gameObject.SetActive(true);
		tutorialTitle.text = tutorial.tutorialName;
		textScanner.AquireTexts(tutorial.textFile);
		textScanner.ApplyText(textScanner.GetTexts[textScanner.GetCurrentTextIndex]);

		SetupFocusObjects(tutorial);
		SetFocusObjects(tutorial);

		// Sets or resets custom transform
		if (GetCustomTransform(textScanner, tutorial) != null)
			SetCustomTransform(tutorialWindow, GetCustomTransform(textScanner, tutorial));
		else
			ResetWindow(tutorialWindow.GetComponent<RectTransform>());
	}

	public void UnPauseTutorial()
	{
		currentTutorial.pause = false;
	}

	/// <summary>
	/// Focuses the tutorial, so the player cannot perform actions
	/// </summary>
	public void FocusTutorial()
	{
		tutorialWindow.SetActive(true);
		overlay.gameObject.SetActive(true);
	}

	/// <summary>
	/// Unfocuses the tutorial, so the player can perform actions
	/// </summary>
	public void UnFocusTutorial()
	{
		tutorialWindow.SetActive(false);
		overlay.gameObject.SetActive(false);
	}

	/// <summary>
	/// Call method for buttons
	/// </summary>
	public void AdvanceTutorialClick()
	{
		AdvanceTutorial(currentTutorial);
	}

	/// <summary>
	/// Advances inside given (current) tutorial
	/// </summary>
	/// <param name="tutorial"></param>
	public void AdvanceTutorial(Tutorial tutorial)
	{
		if (tutorial == null || tutorial.pause || tutorial.done) return;

		if (textScanner.GetCurrentTextIndex == textScanner.GetTexts.Count-1)
		{
			FinishTutorialPart(tutorial);
			return;
		}

		textScanner.IncreaseTextIndex();
		SetFocusObjects(tutorial);

		// Sets custom transform if needed
		if (GetCustomTransform(textScanner, tutorial) != null)
			SetCustomTransform(tutorialWindow, GetCustomTransform(textScanner, tutorial));

		List<FocusObject> currentFocusedObjects = new List<FocusObject>();

		// Adds all current focusobjects to a list
		foreach (FocusObject f in tutorial.focusObjects)
		{
			if (f.focusTextIndex == textScanner.GetCurrentTextIndex)
			{
				currentFocusedObjects.Add(f);
				f.gObject.SetActive(true);
				//Debug.Log("focus object added: " + f.gObject.name);
			}

		}

		if (currentFocusedObjects.Count == 0)
		{
			foreach (FocusObject f in tutorial.focusObjects)
			{
				ResetFocusObject(f);
			}

			return;
		}

		foreach (FocusObject f in currentFocusedObjects)
		{
			foreach (FocusObject fo in tutorial.focusObjects)
			{
				if (fo.gObject != f.gObject && fo.focusTextIndex != f.focusTextIndex)
				{
					ResetFocusObject(fo);
					continue;
				}
				else
				{
					fo.gObject.SetActive(true);

					/*if (textScanner.GetCurrentTextIndex == 5)
					{
						Debug.Log(f.gObject.name);
						Debug.Log(f.focusTextIndex);
						Debug.Log(fo.gObject.name);
						Debug.Log(fo.focusTextIndex);
					}*/
				}
			}
		}
	}

	/// <summary>
	/// Advances to the next tutorial
	/// </summary>
	/// <param name="tutorial"></param>
	public void FinishTutorialPart(Tutorial tutorial)
	{
		if (GetNextTutorial(tutorialForm) == null)
		{
			switchOverlay.Lerp(1);
			StartCoroutine(LerpDelayer(LerpType.Outro));
			gameData.tutorialDone = true;
			return;
		}

		textScanner.Reset();
		tutorial.done = true;
		tutorialWindow.GetComponent<RectTransform>().localPosition = originalWindowPos;

		foreach (FocusObject o in tutorial.focusObjects)
		{
			//ResetFocusObject(o);
			
			// Checks if focus object has button component to disable it
			if (o.gObject.GetComponent<Button>())
				o.gObject.GetComponent<Button>().interactable = false;
			
			// Checks if childs in focus object has button component to disable it
			if (o.gObject.transform.childCount > 0)
			{
				for (int i = 0; i < o.gObject.transform.childCount; i++)
				{
					if (o.gObject.transform.GetChild(i).GetComponent<Button>())
						o.gObject.transform.GetChild(i).GetComponent<Button>().interactable = false;
				}
			}
		}

		tutorial = GetNextTutorial(tutorialForm);
		StartTutorial(tutorial);
	}

	public void SetupFocusObjects(Tutorial tutorial)
	{
		foreach (FocusObject o in tutorial.focusObjects)
		{
			// Saves if object needs to stay active after object is not in focus anymore
			if (!o.gObject.activeSelf)
				o.wasActive = false;
			else
				o.wasActive = true;

			if (!o.gObject.GetComponent<Canvas>())
			{
				Debug.LogWarning(o.gObject.name + " at index " + tutorial.focusObjects.IndexOf(o) + " has no canvas component");
				return;
			}

			o.originalOrder = o.gObject.GetComponent<Canvas>().sortingOrder;
		}
	}

	/// <summary>
	/// Sets focus objects to given parameters
	/// </summary>
	/// <param name="focusObject"></param>
	/// <param name="tutorial"></param>
	public void SetFocusObjects(Tutorial tutorial)
	{
		for (int i = 0; i < tutorial.focusObjects.Count; i++)
		{
			if (tutorial.focusObjects[i].focusTextIndex != textScanner.GetCurrentTextIndex) continue;

			tutorial.pause = tutorial.focusObjects[i].waitForInput;

			// Applies different sorting order to focused object
			if (tutorial.focusObjects[i].gObject != overlay.gameObject)
			{
				if (!tutorial.focusObjects[i].isCustomSortOrder)
					tutorial.focusObjects[i].gObject.GetComponent<Canvas>().sortingOrder = overlay.sortingOrder + 1;
				else
					tutorial.focusObjects[i].gObject.GetComponent<Canvas>().sortingOrder = tutorial.focusObjects[i].customSortOrder;
			}

			// Checks if button in object and sets those buttons interactable accordingly
			if (tutorial.focusObjects[i].gObject.GetComponent<Button>())
				tutorial.focusObjects[i].gObject.GetComponent<Button>().interactable = tutorial.focusObjects[i].interactable;

			// Checks if button in children and sets those buttons interactable accordingly
			if (tutorial.focusObjects[i].gObject.transform.childCount > 0)
			{
				for (int j = 0; j < tutorial.focusObjects[i].gObject.transform.childCount; j++)
					if (tutorial.focusObjects[i].gObject.transform.GetChild(j).GetComponent<Button>())
						tutorial.focusObjects[i].gObject.transform.GetChild(j).GetComponent<Button>().interactable = tutorial.focusObjects[i].interactable;
			}
		}
	}

	/// <summary>
	/// Gets custom transform accoring the current tutorial text index
	/// </summary>
	/// <param name="textScanner"></param>
	/// <param name="tutorial"></param>
	/// <returns></returns>
	public CustomTransform GetCustomTransform(TextScanner textScanner, Tutorial tutorial)
	{
		for (int i = 0; i < tutorial.customTransforms.Count; i++)
		{
			CustomTransform customTransform = tutorial.customTransforms[i];

			if (textScanner.GetCurrentTextIndex != customTransform.textIndex) continue;

			return customTransform;
		}

		return null;
	}

	/// <summary>
	/// Sets tutorial window to given parameters of custom tranform
	/// </summary>
	/// <param name="tutorialWindow"></param>
	/// <param name="customTransform"></param>
	public void SetCustomTransform(GameObject tutorialWindow, CustomTransform customTransform)
	{
		if (customTransform.newSize)
			tutorialWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(customTransform.width, customTransform.height);

		if (customTransform.newPos)
			tutorialWindow.GetComponent<RectTransform>().localPosition = customTransform.position;
	}

	/// <summary>
	/// Gets next tutorial in tutorial list
	/// </summary>
	/// <param name="tutorials"></param>
	/// <returns></returns>
	public Tutorial GetNextTutorial(TutorialForm tutorials)
	{
		++currentTutorialIndex;

		if (tutorials.customOrder)
		{
			foreach (Tutorial t in tutorials.tutorialParts)
				if (t.orderIndex == currentTutorialIndex)
				{
					return t;
				}

		}
		else
		{
			foreach (Tutorial tu in tutorials.tutorialParts)
				if (tutorials.tutorialParts.IndexOf(tu) == currentTutorialIndex)
				{
					return tu;
				}
		}
		
		return null;
	}

	public void ResetTutorialIndex()
	{
		currentTutorialIndex = 0;
	}

	public void ResetFocusObject(FocusObject fo)
	{
		fo.gObject.SetActive(fo.wasActive);

		if (!fo.gObject.GetComponent<Canvas>() || fo.gObject.name == "TutorialOverlay")	return;

		fo.gObject.GetComponent<Canvas>().sortingOrder = fo.originalOrder;
	}

	public void ResetWindow(RectTransform window)
	{
		window.transform.localPosition = originalWindowPos;
		window.sizeDelta = originalWindowSize;
	}

	#region Getters & Setters

	public Tutorial GetCurrentTutorial { get { return currentTutorial; } }
	public int GetNeedsValueGoal { get { return needsValueGoal; } }
	public int GetSkillsValueGoal { get { return skillsValueGoal; } }

	#endregion
}
