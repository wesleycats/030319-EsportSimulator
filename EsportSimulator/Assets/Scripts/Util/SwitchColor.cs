using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SwitchColor : MonoBehaviour
{
    [SerializeField] private Color switchTo;

    private Image image;
    private Color initialColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        initialColor = image.color;
    }

    public void Switch()
    {
        if (image.color == initialColor)
            image.color = switchTo;
        else
            image.color = initialColor;
    }
}
