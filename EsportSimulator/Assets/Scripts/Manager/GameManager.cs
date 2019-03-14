using UnityEngine;
using System;

/// <summary>
/// Controls game stages
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Player Variables

    [SerializeField] private float money;
    [SerializeField] private float rating;
    [SerializeField] private float fame;
    [SerializeField] private float workExperience;
    [SerializeField] private float workLevel;
    [SerializeField] private float tiredness;
    [SerializeField] private float hunger;
    [SerializeField] private float thirst;
    [SerializeField] private float gameKnowledge;
    [SerializeField] private float teamPlay;
    [SerializeField] private float mechanics;

    #endregion

    public PlayerData playerData;
    public UIManager uiManager;
    public ArtManager artManager;
    public TimeManager timeManager;

    void Start()
    {
        if (!playerData.Saved) ResetGame();

        InitializePlayerData();
        timeManager.InitializeGameData();
        uiManager.InitializePlayerData();
        uiManager.InitializeGameData();
    }

    public void ResetGame()
    {
        playerData.Reset(true);
    }

    /// <summary>
    /// Initializes player data in variables
    /// </summary>
    void InitializePlayerData()
    {
        money = playerData.Money;
        rating = playerData.Rating;
        fame = playerData.Fame;
        workExperience = playerData.WorkExperience;
        workLevel = playerData.WorkLevel;
        tiredness = playerData.Tiredness;
        hunger = playerData.Hunger;
        thirst = playerData.Thirst;
        gameKnowledge = playerData.GameKnowledge;
        teamPlay = playerData.TeamPlay;
        mechanics = playerData.Mechanics;
    }

    #region Getters & Setters

    public float Money { get { return money; } set { money = value; } }
    public float Rating { get { return rating; } set { rating = value; } }
    public float Fame { get { return fame; } set { fame = value; } }
    public float WorkExperience { get { return workExperience; } set { workExperience = value; } }
    public float WorkLevel { get { return workLevel; } set { workLevel = value; } }
    public float Tiredness { get { return tiredness; } set { tiredness = value; } }
    public float Hunger { get { return hunger; } set { hunger = value; } }
    public float Thirst { get { return thirst; } set { thirst = value; } }
    public float GameKnowledge { get { return gameKnowledge; } set { gameKnowledge = value; } }
    public float TeamPlay { get { return teamPlay; } set { teamPlay = value; } }
    public float Mechanics { get { return mechanics; } set { mechanics = value; } }

    #endregion
}
