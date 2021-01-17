using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{

    void Start()
    {
        UI.singleton.onPaused += PauseCar;       // singleton - крутая штука что бы не искать обьекты а находить единственный Static-же и обращаться к нему
        GameObject.Find("Scripts").GetComponent<Hard_controller>().OnUpSpead += Speed;
    }


     
    #region Pause
    private bool pause = true;
    void PauseCar(bool value) {
        pause = value;
    }
    #endregion

    #region Car speed
    private float car_speed = 3f;
    private void Speed(float val)
    {
        car_speed = val;
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
