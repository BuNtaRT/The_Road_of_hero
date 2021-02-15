using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMove : MonoBehaviour
{


    private float invokeTime = 1f;

    // Тут происходит движение автомобиля вперед, а так же его ускорение

    void Start()
    {
        Debug.Log("Index = " + PlayerPrefs.GetInt("Car_index"));

        UI.singleton.onPaused += PauseCar;       // singleton - крутая штука что бы не искать обьекты а находить единственный Static-же и обращаться к нему
        if (PlayerPrefs.GetInt("Car_index") > 2)
        {
            car_speed = 0.45f + (PlayerPrefs.GetFloat("Cur_car_lvl")+ PlayerPrefs.GetFloat("Cur_map_lvl")) / 70;
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
        if (PlayerPrefs.GetInt("Car_index") <= 2)
        {
            invokeTime = 7f;
        }
        else 
        {
            invokeTime = 90 / car_lvl >= 2 ? 90 / car_lvl : 2;
        }
        Debug.Log("Start speed = " + car_speed);
        InvokeRepeating("UpSpeed", 5, invokeTime);
        car_speed = 0.6f;
        Invoke("SuperSpeed", 360);
    }

    void UpSpeed()
    {
        if (!pause)
        {
            car_speed = car_speed + 0.08f <= 1.6f ? car_speed + 0.08f : 1.6f;
            GameObject.Find("In_game_ui/Speedometr").GetComponent<Text>().text = car_speed.ToString();      // Убрать

            invokeTime = invokeTime - 0.5f >= 1.5f ? invokeTime - 0.33f : 1.5f;

            if (car_speed >= 1.65f)
                CancelInvoke("UpSpeed");
        }
        
    }

    void SuperSpeed() {
        if (car_speed >= 1.59f && transform.position.x >= 8000)
        {
            car_speed += 0.25f;
        }
        else
            Invoke("SuperSpeed", 50);

    }
    #endregion

    void FixedUpdate()
    {
        if (!pause)
        {
            float to = transform.position.x + car_speed * 10;
            transform.position = Vector3.Lerp(transform.position, new Vector3(to, 0, 0), Time.deltaTime);

            //transform.position = new Vector3(transform.position.x + (car_speed * 0.0025f), transform.position.y);
        }
    }
}
