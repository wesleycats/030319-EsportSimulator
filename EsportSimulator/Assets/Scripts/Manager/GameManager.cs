using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls game stages & keeps track of variables in current session
/// </summary>
public class GameManager : MonoBehaviour
{
	#region Player Variables

	[SerializeField] private int money;
    [SerializeField] private int rating;
    [SerializeField] private int fame;
    [SerializeField] private float workExperience;
    [SerializeField] private int workLevel;
    [SerializeField] private int houseLevel;
    [SerializeField] private int tiredness;
    [SerializeField] private int hunger;
    [SerializeField] private int thirst;
    [SerializeField] private int gameKnowledge;
    [SerializeField] private int teamPlay;
    [SerializeField] private int mechanics;

	[SerializeField] private List<Item> currentItems = new List<Item>();
    [SerializeField] private Accommodation currentAccommodation;
	[SerializeField] private List<Event> plannedEvents;

	#endregion

	#region References

	[Header("References")]
    public PlayerData playerData;
    public GameData gameData;
    public UIManager uiManager;
    public ArtManager artManager;
    public TimeManager timeManager;
    public ButtonManager buttonManager;
    public ActivityManager activityManager;
    public OpponentManager opponentManager;
    public LeaderboardManager leaderboardManager;
    public ResultManager resultManager;
    public ShopManager shopManager;
	public LerpColor switchOverlay;

	public Debugger debug;

	#endregion
	
	void Start()
    {
		ResetGame();
		Time.timeScale = 1f;
		switchOverlay.LerpValue = 1;
		switchOverlay.Lerp(1);
	}

	public Event GetPlannedEventOn(int currentMonth, List<Event> events)
	{
		foreach (Event b in events)
			if (b.month == currentMonth) return b;

		return null;
	}

    public void InitializeData()
    {
        InitializePlayerData();
        timeManager.InitializeGameData();
        opponentManager.InitializeOpponents();
        leaderboardManager.Initialize();
        uiManager.Initialize();
        artManager.Initialize();
    }

	public void InitializeSaveData()
	{
		InitializePlayerData();
		timeManager.InitializeGameData();
		uiManager.Initialize();
		artManager.Initialize();
	}

	public void ResetGame()
    {
        playerData.Reset();
        gameData.Reset();
		InitializeData();
    }

    public void GameOver()
    {
        if (debug.noLose) return;

        uiManager.gameOverMenu.SetActive(true);
    }

    public void WinGame()
    {
        uiManager.winGameMenu.SetActive(true);
    }

	public void RemoveItemOfType(Item.Type type)
	{
		foreach (Item i in currentItems)
		{
			if (type != i.type) continue;

			currentItems.Remove(i);
		}
	}
    
    public void IncreaseMoney(int amount)
    {
        money += amount;

        uiManager.UpdateProgress(money, rating, fame);
    }

    public void DecreaseMoney(int amount)
    {
        money -= amount;

        uiManager.UpdateProgress(money, rating, fame);
    }

    public void IncreaseFame(int amount)
    {
        fame += amount;

        uiManager.UpdateProgress(money, rating, fame);
    }

    public void DecreaseFame(int amount)
    {
        fame -= amount;

        uiManager.UpdateProgress(money, rating, fame);
    }

    public void IncreaseRating(int amount)
    {
        rating += amount;

        uiManager.UpdateProgress(money, rating, fame);
    }

    public void DecreaseRating(int amount)
    {
        rating -= amount;

        uiManager.UpdateProgress(money, rating, fame);
    }

    public void IncreaseGameKnowledge(int amount)
    {
        gameKnowledge += amount;

        uiManager.UpdateSkills(gameKnowledge, teamPlay, mechanics);
    }

    public void IncreaseTeamPlay(int amount)
    {
        teamPlay += amount;

        uiManager.UpdateSkills(gameKnowledge, teamPlay, mechanics);
    }

    public void IncreaseMechanics(int amount)
    {
        mechanics += amount;

        uiManager.UpdateSkills(gameKnowledge, teamPlay, mechanics);
    }

	public void IncreaseHunger(int amount)
	{
		hunger += amount;
	}

	public void IncreaseThirst(int amount)
	{
		thirst += amount;
	}

	public void IncreaseWorkExp(int amount)
	{
		workExperience += amount;
	}

	public void DecreaseHunger(int amount)
	{
		hunger -= amount;
	}

	public void DecreaseThirst(int amount)
	{
		thirst -= amount;
	}

	public void DecreaseWorkExp(int amount)
	{
		workExperience -= amount;
	}

	public void IncreaseTiredness(int amount)
    {
        tiredness += amount;

        if (tiredness >= 70)
        {
			uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.yellow;
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
        }

        if (tiredness >= 100)
        {
            uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.red;
            uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
		}
	}

	public void DecreaseTiredness(int amount)
	{
		tiredness -= amount;

		if (tiredness < 100)
		{
			uiManager.needsMenuButton.GetComponent<LerpColor>().endColor = Color.yellow;
			uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = true;
		}

		if (tiredness < 70)
		{
			uiManager.needsMenuButton.GetComponent<LerpColor>().LerpActivated = false;
		}
	}

	public bool IsMoneyLowEnough(float amount)
    {
        return money <= amount;
    }
    
    public bool IsMoneyHighEnough(float amount)
    {
        return money >= amount;
    }

    public bool IsTirednessLowEnough(float amount)
    {
        return tiredness <= amount;
    }

    public bool IsTirednessHighEnough(float amount)
    {
        return tiredness >= amount;
    }

    public bool IsHungerLowEnough(float amount)
    {
        return hunger <= amount;
    }

    public bool IsHungerHighEnough(float amount)
    {
        return hunger >= amount;
    }

    public bool IsThirstLowEnough(float amount)
    {
        return thirst <= amount;
    }

    public bool IsThirstHighEnough(float amount)
    {
        return thirst >= amount;
    }
    
    /// <summary>
    /// Initializes player data in variables
    /// </summary>
    public void InitializePlayerData()
    {
        money = playerData.GetMoney;
        rating = playerData.GetRating;
        fame = playerData.GetFame;
        workExperience = playerData.GetWorkExperience;
        workLevel = playerData.GetWorkLevel;
        tiredness = playerData.GetTiredness;
        hunger = playerData.GetHunger;
        thirst = playerData.GetThirst;
        gameKnowledge = playerData.GetGameKnowledge;
        teamPlay = playerData.GetTeamPlay;
        mechanics = playerData.GetMechanics;
		plannedEvents.Clear();
		plannedEvents = playerData.GetPlannedTournaments;

		currentItems.Clear();
		foreach (Item i in playerData.CurrentItems)
		{
			currentItems.Add(i);
		}

		currentAccommodation = playerData.CurrentAccommodation;
	}

    #region Getters & Setters

    public int GetMoney { get { return money; } }
    public int GetRating { get { return rating; } }
    public int GetFame { get { return fame; } }
    public float GetWorkExperience { get { return workExperience; } }
    public int GetWorkLevel { get { return workLevel; } }
    public int GetHouseLevel { get { return houseLevel; } }
    public int GetTiredness { get { return tiredness; } }
    public int GetHunger { get { return hunger; } }
    public int GetThirst { get { return thirst; } }
    public int GetGameKnowledge { get { return gameKnowledge; } }
    public int GetTeamPlay { get { return teamPlay; } }
    public int GetMechanics { get { return mechanics; } }
    public List<Event> GetPlannedEvents { get { return plannedEvents; } }

    public int SetMoney { set { money = value; } }
    public int SetRating { set { rating = value; } }
    public int SetFame { set { fame = value; } }
    public float SetWorkExperience { set { workExperience = value; } }
    public int SetWorkLevel { set { workLevel = value; } }
    public int SetTiredness { set { tiredness = value; } }
    public int SetHunger { set { hunger = value; } }
    public int SetThirst { set { thirst = value; } }
    public int SetGameKnowledge { set { gameKnowledge = value; } }
    public int SetTeamPlay { set { teamPlay = value; } }
    public int SetMechanics { set { mechanics = value; } }

    public int Money { get { return money; } set { money = value; } }
    public int Rating { get { return rating; } set { rating = value; } }
    public int Fame { get { return fame; } set { fame = value; } }
    public float WorkExperience { get { return workExperience; } set { workExperience = value; } }
    public int WorkLevel { get { return workLevel; } set { workLevel = value; } }
    public int Tiredness { get { return tiredness; } set { tiredness = value; } }
    public int Hunger { get { return hunger; } set { hunger = value; } }
    public int Thirst { get { return thirst; } set { thirst = value; } }
    public int GameKnowledge { get { return gameKnowledge; } set { gameKnowledge = value; } }
    public int TeamPlay { get { return teamPlay; } set { teamPlay = value; } }
    public int Mechanics { get { return mechanics; } set { mechanics = value; } }
	public List<Item> CurrentItems { set { currentItems = value; } get { return currentItems; } }
	public Accommodation CurrentAccommodation { set { currentAccommodation = value; } get { return currentAccommodation; } }

	#endregion
}
