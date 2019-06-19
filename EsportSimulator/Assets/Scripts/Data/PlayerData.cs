using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for all data about the player
/// </summary>
[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player Data", order = 51)]
public class PlayerData : ScriptableObject
{
    [Header("Default Attributes")]
    #region Default Variables

    [SerializeField] private int defaultMoney;
	[SerializeField] private List<ItemForm> allItems;
	[SerializeField] private List<AccommodationForm> allAccommodations;

	#endregion

	[Header("Attributes")]
    #region Progress

    [SerializeField] private int money;
    [SerializeField] private int rating;
    [SerializeField] private int fame;
    [SerializeField] private float workExperience;
    [SerializeField] private int workLevel;

    #endregion

    #region Needs

    [SerializeField] private int tiredness;
    [SerializeField] private int hunger;
    [SerializeField] private int thirst;

    #endregion

    #region Skills

    [SerializeField] private int gameKnowledge;
    [SerializeField] private int teamPlay;
    [SerializeField] private int mechanics;

    #endregion

	[Header("Properties")]
    #region Properties 

    [SerializeField] private List<Item> currentItems = new List<Item>();
	[SerializeField] private AccommodationForm currentAccommodation;

	#endregion

	[Header("Events")]
	#region Events

	[SerializeField] List<Event> plannedTournaments;

	#endregion

	public void Reset()
    {
		Debug.Log("RESET");
        money = defaultMoney;
        rating = 0;
        fame = 0;
        workLevel = 0;
        workExperience = 0;
        tiredness = 0;
        hunger = 0;
        thirst = 0;
        gameKnowledge = 0;
        teamPlay = 0;
        mechanics = 0;
		//currentItems.Clear();
		
		foreach (ItemForm f in allItems)
		{
			foreach (Item i in f.qualities)
			{
				if (i.quality != Item.Quality.Default) continue;

				currentItems.Add(i);
			}
		}
		
        currentAccommodation = allAccommodations[0];
		plannedTournaments.Clear();
	}

    #region Getters & Setters

    public int GetDefaultMoney { get { return defaultMoney; } }
    public int GetMoney { get { return money; } }
    public int GetRating { get { return rating; } }
    public int GetFame { get { return fame; } }
    public float GetWorkExperience { get { return workExperience; } }
    public int GetWorkLevel { get { return workLevel; } }
    public int GetTiredness { get { return tiredness; } }
    public int GetHunger { get { return hunger; } }
    public int GetThirst { get { return thirst; } }
    public int GetGameKnowledge { get { return gameKnowledge; } }
    public int GetTeamPlay { get { return teamPlay; } }
    public int GetMechanics { get { return mechanics; } }
    public List<Item> CurrentItems { get { return currentItems; } }
    public List<ItemForm> GetAllItems { get { return allItems; } }
	public List<Event> GetPlannedTournaments { get { return plannedTournaments; } }
	public AccommodationForm GetCurrentAccommodation { get { return currentAccommodation; } }
	public List<AccommodationForm> GetAllAccommodations { get { return allAccommodations; } }

	public int SetDefaultMoney { set { defaultMoney = value; } }
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
    public List<Event> SetPlannedTournaments { set { plannedTournaments = value; } }
    public AccommodationForm SetCurrentAccommodation { set { currentAccommodation = value; } }
	public List<AccommodationForm> SetAllAccommodations { set { allAccommodations = value; } }

	#endregion
}

