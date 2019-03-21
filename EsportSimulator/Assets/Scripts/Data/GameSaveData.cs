using System;
using System.Collections;
using UnityEngine;

[Serializable]

public class GameSaveData
{
    #region Progress

    public float money;
    public float rating;
    public float fame;
    public float workExperience;
    public int workLevel;
    public int houseLevel;

    #endregion

    #region Needs

    public float tiredness;
    public float hunger;
    public float thirst;

    #endregion

    #region Skills

    public int gameKnowledge;
    public int teamPlay;
    public int mechanics;

    #endregion

    #region In-game time

    public int hour;
    public int minute;
    public int year;
    public int month;

    #endregion

    #region Real date

    public int realDay;
    public int realMonth;
    public int realYear;

    #endregion

    #region Utilities

    public int saveSlotIndex;

    #endregion

    public Opponent[] opponents;
}
