using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpPosition : MonoBehaviour
{

    [SerializeField] private bool lerp;
    [Range(0, 1)]
    [SerializeField] private float lerpValue;
    [SerializeField] private float lerpSpeed;

    private float lerpSteps;
    private bool increasing = true;

    public Transform startMarker;
    public Transform endMarker;
    public Transform objectToMove;

    void Update()
    {
        if (!lerp) return;

        lerpSteps = Time.deltaTime * lerpSpeed;

        if (increasing)
            lerpValue += lerpSteps;
        else
            lerpValue -= lerpSteps;

        objectToMove.position = Vector3.Lerp(startMarker.position, endMarker.position, lerpValue);

        if (lerpValue > 1)
        {
            lerp = false;
            lerpValue = 1;
            increasing = false;
        }

        if (lerpValue < 0)
        {
            lerp = false;
            lerpValue = 0;
            increasing = true;
        }

    }

    public void Lerp(bool value)
    {
        lerp = value;
    }
}
