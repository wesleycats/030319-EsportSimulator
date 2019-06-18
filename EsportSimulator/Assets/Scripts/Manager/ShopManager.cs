using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Forms
    
    [SerializeField] private List<AccommodationForm> accommodations = new List<AccommodationForm>();
    [SerializeField] private List<Item> gameGuides = new List<Item>();
    [SerializeField] private List<Item> headsets = new List<Item>();
    [SerializeField] private List<Item> mouses = new List<Item>();
    [SerializeField] private List<Item> keyboards = new List<Item>();
    [SerializeField] private List<Item> screens = new List<Item>();

    private List<List<Item>> allItems = new List<List<Item>>();

    #endregion

    public ArtManager artManager;
    public UIManager uiManager;
    public ResultManager resultManager;
    public GameManager gameManager;
    public PlayerData playerData;

	private void Start()
    {
		accommodations = playerData.GetAllAccommodations;
        allItems.Add(headsets);
        allItems.Add(gameGuides);
        allItems.Add(mouses);
        allItems.Add(keyboards);
        allItems.Add(screens);
    }

    public void BuyItem(Item item, List<Item> currentItems)
    {
		if (!gameManager.IsMoneyHighEnough(item.cost))
		{
			Debug.LogWarning("Not Enough Money");
			return;
		}

		artManager.UpdateItems(currentItems);
        uiManager.UpdateItems(uiManager.allItemTexts, currentItems);
        gameManager.DecreaseMoney(item.cost);

		foreach (Skill s in item.skills)
		{
			switch (s.type)
			{
				case Skill.Type.GameKnowledge:
					gameManager.IncreaseGameKnowledge(s.amount);
					break;

				case Skill.Type.Mechanics:
					gameManager.IncreaseTeamPlay(s.amount);
					break;

				case Skill.Type.TeamPlay:
					gameManager.IncreaseMechanics(s.amount);
					break;
			}
		}
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
	
    public AccommodationForm GetAccommodationForm(Accommodation accommodation, List<AccommodationForm> allAccommodationList)
    {
        for (int i = 0; i < allAccommodationList.Count; i++)
        {
            if (accommodation.type == allAccommodationList[i].accommodation.type) return allAccommodationList[i];
        }

        return allAccommodationList[0];
    }

    public void Initialize()
    {
		accommodations = playerData.GetAllAccommodations;
    }

    #region Getters & Setters

    public List<Item> GetHeadsetList { get { return headsets; } }
    public List<Item> GetGuideList { get { return gameGuides; } }
    public List<Item> GetMouseList { get { return mouses; } }
    public List<Item> GetKeyboardList { get { return keyboards; } }
    public List<Item> GetScreenList { get { return screens; } }
    public List<List<Item>> GetAllItems { get { return allItems; } }
    public List<AccommodationForm> GetAllAccommodations { get { return accommodations; } }

    #endregion
}
