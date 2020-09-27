using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hadr_controller : MonoBehaviour
{
    int skill_auto = 0;

    float next_hard = 30;
    float tic_hard = 40;

    public float mass_plit = 1;

    interaction_car car;


    float hard = 1;

    private void Start()
    {
        car =  gameObject.transform.GetChild(0).GetComponent<interaction_car>();
    }

    private void Update()
    {
        if (transform.position.x >= next_hard) {
            Hard_plus();
        }
    }

    void Hard_plus() {

        hard += 0.08f;
        tic_hard = tic_hard * hard + 8;
        next_hard += tic_hard;


        Debug.Log("hard = " + hard);

        gameObject.GetComponent<Car_controller>().hard = hard;      // скорость
        Camera.main.GetComponent<lat_controller>().hard = hard;     // препятствия 
        car.hard = hard;
        GameObject.Find("script").GetComponent<controll>().hard = hard;


        Debug.Log("Hard++");
    }
}
