using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    [SerializeField] private int maxSaveSlots = 3;

    public GameSaveData gameSaveData;
    public GameManager gameManager;
    public TimeManager timeManager;

    public void SaveGame(int saveSlot)
    {
        if (saveSlot < 0 || saveSlot > maxSaveSlots)
        {
            Debug.LogError("The given slot number is not available");
            return;
        }

        gameSaveData.money = gameManager.GetMoney;
        gameSaveData.rating = gameManager.GetRating;
        gameSaveData.fame = gameManager.GetFame;
        gameSaveData.workExperience = gameManager.GetWorkExperience;
        gameSaveData.workLevel = gameManager.GetWorkLevel;
        gameSaveData.houseLevel = gameManager.GetHouseLevel;
        gameSaveData.tiredness = gameManager.GetTiredness;
        gameSaveData.hunger = gameManager.GetHunger;
        gameSaveData.thirst = gameManager.GetThirst;
        gameSaveData.gameKnowledge = gameManager.GetGameKnowledge;
        gameSaveData.teamPlay = gameManager.GetTeamPlay;
        gameSaveData.mechanics = gameManager.GetMechanics;
        gameSaveData.hour = timeManager.GetHour;
        gameSaveData.minute = timeManager.GetMinute;
        gameSaveData.year = timeManager.GetYear;
        gameSaveData.month = timeManager.GetMonth;
        gameSaveData.saveSlotUsed = 1;

        SaveData(gameSaveData, saveSlot);
    }

    static void SaveData(GameSaveData data, int saveSlot)
    {
        PlayerPrefs.SetInt("saveSlotUsed_SaveSlot" + saveSlot, data.saveSlotUsed);

        PlayerPrefs.SetInt("realDay_SaveSlot" + saveSlot, System.DateTime.Now.Day);
        PlayerPrefs.SetInt("realMonth_SaveSlot" + saveSlot, System.DateTime.Now.Month);
        PlayerPrefs.SetInt("realYear_SaveSlot" + saveSlot, System.DateTime.Now.Year);

        PlayerPrefs.SetFloat("money_SaveSlot" + saveSlot, data.money);
        PlayerPrefs.SetFloat("rating_SaveSlot" + saveSlot, data.rating);
        PlayerPrefs.SetFloat("fame_SaveSlot" + saveSlot, data.fame);
        PlayerPrefs.SetFloat("workExperience_SaveSlot" + saveSlot, data.workExperience);
        PlayerPrefs.SetInt("workLevel_SaveSlot" + saveSlot, data.workLevel);
        PlayerPrefs.SetInt("houseLevel_SaveSlot" + saveSlot, data.houseLevel);

        PlayerPrefs.SetFloat("tiredness_SaveSlot" + saveSlot, data.tiredness);
        PlayerPrefs.SetFloat("hunger_SaveSlot" + saveSlot, data.hunger);
        PlayerPrefs.SetFloat("thirst_SaveSlot" + saveSlot, data.thirst);

        PlayerPrefs.SetInt("gameKnowledge_SaveSlot" + saveSlot, data.gameKnowledge);
        PlayerPrefs.SetInt("teamPlay_SaveSlot" + saveSlot, data.teamPlay);
        PlayerPrefs.SetInt("mechanics_SaveSlot" + saveSlot, data.mechanics);

        PlayerPrefs.SetInt("hour_SaveSlot" + saveSlot, data.hour);
        PlayerPrefs.SetInt("minute_SaveSlot" + saveSlot, data.minute);
        PlayerPrefs.SetInt("year_SaveSlot" + saveSlot, data.year);
        PlayerPrefs.SetInt("month_SaveSlot" + saveSlot, data.month);
        
        PlayerPrefs.Save();
    }

    #region Getters & Setters

    public float GetMaxSaveSlots { get { return maxSaveSlots; } }

    #endregion
}
