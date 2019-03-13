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

    void Start()
    {
        if (!playerData.Saved) ResetGame();

        InitializePlayerData();
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

        uiManager.InitializePlayerData();
    }

    #region Getters & Setters

    public float Money { get { return money; } }
    public float Rating { get { return rating; } }
    public float Fame { get { return fame; } }
    public float Tiredness { get { return tiredness; } }
    public float Hunger { get { return hunger; } }
    public float Thirst { get { return thirst; } }
    public float GameKnowledge { get { return gameKnowledge; } }
    public float TeamPlay { get { return teamPlay; } }
    public float Mechanics { get { return mechanics; } }

    #endregion
}
