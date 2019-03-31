using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// TODO change name of script or integrate in shopmanager
/// </summary>
public class ItemMenu : MonoBehaviour
{
    public ShopManager shopManager;
    public GameManager gameManager;
    public Text itemTitle;
    public GameObject badQuality;
    public GameObject standardQuality;
    public GameObject goodQuality;
    public GameObject excellentQuality;

    [SerializeField] private Item item;
    private Button[] buttons;
    private Text[] results;
    
    private void OnDisable()
    {
        Skill[] skills = new Skill[0];
        item = new Item(Item.Type.None, Item.Quality.Default, skills);

        foreach (Button b in buttons)
            b.interactable = true;
    }

    public void BuyItem()
    {
        if (!gameManager.IsMoneyHighEnough(shopManager.GetItemForm(item, shopManager.GetAllItems).cost))
        {
            Debug.LogWarning("Not Enough Money");
            return;
        }

        shopManager.BuyItem(item, shopManager.GetAllItems, gameManager.GetEquipedItems);
        ApplyAccquiredItems(item, gameManager.GetEquipedItems);
    }

    public void ApplyAccquiredItems(Item item, List<ItemForm> equipedItems)
    {
        for (int i = 0; i < equipedItems.Count; i++)
        {
            if (item.type != equipedItems[i].type) continue;

            switch (equipedItems[i].quality)
            {
                case Item.Quality.Bad:
                    buttons[0].interactable = false;
                    results[0].text = results[0].text + "\n *equiped*"; 
                    break;

                case Item.Quality.Standard:
                    buttons[0].interactable = false;
                    buttons[1].interactable = false;
                    break;

                case Item.Quality.Good:
                    buttons[0].interactable = false;
                    buttons[1].interactable = false;
                    buttons[2].interactable = false;
                    break;

                case Item.Quality.Excellent:
                    buttons[0].interactable = false;
                    buttons[1].interactable = false;
                    buttons[2].interactable = false;
                    buttons[3].interactable = false;
                    break;
            }

            return;
        }
    }
    
    public void SetItemType(string type)
    {
        switch (type)
        {
            case "Guide":
                item.type = Item.Type.Guide;
                break;

            case "Headset":
                item.type = Item.Type.Headset;
                break;

            case "Mouse":
                item.type = Item.Type.Mouse;
                break;

            case "Keyboard":
                item.type = Item.Type.Keyboard;
                break;

            case "Screen":
                item.type = Item.Type.Screen;
                break;

            default:
                Debug.LogError("Given type is not available (try: Guide, Headset, Mouse, Keyboard or Screen)");
                break;
        }
    }
    
    public void SetItemQuality(string quality)
    {
        switch (quality)
        {
            case "Bad":
                item.quality = Item.Quality.Bad;
                break;

            case "Standard":
                item.quality = Item.Quality.Standard;
                break;

            case "Good":
                item.quality = Item.Quality.Good;
                break;

            case "Excellent":
                item.quality = Item.Quality.Excellent;
                break;

            default:
                Debug.LogError("Given quality '" + quality + "' is not available (try Bad, Standard, Good or Excellent)");
                break;
        }
    }

    public void SetItemSkills()
    {
        item.skills = SetSkills(item, shopManager.GetAllItems);
    }

    public void ApplyItemTitle(Text itemTitle, string text)
    {
        itemTitle.text = text;
    }
    
    public void ApplyItemResults(Item item, List<List<ItemForm>> itemList)
    {
        List<ItemForm> itemType = new List<ItemForm>();

        for (int i = 0; i < results.Length; i++)
        {
            results[i].text = "";
        }

        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = 0; j < itemList[i].Count; j++)
            {
                if (item.type == itemList[i][j].type)
                {
                    itemType = itemList[i];
                }
            }
        }

        switch (item.type)
        {
            case Item.Type.Guide:
                for (int i = 0; i < results.Length; i++)
                {
                    results[i].text = "+" + itemType[i].gameKnowledge + " game knowledge, -$" + itemType[i].cost;
                }
                break;

            case Item.Type.Headset:
                for (int i = 0; i < results.Length; i++)
                {
                    results[i].text = "+" + itemType[i].teamPlay + " team play, -$" + itemType[i].cost;
                }
                break;

            case Item.Type.Keyboard:
                for (int i = 0; i < results.Length; i++)
                {
                    results[i].text = "+" + itemType[i].mechanics + " mechanics, -$" + itemType[i].cost;
                }
                break;

            case Item.Type.Mouse:
                for (int i = 0; i < results.Length; i++)
                {
                    results[i].text = "+" + itemType[i].mechanics + " mechanics, -$" + itemType[i].cost;
                }
                break;

            case Item.Type.Screen:
                for (int i = 0; i < results.Length; i++)
                {
                    results[i].text = "+" + itemType[i].teamPlay + " team play,\n +" + itemType[i].gameKnowledge + " game knowledge, -$" + itemType[i].cost;
                }
                break;
        }
    }

    public Skill[] SetSkills(Item item, List<List<ItemForm>> itemList)
    {
        List<Skill> skillsBuffer = new List<Skill>();

        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = 0; j < itemList[i].Count; j++)
            {
                if (item.type == itemList[i][j].type && item.quality == itemList[i][j].quality)
                {
                    skillsBuffer.Add(new Skill(Skill.Type.GameKnowledge, itemList[i][j].gameKnowledge));
                    skillsBuffer.Add(new Skill(Skill.Type.TeamPlay, itemList[i][j].teamPlay));
                    skillsBuffer.Add(new Skill(Skill.Type.Mechanics, itemList[i][j].mechanics));
                }
            }
        }

        Skill[] skills = new Skill[skillsBuffer.Count];

        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] = skillsBuffer[i];
        }

        return skills;
    }

    public void Initialize()
    {
        buttons = new Button[4];
        buttons[0] = badQuality.transform.Find("Button").GetComponent<Button>();
        buttons[1] = standardQuality.transform.Find("Button").GetComponent<Button>();
        buttons[2] = goodQuality.transform.Find("Button").GetComponent<Button>();
        buttons[3] = excellentQuality.transform.Find("Button").GetComponent<Button>();

        results = new Text[4];
        results[0] = badQuality.transform.Find("Result").GetComponent<Text>();
        results[1] = standardQuality.transform.Find("Result").GetComponent<Text>();
        results[2] = goodQuality.transform.Find("Result").GetComponent<Text>();
        results[3] = excellentQuality.transform.Find("Result").GetComponent<Text>();

        ApplyItemTitle(itemTitle, item.type.ToString());
        ApplyItemResults(item, shopManager.GetAllItems);
        ApplyAccquiredItems(item, gameManager.GetEquipedItems);
    }

    #region Getters & Setters

    public Item GetItem { get { return item; } }

    #endregion
}
