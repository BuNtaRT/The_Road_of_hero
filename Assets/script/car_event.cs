using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_event : MonoBehaviour
{
    public Transform curret_car;
    public bool _die = false;
    public int rocket_count = 2;

    public void Die_enemy(GameObject car) {
        Die(car);
    }

    void Die(GameObject car) {
        gameObject.GetComponent<AudioCore>().Create_audio_eff("expl");
        car.transform.Find("Exhaust").GetComponent<ParticleSystem>().Stop();
        GameObject.Find("script").GetComponent<effect_Core>().Create_effect("explosion_car", 0, 1.4f, car.transform);
        GameObject.Find("script").GetComponent<effect_Core>().Effect_die(car, "fix_fall");
        GameObject.Find("Car").GetComponent<Car_controller>().enabled = false;
        Die_flag();
    }

    public void Die_bomb(GameObject car) {
        Die(car);

    }

    public void Die_pit() {
        Die_flag();

    }

    public int Bonus(float hard) {
        int coin = (int)Random.Range(2 * (hard + 1), 8 * (hard + 1));
        GameObject.Find("script").GetComponent<money_sc>().Get_coin(coin);
        return coin;
    }

    void Die_flag() {
        _die = true;
        
        GameObject.Find("script").GetComponent<controll>()._die = true;
        GameObject.Find("script").GetComponent<UI_game>().Die();

    }

    public void Car_Shot() {

        if (rocket_count > 0)
        {
            rocket_count--;
            GameObject.Find("script").GetComponent<UI_game>().Rocket(rocket_count);
            StartCoroutine(car_shot());
        }
    }

    IEnumerator car_shot() {


        gameObject.GetComponent<AudioCore>().Create_audio_eff("fire_load");
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<AudioCore>().Create_audio_eff("fire_start");
            
        GameObject temp = new GameObject();
        temp = Instantiate(Resources.Load<GameObject>("rocket"));
        temp.transform.position = new Vector3(curret_car.position.x, curret_car.position.y, curret_car.position.z);
        temp.GetComponent<rocket_controller>().StartRocket();

    }


}
