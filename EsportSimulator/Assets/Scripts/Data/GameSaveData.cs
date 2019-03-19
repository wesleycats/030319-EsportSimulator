using System;

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

    #region Time

    public int hour;
    public int minute;
    public int year;
    public int month;

    #endregion

    #region Utilities
    
    public int saved;

    #endregion
}
