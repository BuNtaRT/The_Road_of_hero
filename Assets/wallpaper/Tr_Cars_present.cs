﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tr_Cars_present : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y,transform.position.z);
    }
}
