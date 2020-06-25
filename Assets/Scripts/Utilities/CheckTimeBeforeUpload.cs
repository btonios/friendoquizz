using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTimeBeforeUpload
{
    System.DateTime expiryTime;
    void Start () 
    {
        if (!this.ReadTimestamp ("timer")) 
        {
            this.ScheduleTimer ();
        }
    }
    void Update () 
    {
        if (System.DateTime.Now > expiryTime) 
        {
            GlobalVariables.canUpload = true;
            this.ScheduleTimer ();
        }
    }

    void ScheduleTimer () 
    {
        expiryTime = System.DateTime.Now.AddHours(1.0);
        this.WriteTimestamp ("timer");
    }

    private bool ReadTimestamp (string key) 
    {
        long tmp = System.Convert.ToInt64 (PlayerPrefs.GetString(key, "0"));
        if (tmp == 0) 
        {
            return false;
        }
        expiryTime = System.DateTime.FromBinary(tmp);
        return true;
    }
    
    private void WriteTimestamp (string key) 
    {
        PlayerPrefs.SetString (key, expiryTime.ToBinary().ToString());
    }
}
