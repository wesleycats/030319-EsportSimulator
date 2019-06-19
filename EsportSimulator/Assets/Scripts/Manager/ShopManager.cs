using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
	private Item.Type chosenItemType;
	private Item.Quality chosenItemQuality;
	private Accommodation.Type chosenAccommodationType;

	[Header("References")]
    public ArtManager artManager;
    public UIManager uiManager;
    public ResultManager resultManager;
    public GameManager gameManager;
    public PlayerData playerData;

	public GameObject badItemQuality;
	public GameObject standardItemQuality;
	public GameObject goodItemQuality;
	public GameObject excellentItemQuality;
    public Text itemTitle;
	private Text[] results = new Text[4];
	private Button[] buttons = new Button[4];

	public void BuyItem(Item item)
    {
		if (!gameManager.IsMoneyHighEnough(item.cost))
		{
			Debug.LogWarning("Not Enough Money");
			return;
		}

		foreach (Item i in gameManager.CurrentItems)
		{
			if (item.type != i.type) continue;

			gameManager.CurrentItems.Remove(i);
			break;
		}

		foreach (Skill s in item.skills)
		{
			switch (s.type)
			{
				case Skill.Type.GameKnowledge:
					gameManager.IncreaseGameKnowledge(s.amount);
					break;

				case Skill.Type.Mechanics:
					gameManager.IncreaseTeamPlay(s.amount);
					break;

				case Skill.Type.TeamPlay:
					gameManager.IncreaseMechanics(s.amount);
					break;
			}
		}

		gameManager.CurrentItems.Add(item);
		gameManager.DecreaseMoney(item.cost);
		artManager.UpdateItems(gameManager.CurrentItems);
		uiManager.UpdateItems(uiManager.allItemTexts, gameManager.CurrentItems);
		ApplyCurrentItems(gameManager.CurrentItems, item.type);
	}

	public void BuyItem()
	{
		BuyItem(GetItem(chosenItemType, chosenItemQuality));
	}

	public void ApplyCurrentItems(List<Item> currentItems, Item.Type currentType)
	{
		foreach (Item i in currentItems)
		{
			if (currentType != i.type) continue;

			switch (i.quality)
			{
				case Item.Quality.Bad:
					buttons[0].interactable = false;
					break;

				case Item.Quality.Standard:
					buttons[0].interactable = false;
					buttons[1].interactable = false;
					break;

				case Item.Quality.Good:
					buttons[0].interactable = false;
					buttons[1].interactable = false;
					buttons[2].interactable = false;
					break;

				case Item.Quality.Excellent:
					buttons[0].interactable = false;
					buttons[1].interactable = false;
					buttons[2].interactable = false;
					buttons[3].interactable = false;
					break;
			}
			return;
		}
	}

	public void ResetButtons()
	{
		foreach (Button b in buttons)
			b.interactable = true;
	}

	public void ApplyItemTitle(Text itemTitle, string text)
	{
		itemTitle.text = text;
	}

	public void ApplyItemResults(Item.Type type)
	{
		for (int i = 0; i < results.Length; i++)
		{
			results[i].text = "";
		}

		List<int> gameKnowledge = new List<int>();
		List<int> mechanics = new List<int>();
		List<int> teamPlay = new List<int>();
		List<int> cost = new List<int>();

		foreach (ItemForm f in playerData.GetAllItems)
		{
			if (f.itemType != type.ToString()) continue;

			for (int i = 0; i < f.qualities.Count; i++)
			{
				if (f.qualities[i].quality == Item.Quality.Default) continue;

				foreach (Skill s in f.qualities[i].skills)
				{
					switch (s.type)
					{
						case Skill.Type.GameKnowledge:
							gameKnowledge.Add(s.amount);
							break;

						case Skill.Type.Mechanics:
							mechanics.Add(s.amount);
							break;

						case Skill.Type.TeamPlay:
							teamPlay.Add(s.amount);
							break;
					}
				}

				cost.Add(f.qualities[i].cost);
			}
		}

		for (int i = 0; i < results.Length; i++)
		{
			switch (type)
			{
				case Item.Type.Guide:
					results[i].text = "+" + gameKnowledge[i] + " game knowledge, -$" + cost[i];
					break;

				case Item.Type.Headset:
					results[i].text = "+" + teamPlay[i] + " team play, -$" + cost[i];
					break;

				case Item.Type.Keyboard:
					results[i].text = "+" + mechanics[i] + " mechanics, -$" + cost[i];
					break;

				case Item.Type.Mouse:
					results[i].text = "+" + mechanics[i] + " mechanics, -$" + cost[i];
					break;

				case Item.Type.Screen:
					results[i].text = "+" + teamPlay[i] + " team play,\n +" + gameKnowledge[i] + " game knowledge, -$" + cost[i];
					break;
			}
		}
	}

	public void BuyAccommodation(Accommodation accommodation)
	{
		if (!gameManager.IsMoneyHighEnough(accommodation.cost))
		{
			Debug.LogWarning("Not Enough Money");
			return;
		}

		gameManager.CurrentAccommodation = accommodation;
        artManager.UpdateAccommodation(accommodation);
        uiManager.UpdateAccommodations(accommodation);
		gameManager.DecreaseMoney(accommodation.cost);
	}

	public void BuyAccommodation()
	{
		BuyAccommodation(GetAccommodation(chosenAccommodationType));
	}

	public void SetItemType(string type)
	{
		chosenItemType = GetItemType(type);
	}

	public void SetItemQuality(string quality)
	{
		chosenItemQuality = GetItemQuality(quality);
	}

	public void SetAccommodation(string accommodation)
	{

	}

	public Accommodation.Type GetAccomodationType(string accommodation)
	{
		switch (accommodation)
		{
			case "Garage":
				return Accommodation.Type.Garage;

			case "Apartment":
				return Accommodation.Type.Apartment;

			case "House":
				return Accommodation.Type.House;

			case "Luxury Apartment":
				return Accommodation.Type.LuxuryApartment;

			default:
				Debug.LogWarning("Given accommodation is unavailable (try Garage, Apartment, House or Luxury Apartment)");
				return Accommodation.Type.None;
		}
	}

	public Accommodation GetAccommodation(Accommodation.Type type)
	{
		foreach (Accommodation a in playerData.GetAllAccommodations)
		{
			//if (type != f.itemType) continue;
		}
		return null;
	}

	public Item.Type GetItemType(string type)
	{
		switch (type)
		{
			case "Guide":
				return Item.Type.Guide;

			case "Headset":
				return Item.Type.Headset;

			case "Mouse":
				return Item.Type.Mouse;

			case "Keyboard":
				return Item.Type.Keyboard;

			case "Screen":
				return Item.Type.Screen;

			default:
				Debug.LogError("Given type is not available (try: Guide, Headset, Mouse, Keyboard or Screen)");
				return Item.Type.None;
		}
	}

	public Item.Quality GetItemQuality(string quality)
	{
		switch (quality)
		{
			case "Bad":
				return Item.Quality.Bad;

			case "Standard":
				return Item.Quality.Standard;

			case "Good":
				return Item.Quality.Good;

			case "Excellent":
				return Item.Quality.Excellent;

			default:
				Debug.LogError("Given quality '" + quality + "' is not available (try Bad, Standard, Good or Excellent)");
				return Item.Quality.Default;
		}
	}

	public Item GetItem(Item.Type type, Item.Quality quality)
	{
		foreach (ItemForm f in playerData.GetAllItems)
		{
			//if (type.ToString() != f.itemType) continue;

			foreach (Item i in f.qualities)
			{
				if (type != i.type || quality != i.quality) continue;

				return i;
			}
		}
		return null;
	}
	
	public void InitializeItemMenu()
	{
		buttons[0] = badItemQuality.transform.Find("Button").GetComponent<Button>();
		buttons[1] = standardItemQuality.transform.Find("Button").GetComponent<Button>();
		buttons[2] = goodItemQuality.transform.Find("Button").GetComponent<Button>();
		buttons[3] = excellentItemQuality.transform.Find("Button").GetComponent<Button>();

		results[0] = badItemQuality.transform.Find("Result").GetComponent<Text>();
		results[1] = standardItemQuality.transform.Find("Result").GetComponent<Text>();
		results[2] = goodItemQuality.transform.Find("Result").GetComponent<Text>();
		results[3] = excellentItemQuality.transform.Find("Result").GetComponent<Text>();

		ApplyCurrentItems(gameManager.CurrentItems, chosenItemType);
		ApplyItemResults(chosenItemType);
		ApplyItemTitle(itemTitle, chosenItemType.ToString());
	}
}
