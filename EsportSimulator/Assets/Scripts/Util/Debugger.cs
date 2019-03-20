using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public bool noWait;

    public GameManager gameManager;
    public TimeManager timeManager;

    public void AddMoney(float amount)
    {
        gameManager.IncreaseMoney(amount);
    }

    public void AddHours(int amount)
    {
        timeManager.AddHours(amount);
    }

    public void SwitchNoWait()
    {
        noWait = !noWait;
    }
}
