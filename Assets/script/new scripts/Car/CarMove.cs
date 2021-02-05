using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMove : MonoBehaviour
{

    float speed = 0;

    private float invokeTime = 1f;

    // Тут происходит движение автомобиля вперед, а так же его ускорение

    void Start()
    {
        UI.singleton.onPaused += PauseCar;       // singleton - крутая штука что бы не искать обьекты а находить единственный Static-же и обращаться к нему
        if (PlayerPrefs.GetFloat("Car_index") >= 2)
        {
            car_speed = PlayerPrefs.GetInt("Cur_car_lvl") / 22;
        }
        else
        {
            switch (PlayerPrefs.GetInt("Car_index"))
            {
                case 0:
                    car_speed = 0.9f;
                    break;
                case 1:
                    car_speed = 0.8f;
                    break;
                case 2:
                    car_speed = 0.7f;
                    break;
            }
        }
        Speed();
        //GameObject.Find("Scripts").GetComponent<Hard_controller>().OnUpSpead += Speed;
    }


    #region Pause
    private bool pause = false;
    void PauseCar(bool value) 
    {
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
        Invoke("SuperSpeed", 360);
    }

    void UpSpeed()
    {
        if (!pause)
        {
            car_speed = car_speed + 0.08f <= 1.7f ? car_speed + 0.08f : 1.7f;
            GameObject.Find("In_game_ui/Speedometr").GetComponent<Text>().text = car_speed.ToString();
            //Debug.Log("speed = " + car_speed);
            if (car_speed >= 1.65f)
                CancelInvoke("UpSpeed");
        }
        
    }

    void SuperSpeed() {
        if (car_speed >= 1.69f && transform.position.x >= 8000)
        {
            car_speed += 0.3f;
        }
        else
            Invoke("SuperSpeed", 50);

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
