using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public int num_car;

    public event Action OnDie;

    public float lvl;       // коэффицент для умножения счета со стороны машины 



    [System.Obsolete]
    void Start()
    {
        //цвет выхлопа
        transform.GetChild(0).GetComponent<ParticleSystem>().startColor = GameObject.Find("Scripts").GetComponent<Colors>().color_exthose_car[num_car];
        //цвет машины от карты
    }

    void Die() {
        OnDie?.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }


}
