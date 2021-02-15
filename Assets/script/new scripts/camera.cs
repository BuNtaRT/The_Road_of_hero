using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Transform cam;


    public Transform target;

    void Start()
    {

        cam = gameObject.transform;
    }



    private void FixedUpdate()
    {
        cam.position = new Vector3(target.position.x , cam.position.y, -10);
    }
}
