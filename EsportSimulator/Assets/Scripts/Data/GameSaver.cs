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
    public ShopManager shopManager;

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

		gameSaveData.plannedTournaments = gameManager.GetPlannedEvents;

		//save current accommoadation: gameSaveData.currentAccommodation = gameManager.GetCurrentAccommodation;

		gameSaveData.currentItems.Clear();
		foreach (Item i in gameManager.CurrentItems)
		{
			gameSaveData.currentItems.Add(i);
		}

		gameSaveData.opponents = new Opponent[opponentManager.GetAllOpponents.Count];
        for (int i = 0; i < opponentManager.GetAllOpponents.Count; i++)
		{
			gameSaveData.opponents[i] = opponentManager.GetAllOpponents[i];
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
