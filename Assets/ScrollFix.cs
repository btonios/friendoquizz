using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollFix : MonoBehaviour
{
    TMP_InputField ifName;
    public bool inter;
    public void DraggingStart()
    {
        ifName = this.GetComponentInChildren<TMP_InputField>();
        ifName.interactable = false;
        inter = ifName.interactable;
    }

    public void DraggingEnd()
    {
        ifName = this.GetComponentInChildren<TMP_InputField>();
        ifName.interactable = true;
        inter = ifName.interactable;
    }
}
