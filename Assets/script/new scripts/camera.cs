using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Transform cam;


    public Transform target;

    void Start()
    {
        fix_camera_from_screen();

        cam = gameObject.transform;
    }

    //меняем размер от соотношения
    void fix_camera_from_screen() {

    }



    private void Update()
    {
        //cam.position = Vector3.Lerp(cam.position, new Vector3(target.position.x , cam.position.y, -10), Time.deltaTime * camera_speed);
        cam.position = new Vector3(target.position.x , cam.position.y, -10);
    }
}
