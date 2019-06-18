using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// TODO change name of script or integrate in shopmanager
/// </summary>
public class AccommodationMenu : MonoBehaviour
{
    public ShopManager shopManager;
    public GameManager gameManager;
    public UIManager uiManager;
	public PlayerData playerData;

    [SerializeField] private Accommodation accommodation;
    
    private void OnDisable()
    {
        accommodation = new Accommodation(Accommodation.Type.Garage, true);
    }

    public void BuyAccommodation()
    {
        if (accommodation.bought)
        {
            shopManager.SwitchAccommodation(accommodation);
            return;
        }

        if (!gameManager.IsMoneyHighEnough(shopManager.GetAccommodationForm(accommodation).cost))
        {
            Debug.LogWarning("Not Enough Money");
            return;
        }

        shopManager.SwitchAccommodation(accommodation);
    }

    /// <summary>
    /// Call function for dropdown component
    /// </summary>
    public void SetAccommodation(Dropdown dropdown)
    {
        SetAccommodation(dropdown.value);
    }

    /// <summary>
    /// Sets accommodation type based on given level
    /// </summary>
    /// <param name="accommodationLevel"></param>
    public void SetAccommodation(int accommodationLevel)
    {
        accommodation = playerData.GetAllAccommodations[accommodationLevel].accommodation;
    }

    public void Initialize()
    {
        uiManager.UpdateAccommodations(uiManager.allAccommdationButtons, gameManager.GetCurrentAccommodation);
    }

    #region Getters & Setters

    #endregion
}
