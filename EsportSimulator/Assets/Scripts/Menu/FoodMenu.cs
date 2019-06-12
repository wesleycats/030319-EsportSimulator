using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMenu : MonoBehaviour
{
	public enum FoodType { None, Food, Drink }

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
		question.text = "What would you like to " + foodType + "?";
		
		// TODO Change result text accordingly

		switch (foodType)
		{
			case FoodType.Drink:

				break;

			case FoodType.Food:

				break;
		}
	}

	public FoodType GetFoodType { get { return foodType; } }
}
