using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    [SerializeField] private int maxGameSlots;

    public GameSaveData gameData;

    public void SaveGame(int gameSlot, int maxGameSlots)
    {
        if (gameSlot < 0 || gameSlot > maxGameSlots)
        {
            Debug.LogError("The given slot number is not available");
            return;
        }

        SaveData(gameData, gameSlot);
    }

    static void SaveData(GameSaveData data, int gameSlot)
    {
        PlayerPrefs.SetFloat("money_GameSlot" + gameSlot, data.money);
        PlayerPrefs.SetFloat("rating_GameSlot" + gameSlot, data.rating);
        PlayerPrefs.SetFloat("fame_GameSlot" + gameSlot, data.fame);
        PlayerPrefs.SetFloat("workExperience_GameSlot" + gameSlot, data.workExperience);
        PlayerPrefs.SetInt("workLevel_GameSlot" + gameSlot, data.workLevel);
        PlayerPrefs.SetInt("houseLevel_GameSlot" + gameSlot, data.houseLevel);

        PlayerPrefs.SetFloat("tiredness_GameSlot" + gameSlot, data.tiredness);
        PlayerPrefs.SetFloat("hunger_GameSlot" + gameSlot, data.hunger);
        PlayerPrefs.SetFloat("thirst_GameSlot" + gameSlot, data.thirst);

        PlayerPrefs.SetInt("gameKnowledge_GameSlot" + gameSlot, data.gameKnowledge);
        PlayerPrefs.SetInt("teamPlay_GameSlot" + gameSlot, data.teamPlay);
        PlayerPrefs.SetInt("mechanics_GameSlot" + gameSlot, data.mechanics);

        PlayerPrefs.SetInt("hour_GameSlot" + gameSlot, data.hour);
        PlayerPrefs.SetInt("minute_GameSlot" + gameSlot, data.minute);
        PlayerPrefs.SetInt("year_GameSlot" + gameSlot, data.year);
        PlayerPrefs.SetInt("month_GameSlot" + gameSlot, data.month);

        PlayerPrefs.SetInt("saved_GameSlot" + gameSlot, data.saved);

    }

}
