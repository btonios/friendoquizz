using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
 
    public void DeleteFiles()
    {
        SaveData.DeleteSaves();
    }
}
