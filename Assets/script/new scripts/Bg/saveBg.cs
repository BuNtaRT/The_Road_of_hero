using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveBg : MonoBehaviour
{
    void Start()
    {

        Transform temp =  gameObject.transform.Find("0");
        temp.SetParent(Camera.main.transform);
    }
}
