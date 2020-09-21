using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dell_obj : MonoBehaviour
{
    Transform camera;
    float camera_size;
    void Start()
    {
        camera = Camera.main.transform;
        camera_size = Camera.main.orthographicSize;
        InvokeRepeating("Im_outside_camera_checker", 10f, 2f);
    }

    void Im_outside_camera_checker() {
        if (transform.position.x <= camera.position.x - (camera_size * 4) || transform.position.y <= camera.position.y - (camera_size * 4)) Destroy(gameObject);
    }
}
