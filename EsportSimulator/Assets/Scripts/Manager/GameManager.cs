using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Controls game stages & keeps track of variables in current session
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Player Variables

    [SerializeField] private float money;
    [SerializeField] private float rating;
    [SerializeField] private float fame;
    [SerializeField] private float workExperience;
    [SerializeField] private int workLevel;
    [SerializeField] private int houseLevel;
    [SerializeField] private float tiredness;
    [SerializeField] private float hunger;
    [SerializeField] private float thirst;
    [SerializeField] private int gameKnowledge;
    [SerializeField] private int teamPlay;
    [SerializeField] private int mechanics;

    #endregion

    public PlayerData playerData;
    public GameData gameData;
    public UIManager uiManager;
    public ArtManager artManager;
    public TimeManager timeManager;
    public ButtonManager buttonManager;
    public ActivityManager activityManager;

    void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        InitializePlayerData();
        timeManager.InitializeGameData();
        uiManager.InitializeData();
    }

    public void ResetGame()
    {
        playerData.Reset(true);
        gameData.Reset(true);
        LoadData();
    }

    public void RestartGame(bool restart)
    {
        if (!restart) return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseMoney(float amount)
    {
        money += amount;

        uiManager.UpdateProgress(money, rating, fame);
    }

    public void DecreaseMoney(float amount)
    {
        money -= amount;

        uiManager.UpdateProgress(money, rating, fame);
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
    void InitializePlayerData()
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
    }

    #region Getters & Setters

    public float GetMoney { get { return money; } }
    public float GetRating { get { return rating; } }
    public float GetFame { get { return fame; } }
    public float GetWorkExperience { get { return workExperience; } }
    public int GetWorkLevel { get { return workLevel; } }
    public int GetHouseLevel { get { return houseLevel; } }
    public float GetTiredness { get { return tiredness; } }
    public float GetHunger { get { return hunger; } }
    public float GetThirst { get { return thirst; } }
    public int GetGameKnowledge { get { return gameKnowledge; } }
    public int GetTeamPlay { get { return teamPlay; } }
    public int GetMechanics { get { return mechanics; } }

    public float SetMoney { set { money = value; } }
    public float SetRating { set { rating = value; } }
    public float SetFame { set { fame = value; } }
    public float SetWorkExperience { set { workExperience = value; } }
    public int SetWorkLevel { set { workLevel = value; } }
    public int SetHouseLevel { set { houseLevel = value; } }
    public float SetTiredness { set { tiredness = value; } }
    public float SetHunger { set { hunger = value; } }
    public float SetThirst { set { thirst = value; } }
    public int SetGameKnowledge { set { gameKnowledge = value; } }
    public int SetTeamPlay { set { teamPlay = value; } }
    public int SetMechanics { set { mechanics = value; } }

    public float Money { get { return money; } set { money = value; } }
    public float Rating { get { return rating; } set { rating = value; } }
    public float Fame { get { return fame; } set { fame = value; } }
    public float WorkExperience { get { return workExperience; } set { workExperience = value; } }
    public int WorkLevel { get { return workLevel; } set { workLevel = value; } }
    public int HouseLevel { get { return houseLevel; } set { houseLevel = value; } }
    public float Tiredness { get { return tiredness; } set { tiredness = value; } }
    public float Hunger { get { return hunger; } set { hunger = value; } }
    public float Thirst { get { return thirst; } set { thirst = value; } }
    public int GameKnowledge { get { return gameKnowledge; } set { gameKnowledge = value; } }
    public int TeamPlay { get { return teamPlay; } set { teamPlay = value; } }
    public int Mechanics { get { return mechanics; } set { mechanics = value; } }


    #endregion
}
