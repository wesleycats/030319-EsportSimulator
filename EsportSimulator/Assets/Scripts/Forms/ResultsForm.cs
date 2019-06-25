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
    public int Tiredness { get { return tiredness; } }
    public int Hunger { get { return hunger; } }
    public int Thirst { get { return thirst; } }
    public int Money { get { return money; } }
    public int MoneyMin { get { return moneyMin; } }
    public int MoneyMax { get { return moneyMax; } }
    public int Rating { get { return rating; } }
    public int Fame { get { return fame; } }
    public int GameKnowledge { get { return gameKnowledge; } }
    public int TeamPlay { get { return teamPlay; } }
    public int Mechanics { get { return mechanics; } }
    public float GetTrainingLevel1Multiplier { get { return trainingLevel1Multiplier; } }
    public float GetTrainingLevel2Multiplier { get { return trainingLevel2Multiplier; } }
    public float GetTrainingLevel3Multiplier { get { return trainingLevel3Multiplier; } }
    public int WorkExperience { get { return workExperience; } set { workExperience = value; } }

	public float SetDuration { set { duration = value; } }
	public int SetTiredness { set { tiredness = value; } }
	public int SetHunger { set { hunger = value; } }
	public int SetThirst { set { thirst = value; } }
	public int SetMoney { set { money = value; } }
	public int SetGameKnowledge { set { gameKnowledge = value; } }
	public int SetTeamPlay { set { teamPlay = value; } }
	public int SetMechanics { set { mechanics = value; } }
}
