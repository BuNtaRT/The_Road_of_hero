using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_event : MonoBehaviour
{
    public Transform curret_car;

    public void Die_enemy(GameObject car) {
        gameObject.GetComponent<AudioCore>().Create_audio_eff("expl");

        GameObject.Find("script").GetComponent<effect_Core>().Create_effect("explosion_car", 0, 1.4f, car.transform);
        GameObject.Find("script").GetComponent<effect_Core>().Effect_die(car,"fix_fall");
        GameObject.Find("Car").GetComponent<Car_controller>().enabled = false;


    }

    public void Car_Shot(GameObject who) {

        StartCoroutine(car_shot(who));

    }

    IEnumerator car_shot(GameObject who) {


        gameObject.GetComponent<AudioCore>().Create_audio_eff("fire_load");
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<AudioCore>().Create_audio_eff("fire_start");

        GameObject temp = new GameObject();
        temp = Instantiate(Resources.Load<GameObject>("rocket"));
        temp.transform.position = new Vector3(curret_car.position.x, curret_car.position.y, curret_car.position.z);
        temp.GetComponent<rocket_controller>().StartRocket(temp.transform.position, who.transform.position);

    }


}
