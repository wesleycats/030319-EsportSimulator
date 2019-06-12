using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	#region UI Elements

	public Color defaultTextColor;
	public Text time;
	public Text date;
	public Text moneyValue;
	public Text ratingValue;
	public Text fameValue;
	public Text tirednessValue;
	public Slider tirednessBar;
	public Text hungerValue;
	public Slider hungerBar;
	public Text thirstValue;
	public Slider thirstBar;
	public Text gameKnowledgeValue;
	public Text teamPlayValue;
	public Text mechanicsValue;
	public Text activityText;
	public GameObject gameOverMenu;
	public GameObject winGameMenu;
	public Text leaderboardNames;
	public Text leaderboardRatings;

	public GameObject battleMenu;
	public GameObject contestResultMenu;
	public Text contestPlacement;
	public Text contestResults;
	public Text competitorNames;
	public Text playerStats;
	public Text opponentStats;
	public Text winChance;

	public GameObject darkOverlay;
	public Image sleepOverlay;
	public Image needsMenuButton;

	public GameObject monthsHolder;

	public GameObject contestAnnouncementMenu;
	public Text contestAnnouncementText;

	public List<Text> allItemTexts = new List<Text>();
	public List<Button> allAccommdationButtons = new List<Button>();

	#endregion

	[Tooltip("[0]=champion, [1]=diamond, etc.")]
	public Color[] divisionColors;
	public TimeManager timeManager;
	public GameManager gameManager;
	public ButtonManager buttonManager;
	public ContestManager contestManager;
	public OpponentManager opponentManager;
	public LeaderboardManager leaderboardManager;

	public void UpdateAll()
	{
		UpdateTime(timeManager.GetHour, timeManager.GetMinute, timeManager.GetYear, timeManager.GetMonth);
		UpdateProgress(gameManager.GetMoney, gameManager.GetRating, gameManager.GetFame);
		UpdateNeeds(gameManager.GetTiredness, gameManager.GetHunger, gameManager.GetThirst);
		UpdateSkills(gameManager.GetGameKnowledge, gameManager.GetTeamPlay, gameManager.GetMechanics);
		UpdateItems(allItemTexts, gameManager.GetEquipedItems);
		UpdateLeaderboard(leaderboardManager.GetLeaderboard);
	}

	public void UpdateTime(int hour, int minute, int year, int month)
	{
		if (hour < 10)
			time.text = "0" + hour.ToString() + ":" + minute.ToString() + "0";
		else
			time.text = hour.ToString() + ":" + minute.ToString() + "0";

		date.text = "Month " + month.ToString() + " of year " + year.ToString();
	}

	public void UpdateProgress(int money, int rating, int fame)
	{
		moneyValue.color = GetTextColor(money, gameManager.GetCurrentAccommodation.rent, defaultTextColor);
		ratingValue.color = GetTextColor(rating, 0, defaultTextColor);
		fameValue.color = GetTextColor(fame, 0, defaultTextColor);

		moneyValue.text = money.ToString();
		ratingValue.text = rating.ToString();
		fameValue.text = fame.ToString();
	}

	public void UpdateNeeds(float tiredness, float hunger, float thirst)
	{
		tirednessValue.text = tiredness.ToString() + "%";
		hungerValue.text = hunger.ToString() + "%";
		thirstValue.text = thirst.ToString() + "%";

		tirednessBar.value = tiredness / 100;
		hungerBar.value = hunger / 100;
		thirstBar.value = thirst / 100;
	}

	public void UpdateSkills(int gameKnowledge, int teamPlay, int mechanics)
	{
		gameKnowledgeValue.text = gameKnowledge.ToString();
		teamPlayValue.text = teamPlay.ToString();
		mechanicsValue.text = mechanics.ToString();
	}

	public void UpdateItems(List<Text> allItemTexts, List<ItemForm> equipedItems)
	{
		string quality = "";
		string type = "";

		foreach (Text t in allItemTexts)
		{
			foreach (ItemForm f in equipedItems)
			{
				type = f.type.ToString();
				if (t.transform.parent.name == type)
				{
					quality = f.quality.ToString();
					t.text = type + ": " + quality;
				}
			}
		}
	}

	public void UpdateAccommodations(List<Button> allAccommdationButton, AccommodationForm currentAccommodation, List<AccommodationForm> allAccommodations)
	{
		string status = "";
		foreach (Button b in allAccommdationButton)
		{
			foreach (AccommodationForm a in allAccommodations)
			{
				b.interactable = true;

				if (b.transform.parent.name == a.accommodation.type.ToString())
				{
					if (a.accommodation.bought)
					{
						status = "OWNED";
					}
					else
					{
						status = "BUY";
					}
				}
			}

			if (b.transform.parent.name == currentAccommodation.accommodation.type.ToString())
			{
				b.interactable = false;
				status = "CURRENT";
			}

			b.transform.GetChild(0).GetComponent<Text>().text = status;
		}
	}

	public void UpdateLeaderboard(List<Opponent> leaderboard)
	{
		leaderboardNames.text = "";
		leaderboardRatings.text = "";

		for (int i = 0; i < leaderboard.Count; i++)
		{
			leaderboardNames.text += i + 1 + ". " + leaderboard[i].name + "\n";
			leaderboardRatings.text += leaderboard[i].eloRating.ToString() + "\n";
		}

	}

	public void UpdateCalender()
	{
		List<Image> eventImages = new List<Image>();
		List<Event> plannendEvents = gameManager.GetPlannedEvents;

		for (int i = 0; i < monthsHolder.transform.childCount; i++)
		{
			eventImages.Add(monthsHolder.transform.GetChild(i).transform.Find("Event").GetComponent<Image>());
			eventImages[i].gameObject.SetActive(false);

			for (int j = 0; j < gameManager.GetPlannedEvents.Count; j++)
			{
				if (gameManager.GetPlannedEvents[j].month - 1 == i)
				{
					eventImages[i].gameObject.SetActive(true);
				}
			}
		}
	}

	public void UpdateCompetitorStats(Opponent player, Opponent opponent, Battle.Mode mode, int winChancePercentage)
	{
		battleMenu.SetActive(true);

		competitorNames.text = player.name.ToUpper() + " VS " + opponent.name.ToUpper();

		switch (mode)
		{
			case Battle.Mode.OneVersusOne:
				playerStats.text = "game knowledge: " + player.gameKnowledge + "\n\n mechanics: " + player.mechanics + "\n\ntournament placement:\n" + contestManager.GetParticipants[contestManager.GetParticipants.IndexOf(player)].placement;
				opponentStats.text = "game knowledge: " + opponent.gameKnowledge + "\n\n mechanics: " + opponent.mechanics + "\n\ntournament placement:\n" + contestManager.GetParticipants[contestManager.GetParticipants.IndexOf(opponent)].placement;
				winChance.text = "win chance: " + winChancePercentage + "%";
				break;

			case Battle.Mode.ThreeVersusThree:
				playerStats.text = "game knowledge: " + player.gameKnowledge + "\n\n team play: " + player.teamPlay + "\n\ntournament placement:\n" + contestManager.GetParticipants[contestManager.GetParticipants.IndexOf(player)].placement;
				opponentStats.text = "game knowledge: " + opponent.gameKnowledge + "\n\n team play: " + opponent.teamPlay + "\n\ntournament placement:\n" + contestManager.GetParticipants[contestManager.GetParticipants.IndexOf(opponent)].placement;
				winChance.text = "win chance: " + winChancePercentage + "%";
				break;

			case Battle.Mode.FiveVersusFive:
				playerStats.text = "game knowledge: " + player.gameKnowledge + "\n\n team play: " + player.teamPlay + "\n\n mechanics: " + player.mechanics + "\n\ntournament placement:\n" + contestManager.GetParticipants[contestManager.GetParticipants.IndexOf(player)].placement;
				opponentStats.text = "game knowledge: " + opponent.gameKnowledge + "\n\n team play: " + opponent.teamPlay + "\n\n mechanics: " + opponent.mechanics + "\n\ntournament placement:\n" + contestManager.GetParticipants[contestManager.GetParticipants.IndexOf(opponent)].placement;
				winChance.text = "win chance: " + winChancePercentage + "%";
				break;
		}
	}

	public void UpdateContestResults(int placement, DivisionForm division)
	{
		string numberExtension = "th";

		if (placement == 21 || placement == 1)
			numberExtension = "st";
		else if (placement == 2)
			numberExtension = "nd";
		else if (placement == 3)
			numberExtension = "rd";

		contestPlacement.text = "You finished " + placement + numberExtension;
		contestResults.text = "You received:\n\n$" + division.moneyReward + "\n" + division.fameReward + " fame";
	}

	public Color GetTextColor(int currentValue, int checkValue, Color defaultColor)
	{
		if (currentValue < checkValue)
			return Color.red;
		else if (currentValue > checkValue)
		{
			Color color = Color.green * 0.7f;
			color.a = 1;
			return color;
		}
		else
			return defaultColor;
	}

	public void ActivateSleepOverlay()
	{
		sleepOverlay.GetComponent<LerpColor>().Increasing = true;
		sleepOverlay.GetComponent<LerpColor>().LerpMaxAmount = 2;
		sleepOverlay.GetComponent<LerpColor>().LerpActivated = true;
	}

	public void DeactivateSleepOverlay()
	{
		sleepOverlay.GetComponent<LerpColor>().Increasing = false;
		sleepOverlay.GetComponent<LerpColor>().LerpActivated = true;
	}

	public void ActivateContestAnnouncement(string battleMode)
	{
		contestAnnouncementMenu.SetActive(true);
		contestAnnouncementText.text = "THE " + battleMode + " TOURNAMENT IS STARTING!";
		contestAnnouncementMenu.GetComponent<Button>().interactable = true;
	}

public void Initialize()
    {
        UpdateAll();
    }

    #region Getters & Settes

    #endregion
}
