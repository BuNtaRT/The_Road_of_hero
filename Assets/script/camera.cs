using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Transform cam;

    float camera_speed = 15f;
    public Transform target;

    void Start()
    {
        fix_camera_from_screen();

        cam = gameObject.transform;
    }

    //меняем размер от соотношения
    void fix_camera_from_screen() {
        Debug.Log(Camera.main.aspect);
    }



    private void FixedUpdate()
    {
        cam.position = Vector3.Lerp(cam.position, new Vector3(target.position.x , target.position.y, -10), Time.deltaTime * camera_speed);
    }
}
