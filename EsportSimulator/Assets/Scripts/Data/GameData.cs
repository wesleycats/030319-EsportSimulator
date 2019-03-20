﻿using System.Collections;
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
    [SerializeField] private int minute;
    [SerializeField] private int month;
    [SerializeField] private int year;

    #endregion

    #region Utilities

    #endregion

    public void Reset(bool reset)
    {
        hour = 0;
        minute = 0;
        month = 0;
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

    #endregion
}

