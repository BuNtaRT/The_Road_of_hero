using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{

    float speed = 0;

    private float invokeTime = 1f;

    // Тут происходит движение автомобиля вперед, а так же его ускорение

    void Start()
    {
        UI.singleton.onPaused += PauseCar;       // singleton - крутая штука что бы не искать обьекты а находить единственный Static-же и обращаться к нему
        Speed();
        //GameObject.Find("Scripts").GetComponent<Hard_controller>().OnUpSpead += Speed;
    }



    #region Pause
    private bool pause = true;
    void PauseCar(bool value) {
        pause = value;
    }
    #endregion

    #region Car speed
    private float car_speed = 0.2f;
    private float car_lvl = 1f;
    
    private void Speed()
    {
        car_lvl = transform.GetChild(0).GetComponent<Car>().lvl + PlayerPrefs.GetFloat("Cur_map_lvl");
        invokeTime = 18 / car_lvl >= 8 ? 18 / car_lvl : 8;
        InvokeRepeating("UpSpeed", 5, invokeTime);
    }

    void UpSpeed()
    {
        if (!pause)
        {
            car_speed = car_speed + 0.12f <= 1.7f ? car_speed + 0.12f : 1.7f;
            //Debug.Log("speed = " + car_speed);
            if (car_speed >= 1.7f)
                CancelInvoke("UpSpeed");
        }
    }
    #endregion

    void FixedUpdate()
    {
        if (!pause)
        {
            transform.position = new Vector3(transform.position.x + (car_speed * 0.25f), transform.position.y);
            //Score.text = ((int)transform.position.x).ToString() + "m";
        }
    }
}
