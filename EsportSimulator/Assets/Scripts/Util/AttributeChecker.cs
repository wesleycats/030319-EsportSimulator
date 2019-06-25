using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks the value of the attributes and gives feedback based on chosen function
/// </summary>
public class AttributeChecker : MonoBehaviour
{
	public enum Attributes { None, Needs, Skills }

	private List<int> needs = new List<int>();
	private List<int> skills = new List<int>();

	[Header("References")]
	public GameManager gameManager;
	public TutorialManager tutorialManager;
	public ObjectManager objectManager;
	public GameObject needsMenu;
	public GameObject darkOverlay;

	private void Start()
	{
		if (gameManager)
		{
			needs.Add(gameManager.GetTiredness);
			needs.Add(gameManager.GetHunger);
			needs.Add(gameManager.GetThirst);
			skills.Add(gameManager.GetGameKnowledge);
			skills.Add(gameManager.GetMechanics);
			skills.Add(gameManager.GetTeamPlay);
		}
	}

	/// <summary>
	/// Call function to check needs
	/// </summary>
	public void CheckNeeds()
	{
		CheckAttributeValues(needs, tutorialManager.GetNeedsValueGoal, Attributes.Needs);
	}

	/// <summary>
	/// Call function to check skills
	/// </summary>
	public void CheckSkills()
	{
		CheckAttributeValues(skills , tutorialManager.GetSkillsValueGoal, Attributes.Skills);
	}

	/// <summary>
	/// Continues tutorial if given attribute values are equal to the check value
	/// </summary>
	/// <param name="list"></param>
	/// <param name="checkValue"></param>
	public void CheckAttributeValues(List<int> list, int checkValue, Attributes attributes)
	{
		switch (attributes)
		{
			case Attributes.Needs:
				list[0] = gameManager.GetTiredness;
				list[1] = gameManager.GetHunger;
				list[2] = gameManager.GetThirst;
				break;

			case Attributes.Skills:
				list[0] = gameManager.GetGameKnowledge;
				list[1] = gameManager.GetMechanics;
				list[2] = gameManager.GetTeamPlay;
				break;
		}

		if (IsAllValuesEqualTo(checkValue, list))
		{
			tutorialManager.AdvanceTutorial(tutorialManager.GetCurrentTutorial);
			tutorialManager.FocusTutorial();
			objectManager.DisableAllObjectsOf("SubMenu");
			needsMenu.SetActive(false);
			darkOverlay.SetActive(false);
		}
	}

	/// <summary>
	/// Checks if all list items are equal to the given value
	/// </summary>
	/// <param name="value"></param>
	/// <param name="list"></param>
	/// <returns></returns>
	public bool IsAllValuesEqualTo(int value, List<int> list)
	{
		foreach (int i in list)
		{
			if (i != value) return false;
		}

		return true;
	}
}
