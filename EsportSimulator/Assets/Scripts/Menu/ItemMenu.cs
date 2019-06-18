using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// TODO change name of script or integrate in shopmanager
/// </summary>
public class ItemMenu : MonoBehaviour
{
    public Text itemTitle;
    public GameObject badQuality;
    public GameObject standardQuality;
    public GameObject goodQuality;
    public GameObject excellentQuality;

    [SerializeField] private Item item;
    private Button[] buttons;
	private Text[] results;

	[Header("References")]
	public PlayerData playerData;
	public ShopManager shopManager;
	public GameManager gameManager;

	private void OnDisable()
    {
        Skill[] skills = new Skill[0];
        item = new Item(Item.Type.None, Item.Quality.Default, 0, skills, null);

        foreach (Button b in buttons)
            b.interactable = true;
    }

    public void BuyItem()
    {
		SetItem(item.type, item.quality);
		Debug.Log(item.type);
		Debug.Log(item.quality);
		Debug.Log(item.cost);

		if (!gameManager.IsMoneyHighEnough(item.cost))
        {
            Debug.LogWarning("Not Enough Money");
            return;
        }

		shopManager.BuyItem(item, gameManager.GetCurrentItems);
		ApplyCurrentItems(gameManager.GetCurrentItems);
    }

	public void ApplyCurrentItems(List<Item> currentItems)
    {
        foreach (Item i in currentItems)
        {
			switch (i.quality)
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

	public void SetItem(Item.Type type, Item.Quality quality)
	{
		foreach (ItemForm f in playerData.GetAllItems)
		{
			if (f.itemType != type.ToString()) continue;

			foreach (Item i in f.qualities)
			{
				Debug.Log(i.type);
				Debug.Log(i.quality);
				Debug.Log(i.cost);
				if (i.quality != quality) continue;

				item = i;
				return;
			}
		}
	}

    public void ApplyItemTitle(Text itemTitle, string text)
    {
        itemTitle.text = text;
    }
    
    public void ApplyItemResults(Item.Type type)
    {
        for (int i = 0; i < results.Length; i++)
        {
            results[i].text = "";
        }

		List<int> gameKnowledge = new List<int>();
		List<int> mechanics = new List<int>();
		List<int> teamPlay = new List<int>();
		List<int> cost = new List<int>();

		foreach (ItemForm f in playerData.GetAllItems)
		{
			if (f.itemType != type.ToString()) continue;

			for (int i = 0; i < f.qualities.Count; i++)
			{
				if (f.qualities[i].quality == Item.Quality.Default) continue;

				foreach (Skill s in f.qualities[i].skills)
				{
					switch (s.type)
					{
						case Skill.Type.GameKnowledge:
							gameKnowledge.Add(s.amount);
							break;

						case Skill.Type.Mechanics:
							mechanics.Add(s.amount);
							break;

						case Skill.Type.TeamPlay:
							teamPlay.Add(s.amount);
							break;
					}
				}

				cost.Add(f.qualities[i].cost);
			}
		}

		for (int i = 0; i < results.Length; i++)
		{
			switch (item.type)
			{
				case Item.Type.Guide:
					results[i].text = "+" + gameKnowledge[i] + " game knowledge, -$" + cost[i];
					break;

				case Item.Type.Headset:
					results[i].text = "+" + teamPlay[i] + " team play, -$" + cost[i];
					break;

				case Item.Type.Keyboard:
					results[i].text = "+" + mechanics[i] + " mechanics, -$" + cost[i];
					break;

				case Item.Type.Mouse:
					results[i].text = "+" + mechanics[i] + " mechanics, -$" + cost[i];
					break;

				case Item.Type.Screen:
					results[i].text = "+" + teamPlay[i] + " team play,\n +" + gameKnowledge[i] + " game knowledge, -$" + cost[i];
					break;
			}
		}
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

        ApplyItemResults(item.type);
        ApplyItemTitle(itemTitle, item.type.ToString());
		ApplyCurrentItems(gameManager.GetCurrentItems);
    }

    #region Getters & Setters

    public Item GetItem { get { return item; } }

	#endregion
}
