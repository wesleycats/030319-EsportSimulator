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

    #region In-game time

    [Header("Time")]
    [SerializeField] private int hour;
    [SerializeField] private int minute;
    [SerializeField] private int month;
    [SerializeField] private int year;

    #endregion

    #region Real date

    public int realDay;
    public int realMonth;
    public int realYear;

    #endregion

    #region Utilities

    #endregion

    public void Reset(bool reset)
    {
        hour = 0;
        minute = 0;
        month = 1;
        year = 0;
    }

    #region Getters & Setters

    public int GetHour { get { return hour; } }
    public int GetMinute { get { return minute; } }
    public int GetMonth { get { return month; } }
    public int GetYear { get { return year; } }

    public int SetHour { set { hour = value; } }
    public int SetMinute { set { minute = value; } }
    public int SetMonth { set { month = value; } }
    public int SetYear { set { year = value; } }

    public int SetRealDay { set { realDay = value; } }
    public int SetRealMonth { set { realMonth = value; } }
    public int SetRealYear { set { realYear = value; } }


    #endregion
}

