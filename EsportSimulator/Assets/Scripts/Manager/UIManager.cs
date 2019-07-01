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
	public Text opponent1Stats;
	public Text opponent2Stats;
	public Text winChance;

	public GameObject darkOverlay;
	public LerpColor sleepOverlay;
	public Image needsMenuButton;

	public Sprite mutedSprite;
	public Sprite unMutedSprite;

	public GameObject monthsHolder;

	public GameObject contestAnnouncementMenu;
	public Text contestAnnouncementText;
	public GameObject battleResultsMenu;
	public Text battleResultsText;

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
	public LeaderboardManager lbManager;
	public PlayerData playerData;
	public ActivityManager activityManager;

	public void CheckNeedsWarning()
	{
		LerpColor needsButton = needsMenuButton.GetComponent<LerpColor>();

		int[] needs = new int[3];
		needs[0] = gameManager.Tiredness;
		needs[1] = gameManager.Hunger;
		needs[2] = gameManager.Thirst;

		foreach (int i in needs)
		{
			if (i >= 100)
			{
				needsButton.endColor = Color.red;
				needsButton.Lerp(true);
				return;
			}

			if (i >= gameManager.needsWarningLimit)
			{
				needsButton.endColor = Color.yellow;
				needsButton.Lerp(true);
				return;
			}

			if (i < gameManager.needsWarningLimit)
			{
				needsButton.Lerp(false);
				return;
			}
		}
	}

	public void UpdateAll()
	{ 
		UpdateTime(timeManager.GetHour, timeManager.GetMinute, timeManager.GetYear, timeManager.GetMonth);
		UpdateProgress(gameManager.GetMoney, gameManager.GetRating, gameManager.GetFame);
		UpdateNeeds(gameManager.GetTiredness, gameManager.GetHunger, gameManager.GetThirst);
		UpdateSkills(gameManager.GetGameKnowledge, gameManager.GetTeamPlay, gameManager.GetMechanics);
		UpdateItems(allItemTexts, gameManager.CurrentItems);
		UpdateLeaderboard(lbManager.Leaderboard);
		CheckNeedsWarning();
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
		moneyValue.color = GetTextColor(money, gameManager.CurrentAccommodation.rent, defaultTextColor);
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

	public void UpdateItems(List<Text> allItemTexts, List<Item> equipedItems)
	{
		string quality = "";
		string type = "";

		foreach (Text t in allItemTexts)
		{
			foreach (Item f in equipedItems)
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

	public void UpdateAccommodations(Accommodation currentAccommodation)
	{
		string status = "";
		foreach (Button b in allAccommdationButtons)
		{
			foreach (Accommodation a in playerData.GetAllAccommodations)
			{
				b.interactable = true;

				if (b.transform.parent.name == a.type.ToString())
				{
					status = "BUY";
				}
			}

			if (b.transform.parent.name == currentAccommodation.type.ToString())
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

		foreach (Opponent o in leaderboard)
		{
			leaderboardNames.text += o.name + "\n";
			leaderboardRatings.text += o.eloRating.ToString() + "\n";
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

	public void UpdateBattleStats(Opponent o1, Opponent o2, Battle.Mode mode, int winChancePercentage)
	{
		battleMenu.SetActive(true);

		competitorNames.text = o1.name.ToUpper() + " VS " + o2.name.ToUpper();

		switch (mode)
		{
			case Battle.Mode.OneVersusOne:
				opponent1Stats.text = "game knowledge: " + o1.gameKnowledge + "\n\n mechanics: " + o1.mechanics;
				opponent2Stats.text = "game knowledge: " + o2.gameKnowledge + "\n\n mechanics: " + o2.mechanics;
				break;

			case Battle.Mode.ThreeVersusThree:
				opponent1Stats.text = "game knowledge: " + o1.gameKnowledge + "\n\n team play: " + o1.teamPlay;
				opponent2Stats.text = "game knowledge: " + o2.gameKnowledge + "\n\n team play: " + o2.teamPlay;
				break;

			case Battle.Mode.FiveVersusFive:
				opponent1Stats.text = "game knowledge: " + o1.gameKnowledge + "\n\n team play: " + o1.teamPlay + "\n\n mechanics: " + o1.mechanics;
				opponent2Stats.text = "game knowledge: " + o2.gameKnowledge + "\n\n team play: " + o2.teamPlay + "\n\n mechanics: " + o2.mechanics;
				break;
		}

		if (activityManager.currentActivity == ActivityManager.Activity.Contest)
		{
			opponent1Stats.text += "\n\ntournament placement:\n" + contestManager.Participants[contestManager.Participants.IndexOf(o1)].placement;
			opponent2Stats.text += "\n\ntournament placement:\n" + contestManager.Participants[contestManager.Participants.IndexOf(o2)].placement;
		}

		if (winChancePercentage < 0) winChancePercentage = 0;
		if (winChancePercentage > 100) winChancePercentage = 100;

		winChance.text = "win chance: " + winChancePercentage + "%";
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

	public void SleepOverlay(bool activate)
	{
		sleepOverlay.Lerp(2);
	}

	public void ActivateContestAnnouncement(Battle.Mode mode)
	{
		darkOverlay.SetActive(true);
		darkOverlay.GetComponent<Button>().interactable = false;
		contestAnnouncementMenu.SetActive(true);
		contestAnnouncementText.text = "THE " + mode.ToString() + " TOURNAMENT IS STARTING!";
		contestAnnouncementMenu.GetComponent<Button>().interactable = true;
	}

	public void ActivateBattleResult(bool win)
	{
		battleResultsMenu.SetActive(true);

		if (win)
			battleResultsText.text = "You won the battle!";
		else
			battleResultsText.text = "You lost the battle!";
	}

	public void Initialize()
    {
        UpdateAll();
    }

    #region Getters & Settes

    #endregion
}
