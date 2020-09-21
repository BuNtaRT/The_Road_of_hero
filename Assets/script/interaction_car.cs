using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction_car : MonoBehaviour
{
    public RuntimeAnimatorController contr;


    private void Start()
    {
        GameObject.Find("script").GetComponent<car_event>().curret_car = transform;
        //Time.timeScale = 0.001f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "pit")
        {
            //gameObject.GetComponent<Animator>().enabled = false;


            GameObject.FindGameObjectWithTag("Player").GetComponent<Car_controller>().enabled = false;
            //gameObject.transform.localPosition = new Vector3(-1.998821f, 0.3867615f, 0);
            gameObject.transform.SetParent(collision.transform, true);
            //gameObject.GetComponent<Animator>().SetBool("crash", true);
            gameObject.GetComponent<Animator>().runtimeAnimatorController = contr;
            //gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;

            //StartCoroutine(Fix_it());

        }

        else if (collision.gameObject.tag == "enemy") {
            GameObject.Find("script").GetComponent<car_event>().Die_enemy(gameObject);

        }

    }



}
