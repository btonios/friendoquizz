using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class Popup
{

    private static GameObject PopGO;
    private static string title;
    private static string content;
    private static GameObject popupParent;
    public static bool popover;

    public static void Initialise(string newTitle, string newContent, GameObject parent, bool pover = false)
    {
        if(pover == false)
            PopGO = Resources.Load("panelPopup", typeof(GameObject)) as GameObject;
        else
            PopGO = Resources.Load("panelPopover", typeof(GameObject)) as GameObject;
        
        title = newTitle;
        content = newContent;
        popupParent = parent;
        popover = pover;
    }

    public static void Show()
    {
        if(popover == true)
            ShowPopOver();
        else
            ShowPopUp();
    }

    private static void ShowPopOver()
    {
        GameObject pop = MonoBehaviour.Instantiate(PopGO, new Vector3(popupParent.transform.position.x, popupParent.transform.position.y + PopGO.GetComponent<RectTransform>().sizeDelta.y, popupParent.transform.position.z), Quaternion.identity);
        SetText(pop);
        pop.transform.SetParent(popupParent.transform);
    }

    private static void ShowPopUp()
    {
        GameObject pop = MonoBehaviour.Instantiate(PopGO, new Vector3(0, 0, 0), Quaternion.identity);
        SetText(pop);
        pop.transform.SetParent(popupParent.transform, false);
    }

    private static void SetText(GameObject pop)
    {
        TMP_Text textTitle = pop.transform.Find("textTitle").GetComponent<TMP_Text>();
        TMP_Text textContent = pop.transform.Find("textContent").GetComponent<TMP_Text>();
        textTitle.text = title;
        textContent.text = content;
    }
}
