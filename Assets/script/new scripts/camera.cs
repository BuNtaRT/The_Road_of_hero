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


    private void FixedUpdate()
    {
        //cam.position = Vector3.Lerp(cam.position, new Vector3(target.position.x , cam.position.y, -10), Time.deltaTime * camera_speed);
        //Vector3 to = new Vector3(target.position.x,0f,-10f);
        //transform.position = Vector3.Lerp(transform.position, to, Time.deltaTime * 10);

        cam.position = new Vector3(target.position.x , cam.position.y, -10);
    }
}
