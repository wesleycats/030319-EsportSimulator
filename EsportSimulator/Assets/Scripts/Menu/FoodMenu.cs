using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMenu : MonoBehaviour
{
	public enum FoodType { None, eat, drink }

	[SerializeField] private FoodType foodType;

	[Header("References")]
	public List<Button> buttons = new List<Button>();
	public List<Text> resultTexts = new List<Text>();
	public Text windowTitle;
	public Text question;

	public void SetFoodType(string foodTypeName)
	{
		SetupMenu(foodTypeName);
	}

	private void SetupMenu(string foodTypeName)
	{
		windowTitle.text = foodTypeName;
		foodType = (FoodType)System.Enum.Parse(typeof(FoodType), foodTypeName);
		question.text = "What would you like to " + foodType.ToString() + "?";

		for (int i = 0; i < resultTexts.Count; i++)
		{
			resultTexts[i].text = "";
			resultTexts[i].text = "-$" + 10 * (i+1) + ", -" + 25 * (i+1) + "% ";
		}

		switch (foodType)
		{
			case FoodType.eat:

				for (int i = 0; i < resultTexts.Count; i++)
					resultTexts[i].text += "hunger";
				break;

			case FoodType.drink:

				for (int i = 0; i < resultTexts.Count; i++)
					resultTexts[i].text += "thirst";
				break;
		}
	}

	public FoodType GetFoodType { get { return foodType; } }
}
