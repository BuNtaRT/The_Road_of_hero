using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Тут описывается автомобиль, его номер, множитель (выше > больше очков)

    public int num_car;

    public event Action OnDie;

    public float lvl;       // коэффицент для умножения счета со стороны машины 



    [Obsolete]
    void Start()
    {
        Reload_animation();

        //цвет машины от карты
    }

    public void Start_play()
    {
        foreach (Transform chil in transform)
        {
            if (chil.name.Contains("Exhaust"))
                chil.GetComponent<ParticleSystem>().Play();
        }
    }

    [System.Obsolete]
    public void Reload_animation() 
    {
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("cars/Player_car" + num_car);
        //цвет выхлопа
        foreach (Transform chil in transform)
        {
            if (chil.name.Contains("Exhaust"))
                chil.GetComponent<ParticleSystem>().startColor = GameObject.Find("Scripts").GetComponent<Colors>().color_exthose_car[num_car];
        }
    }


    void Die() {
        OnDie?.Invoke();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnterCAR  = "+ other.gameObject.tag);
        if (other.gameObject.tag == "enemy" || other.gameObject.tag == "pit" || other.gameObject.tag == "bomb_zone")
        {
            Die();
        }
    }


}
