using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisEnableObject : MonoBehaviour
{
    public void Disable(bool enable)
    {
        this.gameObject.SetActive(enable);
    }
}
