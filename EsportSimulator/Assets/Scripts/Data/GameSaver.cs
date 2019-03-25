using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSaver : MonoBehaviour
{
    [SerializeField] private int maxSaveSlots = 3;

    public GameSaveData gameSaveData;
    public GameManager gameManager;
    public OpponentManager opponentManager;
    public TimeManager timeManager;

    private string dataPath;

    public void SaveGame(int saveSlot)
    {
        if (saveSlot < 0 || saveSlot > maxSaveSlots)
        {
            Debug.LogError("The given slot number is not available");
            return;
        }

        dataPath = Path.Combine(Application.persistentDataPath, "GameSaveData_SaveSlot_" + saveSlot + ".txt");

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
        
        gameSaveData.opponents = new Opponent[opponentManager.GetOpponentAmount];

        for (int i = 0; i < opponentManager.GetOpponentAmount; i++)
        {
            gameSaveData.opponents[i] = opponentManager.GetOpponents[i];
        }

        SaveData(gameSaveData, saveSlot, dataPath);
    }

    static void SaveData(GameSaveData data, int saveSlot, string path)
    {
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }

    #region Getters & Setters

    public float GetMaxSaveSlots { get { return maxSaveSlots; } }

    #endregion
}
