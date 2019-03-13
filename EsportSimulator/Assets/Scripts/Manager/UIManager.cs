using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Just for initialization
    #region UI Elements

    public Text moneyValue;
    public Text ratingValue;
    public Text fameValue;
    public Text tirednessValue;
    public Slider tirednessBar;
    public Text hungerValue;
    public Slider hungerBar;
    public Text thirstValue;
    public Slider thirstBar;
    public Text gameKnowledgeValue;
    public Text teamPlayValue;
    public Text mechanicsValue;
    public Text activityText;

    #endregion

    public PlayerData playerData;

    /// <summary>
    /// Initializes player data in UI
    /// </summary>
    public void InitializePlayerData()
    {
        moneyValue.text = playerData.Money.ToString();
        ratingValue.text = playerData.Rating.ToString();
        fameValue.text = playerData.Fame.ToString();
        tirednessValue.text = playerData.Tiredness.ToString() + "%";
        hungerValue.text = playerData.Hunger.ToString() + "%";
        thirstValue.text = playerData.Thirst.ToString() + "%";
        gameKnowledgeValue.text = playerData.GameKnowledge.ToString();
        teamPlayValue.text = playerData.TeamPlay.ToString();
        mechanicsValue.text = playerData.Mechanics.ToString();

        tirednessBar.value = playerData.Tiredness / 100;
        hungerBar.value = playerData.Hunger / 100;
        thirstBar.value = playerData.Thirst / 100;
    }
}
