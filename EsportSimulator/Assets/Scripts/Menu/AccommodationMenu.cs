using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccommodationMenu : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager uiManager;

    [SerializeField] private Accommodation accommodation;
    
    public void Initialize()
    {
        uiManager.UpdateAccommodations(gameManager.CurrentAccommodation);
    }
}
