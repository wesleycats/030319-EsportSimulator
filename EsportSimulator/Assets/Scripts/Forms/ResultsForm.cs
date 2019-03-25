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
    [SerializeField] private float gameKnowledge;
    [SerializeField] private float teamPlay;
    [SerializeField] private float mechanics;
    [SerializeField] private float trainingLevel1Multiplier;
    [SerializeField] private float trainingLevel2Multiplier;
    [SerializeField] private float trainingLevel3Multiplier;
    [SerializeField] private float workExperience;

    public float Duration { get { return duration; } }
    public int Tiredness { get { return tiredness; } }
    public int Hunger { get { return hunger; } }
    public int Thirst { get { return thirst; } }
    public int Money { get { return money; } }
    public int MoneyMin { get { return moneyMin; } }
    public int MoneyMax { get { return moneyMax; } }
    public int Rating { get { return rating; } }
    public int Fame { get { return fame; } }
    public float GameKnowledge { get { return gameKnowledge; } }
    public float TeamPlay { get { return teamPlay; } }
    public float Mechanics { get { return mechanics; } }
    public float GetTrainingLevel1Multiplier { get { return trainingLevel1Multiplier; } }
    public float GetTrainingLevel2Multiplier { get { return trainingLevel2Multiplier; } }
    public float GetTrainingLevel3Multiplier { get { return trainingLevel3Multiplier; } }
    public float WorkExperience { get { return workExperience; } }
}
