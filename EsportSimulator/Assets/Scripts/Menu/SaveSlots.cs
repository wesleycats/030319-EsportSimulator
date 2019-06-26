using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// Manages the saved slots and applies those to the load game menu UI
/// </summary>
public class SaveSlots : MonoBehaviour
{
    public enum SlotStyle { Load, Save }
    public SlotStyle slotStyle;
	public GameObject[] saveSlotsObjects;

	private string dataPath = "";

    private void Start()
    {
        ApplySavedSlots(saveSlotsObjects);
    }

    public bool isSaveSlotUsed(int saveSlot)
    {

		if (Application.platform != RuntimePlatform.WindowsEditor)
		{
			dataPath = Path.Combine(Application.persistentDataPath, "GameSaveData_SaveSlot_" + saveSlot + ".txt");
		}
		else
		{ 
			dataPath = Path.Combine(Application.dataPath + "/Saves/", "GameSaveData_SaveSlot_" + saveSlot + ".txt");
		}

		if (!File.Exists(dataPath)) return false;

        return true;
    }

    public void ApplySavedSlots()
    {
        ApplySavedSlots(saveSlotsObjects);
    }

    public void ApplySavedSlots(GameObject[] saveSlots)
    {
        for (int i = 0; i < saveSlots.Length; i++)
        {
            if (!isSaveSlotUsed(i))
            {
                EmptySaveSlot(saveSlots, i);
                continue;
            }

            switch (slotStyle)
            {
                case SlotStyle.Load:
                    saveSlots[i].GetComponent<Button>().interactable = true;
                    saveSlots[i].transform.GetChild(1).GetComponent<Button>().interactable = true;
                    break;

                case SlotStyle.Save:
                    saveSlots[i].GetComponent<Button>().interactable = false;
                    saveSlots[i].transform.GetChild(1).GetComponent<Button>().interactable = true;
                    break;
            }

            saveSlots[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "saved on " + PlayerPrefs.GetInt("realDay_SaveSlot" + i) + "/" + PlayerPrefs.GetInt("realMonth_SaveSlot" + i) + "/" + PlayerPrefs.GetInt("realYear_SaveSlot" + i);
            
            if (PlayerPrefs.GetInt("hour_SaveSlot" + i) < 10)
                saveSlots[i].transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "progress:\n month " + PlayerPrefs.GetInt("month_SaveSlot" + i) + " of year " + PlayerPrefs.GetInt("year_SaveSlot" + i) + "\n 0" + PlayerPrefs.GetInt("hour_SaveSlot" + i) + ":00";
            else
                saveSlots[i].transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "progress:\n month " + PlayerPrefs.GetInt("month_SaveSlot" + i) + " of year " + PlayerPrefs.GetInt("year_SaveSlot" + i) + "\n" + PlayerPrefs.GetInt("hour_SaveSlot" + i) + ":00";

            saveSlots[i].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = "load game";
        }
    }

    public void EmptySaveSlot(GameObject[] saveSlots, int slot)
    {
        switch (slotStyle)
        { 
            case SlotStyle.Load:
                saveSlots[slot].GetComponent<Button>().interactable = false;
                break;

            case SlotStyle.Save:
                saveSlots[slot].GetComponent<Button>().interactable = true;
                break;
        }

        saveSlots[slot].transform.GetChild(1).GetComponent<Button>().interactable = false;
        saveSlots[slot].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";
        saveSlots[slot].transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "empty slot";
        saveSlots[slot].transform.GetChild(0).transform.GetChild(2).GetComponent<Text>().text = "";

    }
}
