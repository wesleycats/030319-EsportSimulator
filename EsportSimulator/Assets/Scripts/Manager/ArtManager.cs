using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls dedicated art
/// </summary>
public class ArtManager : MonoBehaviour
{
    #region Art Elements

    public SpriteRenderer background;
    public SpriteRenderer character;
    public SpriteRenderer chair;
    public SpriteRenderer workstation;
    public SpriteRenderer headset;
    public SpriteRenderer keyboard;
    public SpriteRenderer screen;
    #endregion

    #region Sprites

    public Sprite[] houseSprites;
    public Sprite[] workplaceSprites;
    public Sprite[] characterSprites;
    public Sprite[] headsetSprites;
    public Sprite[] keyboardSprites;
    public Sprite[] screenSprites;
    public Sprite[] chairSprites;
    public Sprite[] tableSprites;
    public Sprite[] workstationSprites;

    #endregion

    #region Animations

    public Animator playerAnimator; 
    public Animator workstationAnimator;

    #endregion

    public PlayerData playerData;
    public ShopManager shopManager;
    public GameManager gameManager;

    /// <summary>
    /// Changes art according to activity and progress
    /// </summary>
    /// <param name="activity"></param>
    public void ChangeArt(ActivityManager.Activity activity)
    {
        switch (activity)
        {
            case ActivityManager.Activity.Battle:
                playerAnimator.Play("OnComputer");
                workstationAnimator.Play("WoodenTable");

                background.sprite = houseSprites[(int)playerData.GetHouseLevel];
                chair.sprite = chairSprites[(int)playerData.GetHouseLevel];
                workstation.sprite = tableSprites[(int)playerData.GetHouseLevel];
                break;

            case ActivityManager.Activity.Contest:

                break;

            case ActivityManager.Activity.Drink:

                break;

            case ActivityManager.Activity.Eat:

                break;

            case ActivityManager.Activity.Idle:
                playerAnimator.Play("Idle");
                workstationAnimator.Play("WoodenTable");

				// Sets item sprite to currently equiped item
				foreach (ItemForm f in gameManager.GetEquipedItems)
				{
					switch (f.type)
					{
						case Item.Type.Keyboard:
							keyboard.sprite = f.sprite;
							break;

						case Item.Type.Screen:
							screen.sprite = f.sprite;
							break;
					}
				}

                background.sprite = houseSprites[(int)playerData.GetHouseLevel];
                chair.sprite = chairSprites[(int)playerData.GetHouseLevel];
                workstation.sprite = tableSprites[(int)playerData.GetHouseLevel];
                break;

            case ActivityManager.Activity.Sleep:

                break;

            case ActivityManager.Activity.Stream:
				playerAnimator.Play("OnComputer");
				workstationAnimator.Play("WoodenTable");
				break;

            case ActivityManager.Activity.Train:
                playerAnimator.Play("OnComputer");
                workstationAnimator.Play("WoodenTable");
                break;

            case ActivityManager.Activity.Work:
                background.sprite = workplaceSprites[(int)playerData.GetWorkLevel];
                screen.sprite = null;
                keyboard.sprite = null;
                
                switch (playerData.GetWorkLevel)
                {
                    case 0:
                        playerAnimator.Play("Working1");
                        workstationAnimator.Play("Grill");
                        chair.sprite = null;

                        break;

                    case 1:
                        background.sprite = workplaceSprites[1];

                        break;

                    case 2:
                        background.sprite = workplaceSprites[2];

                        break;

                    case 3:
                        background.sprite = workplaceSprites[3];

                        break;

                    default:

                        break;
                }

                break;

            default:

                break;

        }

    }

    public void UpdateItems(List<ItemForm> equipedItems)
    {
        foreach (ItemForm f in equipedItems)
        { 
            switch (f.type)
            {
                case Item.Type.Headset:
                    headset.sprite = f.sprite;
                    break;
                case Item.Type.Keyboard:
                    keyboard.sprite = f.sprite;
                    break;
                case Item.Type.Screen:
                    screen.sprite = f.sprite;
                    break;
            }
        }
    }

    public void UpdateAccommodation(AccommodationForm currentAccommodation)
    {
        background.sprite = currentAccommodation.sprite;
    }

    public void Initialize()
    {
        UpdateItems(gameManager.GetEquipedItems);
    }
}
