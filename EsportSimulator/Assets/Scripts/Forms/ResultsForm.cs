using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultsForm
{
    [Tooltip("Hours")]
    [SerializeField] private float duration;
    [Tooltip("Percentage")]
    [SerializeField] private int tiredness;
    [Tooltip("Percentage")]
    [SerializeField] private int hunger;
    [Tooltip("Percentage")]
    [SerializeField] private int thirst;
    [SerializeField] private int money;
    [SerializeField] private int moneyMin;
    [SerializeField] private int moneyMax;
    [SerializeField] private int rating;
    [SerializeField] private int fame;
    [SerializeField] private int gameKnowledge;
    [SerializeField] private int teamPlay;
    [SerializeField] private int mechanics;
    [SerializeField] private float trainingLevel1Multiplier;
    [SerializeField] private float trainingLevel2Multiplier;
    [SerializeField] private float trainingLevel3Multiplier;
    [SerializeField] private int workExperience;

    public float Duration { get { return duration; } }
    public int Tiredness { get { return tiredness; } set { tiredness = value; } }
    public int Hunger { get { return hunger; } set { hunger = value; } }
    public int Thirst { get { return thirst; } set { thirst = value; } }
    public int Money { get { return money; } }
    public int MoneyMin { get { return moneyMin; } }
    public int MoneyMax { get { return moneyMax; } }
    public int Rating { get { return rating; } set { rating = value; } }
    public int Fame { get { return fame; } set { fame = value; } }
    public int GameKnowledge { get { return gameKnowledge; } set { gameKnowledge = value; } }
    public int TeamPlay { get { return teamPlay; } set { teamPlay = value; } }
    public int Mechanics { get { return mechanics; } set { mechanics = value; } }
    public int WorkExperience { get { return workExperience; } set { workExperience = value; } }

	public float SetDuration { set { duration = value; } }
	public int SetMoney { set { money = value; } }
}
