using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public bool noWait;
    public bool noLose;

    public GameManager gameManager;
    public TimeManager timeManager;

    public void IncreaseMoney(float amount)
    {
        gameManager.IncreaseMoney(amount);
    }

    public void DecreaseMoney(float amount)
    {
        gameManager.DecreaseMoney(amount);
    }

    public void IncreaseHours(int amount)
    {
        timeManager.IncreaseHours(amount);
    }

    public void DecreaseHours(int amount)
    {
        timeManager.DecreaseHours(amount);
    }

    public void SwitchNoWait()
    {
        noWait = !noWait;
    }

    public void SwitchNoGameOver()
    {
        noLose = !noLose;
    }
}
