﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameLoader : MonoBehaviour
{
	public enum LoadType { None, NewGame, LoadGame }

	[SerializeField] private float loadTime;

    public GameSaver gameSaver;
    public GameData gameData;
    public PlayerData playerData;
    public GameObject mainMenu;
    public ButtonManager buttonManager;
    public OpponentManager opponentManager;
    public GameSaveData gameSaveData;
	public GameManager gameManager;

    private int saveSlotToBeCleared = 0;
	private int slotToBeLoaded = 0;
    private string dataPath = "";

	public void LoadGame(int saveSlot)
    {
        if (saveSlot < 0 || saveSlot > gameSaver.GetMaxSaveSlots)
        {
            Debug.LogError("The given slot number is not available");
            return;
        }

		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			dataPath = Path.Combine(Application.persistentDataPath, "GameSaveData_SaveSlot_" + saveSlot + ".txt");
		}
		else
		{
			dataPath = Path.Combine(Application.dataPath + "/Saves/", "GameSaveData_SaveSlot_" + saveSlot + ".txt");
		}

		LoadData(saveSlot, dataPath);
    }

	public void LoadData(int saveSlot, string path)
    {
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();
            gameSaveData = JsonUtility.FromJson<GameSaveData>(jsonString);
        }

        playerData.SetMoney = gameSaveData.money;
        playerData.SetRating = gameSaveData.rating;
        playerData.SetFame = gameSaveData.fame;
        playerData.WorkExperience = gameSaveData.workExperience;
        playerData.SetWorkLevel = gameSaveData.workLevel;

        playerData.SetTiredness = gameSaveData.tiredness;
        playerData.SetHunger = gameSaveData.hunger;
        playerData.SetThirst = gameSaveData.thirst;

        playerData.SetGameKnowledge = gameSaveData.gameKnowledge;
        playerData.SetTeamPlay = gameSaveData.teamPlay;
        playerData.SetMechanics = gameSaveData.mechanics;

		playerData.CurrentAccommodation = gameSaveData.currentAccommodation;

		playerData.CurrentItems.Clear();
		foreach (Item i in gameSaveData.currentItems)
		{
			playerData.CurrentItems.Add(i);
		}
		
		playerData.SetPlannedTournaments = gameSaveData.plannedTournaments;

        gameData.SetHour = gameSaveData.hour;
        gameData.SetMinute = gameSaveData.minute;
        gameData.SetYear = gameSaveData.year;
        gameData.SetMonth = gameSaveData.month;

        opponentManager.ApplyOpponents(gameSaveData.opponents, opponentManager.GetAllOpponents);
    }

    public void ClearSlot()
    {
		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			File.Delete(Path.Combine(Application.persistentDataPath, "GameSaveData_SaveSlot_" + saveSlotToBeCleared + ".txt"));
		}
		else
		{
			File.Delete(Path.Combine(Application.dataPath + "/Saves/", "GameSaveData_SaveSlot_" + saveSlotToBeCleared + ".txt"));
		}
	}

    #region Getters & Setters

    public float GetLoadTime { get { return loadTime; } }
    public int SetSaveSlotToBeCleared { set { saveSlotToBeCleared = value; } }

    #endregion
}
