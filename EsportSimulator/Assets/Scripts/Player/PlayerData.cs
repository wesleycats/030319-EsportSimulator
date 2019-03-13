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
    [SerializeField] private float workLevel;
    [SerializeField] private float houseLevel;

    #endregion

    #region Needs

    [SerializeField] private float tiredness;
    [SerializeField] private float hunger;
    [SerializeField] private float thirst;

    #endregion

    #region Skills
    [SerializeField] private float gameKnowledge;
    [SerializeField] private float teamPlay;
    [SerializeField] private float mechanics;
    #endregion

    #region Utilities

    [SerializeField] private bool saved;
    [SerializeField] private bool debug;

    #endregion

    public void Reset(bool reset)
    {
        money = defaultMoney;
        rating = 0;
        fame = 0;
        workLevel = 1;
        workExperience = 0;
        houseLevel = 1;
        tiredness = 0;
        hunger = 0;
        thirst = 0;
        gameKnowledge = 0;
        teamPlay = 0;
        mechanics = 0;
        saved = false;
    }

    public void SetMoney(float amount)
    {
        if (!saved || !debug) return;

        money = amount;
    }

    public void SetRating(float amount)
    {
        if (!saved || !debug) return;

        rating = amount;
    }

    public void SetFame(float amount)
    {
        if (!saved || !debug) return;

        money = amount;
    }

    public void SetWorkExperience(float amount)
    {
        if (!saved || !debug) return;

        workExperience = amount;
    }

    public void SetWorkLevel(float amount)
    {
        if (!saved || !debug) return;

        workLevel = amount;
    }

    public void SetTiredness(float amount)
    {
        if (!saved || !debug) return;

        tiredness = amount;
    }

    public void SetHunger(float amount)
    {
        if (!saved || !debug) return;

        hunger = amount;
    }

    public void SetThirst(float amount)
    {
        if (!saved || !debug) return;

        thirst = amount;
    }

    public void SetGameKnowledge(float amount)
    {
        if (!saved || !debug) return;

        gameKnowledge = amount;
    }

    public void SetTeamPlay(float amount)
    {
        if (!saved || !debug) return;

        teamPlay = amount;
    }

    public void SetMechanics(float amount)
    {
        if (!saved || !debug) return;

        mechanics = amount;
    }

    #region Getters & Setters

    public float DefaultMoney { get { return defaultMoney; } }
    public float Money { get { return money; } }
    public float Rating { get { return rating; } }
    public float Fame { get { return fame; } }
    public float WorkExperience { get { return workExperience; } }
    public float WorkLevel { get { return workLevel; } }
    public float HouseLevel { get { return houseLevel; } }
    public float Tiredness { get { return tiredness; } }
    public float Hunger { get { return hunger; } }
    public float Thirst { get { return thirst; } }
    public float GameKnowledge { get { return gameKnowledge; } }
    public float TeamPlay { get { return teamPlay; } }
    public float Mechanics { get { return mechanics; } }
    public bool Saved { get { return saved; } set { saved = value; } }

    #endregion
}

