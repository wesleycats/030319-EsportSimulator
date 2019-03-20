using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private float loadTime;

    public LerpColor switchOverlay;
    public GameSaver gameSaver;
    public GameData gameData;
    public PlayerData playerData;
    public GameObject mainMenu;
    public ButtonManager buttonManager;

    private int saveSlotToBeCleared = 0;

    public void LoadGame(int saveSlot)
    {
        if (saveSlot < 0 || saveSlot > gameSaver.GetMaxSaveSlots)
        {
            Debug.LogError("The given slot number is not available");
            return;
        }

        switchOverlay.LerpMaxAmount = 1;
        switchOverlay.Increasing = true;
        switchOverlay.Lerping = true;
        switchOverlay.LerpActivated = true;
        StartCoroutine(LoadDelayer(saveSlot));
    }

    private IEnumerator LoadDelayer(int saveSlot)
    {
        yield return new WaitForSeconds(loadTime);

        if (!switchOverlay.Lerping && switchOverlay.LerpValue == 1)
        {
            LoadData(saveSlot);
            switchOverlay.LerpMaxAmount = 1;
            switchOverlay.Increasing = false;
            switchOverlay.Lerping = true;
            switchOverlay.LerpActivated = true;
            buttonManager.EnableAllButtons();
        }
        else
        {
            StartCoroutine(LoadDelayer(saveSlot));
        }
    }

    public void LoadData(int saveSlot)
    {
        playerData.SetMoney = PlayerPrefs.GetFloat("money_SaveSlot" + saveSlot);
        playerData.SetRating = PlayerPrefs.GetFloat("rating_SaveSlot" + saveSlot);
        playerData.SetFame = PlayerPrefs.GetFloat("fame_SaveSlot" + saveSlot);
        playerData.SetWorkExperience = PlayerPrefs.GetFloat("workExperience_SaveSlot" + saveSlot);
        playerData.SetWorkLevel = PlayerPrefs.GetInt("workLevel_SaveSlot" + saveSlot);
        playerData.SetHouseLevel = PlayerPrefs.GetInt("houseLevel_SaveSlot" + saveSlot);

        playerData.SetTiredness = PlayerPrefs.GetFloat("tiredness_SaveSlot" + saveSlot);
        playerData.SetHunger = PlayerPrefs.GetFloat("hunger_SaveSlot" + saveSlot);
        playerData.SetThirst = PlayerPrefs.GetFloat("thirst_SaveSlot" + saveSlot);

        playerData.SetGameKnowledge = PlayerPrefs.GetInt("gameKnowledge_SaveSlot" + saveSlot);
        playerData.SetTeamPlay = PlayerPrefs.GetInt("teamPlay_SaveSlot" + saveSlot);
        playerData.SetMechanics = PlayerPrefs.GetInt("mechanics_SaveSlot" + saveSlot);

        gameData.SetHour = PlayerPrefs.GetInt("hour_SaveSlot" + saveSlot);
        gameData.SetMinute = PlayerPrefs.GetInt("minute_SaveSlot" + saveSlot);
        gameData.SetYear = PlayerPrefs.GetInt("year_SaveSlot" + saveSlot);
        gameData.SetMonth = PlayerPrefs.GetInt("month_SaveSlot" + saveSlot);

        mainMenu.SetActive(false);

        switchOverlay.LerpMaxAmount = 1;
        switchOverlay.Increasing = false;
        switchOverlay.Lerping = true;
        switchOverlay.LerpActivated = true;
    }

    public void ClearSlot()
    {
        PlayerPrefs.SetInt("saveSlotUsed_SaveSlot" + saveSlotToBeCleared, 0);
    }

    #region Getters & Setters

    public float GetLoadTime { get { return loadTime; } }
    public int SetSaveSlotToBeCleared { set { saveSlotToBeCleared = value; } }

    #endregion
}
