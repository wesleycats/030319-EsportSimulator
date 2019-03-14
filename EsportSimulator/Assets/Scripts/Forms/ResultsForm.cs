using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultsForm
{
    [Tooltip("Hours")]
    [SerializeField] private float duration;
    [Tooltip("Percentage")]
    [SerializeField] private float tiredness;
    [Tooltip("Percentage")]
    [SerializeField] private float hunger;
    [Tooltip("Percentage")]
    [SerializeField] private float thirst;
    [SerializeField] private float money;
    [SerializeField] private float moneyMin;
    [SerializeField] private float moneyMax;
    [SerializeField] private float rating;
    [SerializeField] private float fame;
    [SerializeField] private float gameKnowledge;
    [SerializeField] private float teamPlay;
    [SerializeField] private float mechanics;
    [SerializeField] private float workExperience;

    public float Duration { get { return duration; } }
    public float Tiredness { get { return tiredness; } }
    public float Hunger { get { return hunger; } }
    public float Thirst { get { return thirst; } }
    public float Money { get { return money; } }
    public float MoneyMin { get { return moneyMin; } }
    public float MoneyMax { get { return moneyMax; } }
    public float Rating { get { return rating; } }
    public float Fame { get { return fame; } }
    public float GameKnowledge { get { return gameKnowledge; } }
    public float TeamPlay { get { return teamPlay; } }
    public float Mechanics { get { return mechanics; } }
    public float WorkExperience { get { return workExperience; } }
}
