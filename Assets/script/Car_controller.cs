using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car_controller : MonoBehaviour
{
    private float car_speed = 0.1f;
    public Text Score;

    public float hard = 1;

    public bool pause = false; 

    //public void Up_speed_car() {

    //    if (car_speed >= 12)
    //    {
    //        car_speed += 0.1f;
    //    }
    //    else if (car_speed >= 8)
    //    {
    //        car_speed += 0.2f;
    //    }
    //    else {
    //        car_speed += 0.5f;
    //    }
    //}

    private void FixedUpdate()
    {
        if (!pause)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), Time.deltaTime * (car_speed * hard));
            transform.position = new Vector3(transform.position.x + (car_speed + (hard - 1) * 0.25f), transform.position.y);
            Score.text = ((int)transform.position.x).ToString() + "m";
        }
    }


}
