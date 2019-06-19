using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// TODO fuse in shopmanager
/// </summary>
public class AccommodationMenu : MonoBehaviour
{
    public ShopManager shopManager;
    public GameManager gameManager;
    public UIManager uiManager;
	public PlayerData playerData;

    [SerializeField] private Accommodation accommodation;
    
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
        accommodation = playerData.GetAllAccommodations[accommodationLevel];
    }

    public void Initialize()
    {
        uiManager.UpdateAccommodations(gameManager.CurrentAccommodation);
    }

    #region Getters & Setters

    #endregion
}
