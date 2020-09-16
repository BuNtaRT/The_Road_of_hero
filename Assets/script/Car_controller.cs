using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_controller : MonoBehaviour
{
    private float car_speed = 3f;

    public void Up_speed_car() {
        car_speed += 2f;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), Time.deltaTime * car_speed);
    }


}
