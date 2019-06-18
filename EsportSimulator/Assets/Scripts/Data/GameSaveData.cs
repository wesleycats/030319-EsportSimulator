using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class GameSaveData
{
    #region Progress

    public int money;
    public int rating;
    public int fame;
    public float workExperience;
    public int workLevel;

    #endregion

    #region Needs

    public int tiredness;
    public int hunger;
    public int thirst;

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

	#region Events

	[SerializeField] public List<Event> plannedTournaments;

	#endregion

	#region Properties

	[SerializeField] public List<Item> currentItems;
	[SerializeField] public AccommodationForm currentAccommodation;

	#endregion

	#region Utilities

	public int saveSlotIndex;

    #endregion

    public Opponent[] opponents;
}
