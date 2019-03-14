using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for all data about the player
/// </summary>
[CreateAssetMenu(fileName = "New GameData", menuName = "Game Data", order = 52)]
public class GameData : ScriptableObject
{
    #region Default Variables

    #endregion

    #region Time

    [Header("Time")]
    [SerializeField] private int hour;
    [SerializeField] private int month;
    [SerializeField] private int year;

    #endregion

    #region Utilities

    [SerializeField] private bool saved;
    [SerializeField] private bool debug;

    #endregion

    public void Reset(bool reset)
    {
        hour = 0;
        month = 0;
        year = 0;
    }

    public void SetTime(int h)
    {
        if (!saved || !debug) return;

        hour = h;
    }

    public void SetDate(int m, int y)
    {
        if (!saved || !debug) return;

        month = m;
        year = y;
    }

    #region Getters & Setters
    
    public int Hour { get { return hour; } }
    public int Month { get { return month; } }
    public int Year { get { return year; } }
    public bool Saved { get { return saved; } set { saved = value; } }

    #endregion
}

