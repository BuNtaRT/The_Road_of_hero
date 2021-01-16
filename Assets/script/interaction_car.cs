using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction_car : MonoBehaviour
{
    public RuntimeAnimatorController contr;

    public float hard = 1;

    private void Start()
    {
        GameObject.Find("script").GetComponent<car_event>().curret_car = transform;
        //GameObject.Find("script").GetComponent<controll>().curret_car = transform;
        //Time.timeScale = 0.001f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "pit")
        {
            transform.Find("Exhaust").GetComponent<ParticleSystem>().Stop();


            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Car_controller>().enabled = false;
            gameObject.transform.SetParent(collision.transform, true);
            gameObject.GetComponent<Animator>().runtimeAnimatorController = contr;
            int rand = Random.Range(1, 5);
            if (rand != 1) {
                gameObject.GetComponent<Animator>().Play("crash" + rand);
            }
            gameObject.GetComponent<Animator>().speed = hard;
            gameObject.GetComponent<Animator>().enabled = true;
            GameObject.Find("script").GetComponent<car_event>().Die_pit();



        }

        else if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "orda")
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("script").GetComponent<car_event>().Die_enemy(gameObject);

        }
        else if (collision.gameObject.tag == "bomb_zone") {
            Destroy(collision.gameObject.transform.parent.gameObject);
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("script").GetComponent<car_event>().Die_bomb(gameObject);

        }

        else if (collision.gameObject.tag == "bonus") {


            string sound = "";
            int count = 1;

            collision.GetComponent<Animator>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (collision.gameObject.name.Contains("BonusR"))
            {
                GameObject.Find("script").GetComponent<car_event>().rocket_count++;
                GameObject.Find("script").GetComponent<UI_game>().Rocket(GameObject.Find("script").GetComponent<car_event>().rocket_count);
                sound = "bonus";
            }
            else {
                sound = "coin" + Random.Range(0, 5);
                count = GameObject.Find("script").GetComponent<car_event>().Bonus(hard);
            }

            GameObject.Find("script").GetComponent<effect_Core>().Effect_bonus(collision.gameObject.transform.GetChild(0).gameObject, sound, count);
        }

    }



}
