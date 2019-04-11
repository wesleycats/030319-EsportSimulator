using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Forms
    
    [SerializeField] private List<AccommodationForm> accommodations = new List<AccommodationForm>();
    [SerializeField] private List<ItemForm> gameGuides = new List<ItemForm>();
    [SerializeField] private List<ItemForm> headsets = new List<ItemForm>();
    [SerializeField] private List<ItemForm> mouses = new List<ItemForm>();
    [SerializeField] private List<ItemForm> keyboards = new List<ItemForm>();
    [SerializeField] private List<ItemForm> screens = new List<ItemForm>();

    private List<List<ItemForm>> allItems = new List<List<ItemForm>>();

    #endregion

    public ArtManager artManager;
    public UIManager uiManager;
    public ResultManager resultManager;
    public GameManager gameManager;
    public PlayerData playerData;

	private void Start()
    {
        allItems.Add(headsets);
        allItems.Add(gameGuides);
        allItems.Add(mouses);
        allItems.Add(keyboards);
        allItems.Add(screens);
    }

    public void BuyItem(Item item, List<List<ItemForm>> allItemList, List<ItemForm> equipedItems)
    {
		if (!gameManager.IsMoneyHighEnough(GetItemForm(item, allItems).cost))
		{
			Debug.LogWarning("Not Enough Money");
			return;
		}

        SetEquipedItems(item, allItemList, equipedItems);
        artManager.UpdateItems(equipedItems);
        uiManager.UpdateItems(uiManager.allItemTexts, equipedItems);

        gameManager.DecreaseMoney(GetItemForm(item, allItems).cost);
        gameManager.IncreaseGameKnowledge(GetItemSkill(item, Skill.Type.GameKnowledge));
        gameManager.IncreaseTeamPlay(GetItemSkill(item, Skill.Type.TeamPlay));
        gameManager.IncreaseMechanics(GetItemSkill(item, Skill.Type.Mechanics));
    }

    public void SwitchAccommodation(Accommodation accommodation, List<AccommodationForm> allAccommodations)
    {
        AccommodationForm currentAccommodation = GetAccommodationForm(accommodation, allAccommodations);

        gameManager.SetCurrentAccommodation = currentAccommodation;
        artManager.UpdateAccommodation(currentAccommodation);
        uiManager.UpdateAccommodations(uiManager.allAccommdationButtons, currentAccommodation, allAccommodations);

        if (!currentAccommodation.accommodation.bought)
        {
            for (int i = 0; i < allAccommodations.Count; i++)
            {
                if (allAccommodations[i].accommodation.type == currentAccommodation.accommodation.type)
                {
                    currentAccommodation.accommodation.bought = true;
                }
            }

            gameManager.DecreaseMoney(currentAccommodation.cost);
        }
    }

    public int GetItemSkill(Item item, Skill.Type type)
    {
        int skillAmount = 0;

        foreach (Skill s in item.skills)
        {
            if (s.type == type)
            {
                return s.amount;
            }
        }

        return skillAmount;
    }

    public ItemForm GetItemForm(Item item, List<List<ItemForm>> allItemList)
    {
        for (int i = 0; i < allItemList.Count; i++)
        {
            for (int j = 0; j < allItemList[i].Count; j++)
            {
                if (item.type == allItemList[i][j].type && item.quality == allItemList[i][j].quality) return allItemList[i][j];
            }
        }

        return allItemList[0][0];
    }

    public AccommodationForm GetAccommodationForm(Accommodation accommodation, List<AccommodationForm> allAccommodationList)
    {
        for (int i = 0; i < allAccommodationList.Count; i++)
        {
            if (accommodation.type == allAccommodationList[i].accommodation.type) return allAccommodationList[i];
        }

        return allAccommodationList[0];
    }

    public void SetEquipedItems(Item item, List<List<ItemForm>> allItemList, List<ItemForm> equipedItems)
    {
        for (int i = 0; i < equipedItems.Count; i++)
        {
            if (item.type != equipedItems[i].type) continue;

            equipedItems[i] = GetItemForm(item, allItemList);

            return;
        }
    }

    public void Initialize()
    {
		accommodations = playerData.GetAllAccommodations;
    }

    #region Getters & Setters

    public List<ItemForm> GetHeadsetList { get { return headsets; } }
    public List<ItemForm> GetGuideList { get { return gameGuides; } }
    public List<ItemForm> GetMouseList { get { return mouses; } }
    public List<ItemForm> GetKeyboardList { get { return keyboards; } }
    public List<ItemForm> GetScreenList { get { return screens; } }
    public List<List<ItemForm>> GetAllItems { get { return allItems; } }
    public List<AccommodationForm> GetAllAccommodations { get { return accommodations; } }

    #endregion
}
