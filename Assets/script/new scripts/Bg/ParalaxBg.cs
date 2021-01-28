using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBg : MonoBehaviour
{
    float legth, startpos;
    Transform cam;
    public float parallaxEffect; 

    void Start()
    {
        startpos = transform.position.x;
        legth = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
    }

    private void Update()
    {
        float temp = (cam.position.x * (1 - parallaxEffect));
        float dist = (cam.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + legth) startpos += legth;
        else if (temp < startpos - legth) startpos -= legth;
    }
}
