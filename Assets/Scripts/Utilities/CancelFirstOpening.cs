using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelFirstOpening : MonoBehaviour
{
    public void Cancel()
    {
        GlobalVariables.firstOpening = false;
    }
}
