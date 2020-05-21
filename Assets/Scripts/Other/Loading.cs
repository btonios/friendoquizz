using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, -500*Time.deltaTime);
    }
}
