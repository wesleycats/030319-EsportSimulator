using UnityEngine;

/// <summary>
/// Controls dedicated art
/// </summary>
public class ArtManager : MonoBehaviour
{
    #region Art Elements

    public SpriteRenderer background;
    public SpriteRenderer character;
    public SpriteRenderer headset;
    public SpriteRenderer keyboard;
    public SpriteRenderer screen;
    public SpriteRenderer chair;
    public SpriteRenderer workstation;

    #endregion

    #region Sprites
    
    public Sprite[] houseSprites;
    public Sprite[] workplaceSprites;
    public Sprite[] characterSprites;
    public Sprite[] headsetSprites;
    public Sprite[] keyboardSprites;
    public Sprite[] screenSprites;
    public Sprite[] chairSprites;
    public Sprite[] workstationSprites;

    #endregion

    #region Animations

    public Animator playerAnimator; 
    public Animator workstationAnimator;

    #endregion

    public PlayerData playerData;
    
    /// <summary>
    /// Changes art according to activity and progress
    /// </summary>
    /// <param name="activity"></param>
    public void ChangeArt(ActivityManager.Activity activity)
    {
        switch (activity)
        {
            case ActivityManager.Activity.Battle:

                break;

            case ActivityManager.Activity.Contest:

                break;

            case ActivityManager.Activity.Drink:

                break;

            case ActivityManager.Activity.Eat:

                break;

            case ActivityManager.Activity.Idle:
                playerAnimator.Play("Idle");
                workstationAnimator.Play("Idle");

                switch (playerData.HouseLevel)
                {
                    case 1:
                        background.sprite = houseSprites[0];

                        break;

                    case 2:
                        background.sprite = houseSprites[1];

                        break;

                    case 3:
                        background.sprite = houseSprites[2];

                        break;

                    case 4:
                        background.sprite = houseSprites[3];

                        break;

                    default:

                        break;
                }

                break;

            case ActivityManager.Activity.Sleep:

                break;

            case ActivityManager.Activity.Stream:

                break;

            case ActivityManager.Activity.Train:

                break;

            case ActivityManager.Activity.Work:
                
                switch (playerData.WorkLevel)
                {
                    case 1:
                        playerAnimator.Play("Working1");
                        workstationAnimator.Play("Grill");
                        background.sprite = workplaceSprites[0];
                        screen.sprite = null;
                        keyboard.sprite = null;

                        break;

                    case 2:
                        background.sprite = workplaceSprites[1];

                        break;

                    case 3:
                        background.sprite = workplaceSprites[2];

                        break;

                    case 4:
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
}
