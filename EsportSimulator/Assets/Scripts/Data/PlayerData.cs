using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for all data about the player
/// </summary>
[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player Data", order = 51)]
public class PlayerData : ScriptableObject
{
    #region Default Variables

    [SerializeField] private float defaultMoney;

    #endregion

    #region Progress

    [SerializeField] private float money;
    [SerializeField] private float rating;
    [SerializeField] private float fame;
    [SerializeField] private float workExperience;
    [SerializeField] private int workLevel;
    [SerializeField] private int houseLevel;

    #endregion

    #region Needs

    [SerializeField] private float tiredness;
    [SerializeField] private float hunger;
    [SerializeField] private float thirst;

    #endregion

    #region Skills
    [SerializeField] private int gameKnowledge;
    [SerializeField] private int teamPlay;
    [SerializeField] private int mechanics;
    #endregion

    #region Utilities

    #endregion

    public void Reset(bool reset)
    {
        money = defaultMoney;
        rating = 0;
        fame = 0;
        workLevel = 0;
        workExperience = 0;
        houseLevel = 0;
        tiredness = 0;
        hunger = 0;
        thirst = 0;
        gameKnowledge = 0;
        teamPlay = 0;
        mechanics = 0;
    }

    #region Getters & Setters

    public float GetDefaultMoney { get { return defaultMoney; } }
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

    public float SetDefaultMoney { set { defaultMoney = value; } }
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

    #endregion
}

