using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lat_controller : Generate
{

    // этот скрипт висит на камере и по прошествию растояния запрашивает генерацию чего либо, а так же решает что будет созданно

    float last_pit = 0;
    float last_enemy = 0;
    float last_bonus = 0;
    float last_bomb = 0;
    

    int chance_pit = 80; 
    int chance_enemy = 70;
    int chance_bomb = 80; 

    float min_pit_distance = 10;
    float min_enemy_distance = 15;

    car_event car_Event;

    public float hard = 1;
    float hard_correct = 1;

    //public void Up_hard() {
    //    hard = 
    //}

    void Start()
    {
        InvokeRepeating("What_now", 0f, 0.5f);
        car_Event = GameObject.Find("script").GetComponent<car_event>();
    }

    void What_now() {

        Hard_correct();


        int rand = Random.Range(0, 101);
        if (rand >= chance_enemy && transform.position.x+ 15 >= (min_enemy_distance + last_enemy))
        {
            if (last_pit + 4 <= transform.position.x+15 || car_Event.rocket_count >= 1)
            {
                enemy_generate();
            }
        }
        rand = Random.Range(0, 101);
        if (rand >= chance_pit && transform.position.x + 15 >= (min_pit_distance + last_pit) && last_bomb+4 <= transform.position.x+13)
        {
            pit_generate();
        }
        rand = Random.Range(0, 101);
        if (rand >= 0 && last_bonus + 20 <= transform.position.x)
        {
            bonus_generate();
        }
        rand = Random.Range(0, 101);
        if (
            last_pit + 4 <= transform.position.x +15
            && rand >= chance_bomb
            && transform.position.x >= 550f 
            && car_Event.rocket_count >= 1
            && last_bomb < transform.position.x + 25
            )
        {

            Bomb_generate();
        }

    }

    void Bomb_generate() {
        int rand = Random.Range(0, 101);
        if (rand >= 50)
        {

            generate("lat/Bomb", transform.position.x + 15, 0f);
        }
        else
        {
            generate("lat/Bomb", transform.position.x + 15, -1f);

        }
        last_bomb = transform.position.x + 15;
    }

    void Hard_correct() {
        if (hard_correct != hard) {
            chance_pit = (int)(100 / hard) + 20 >= 10 ? (int)(70 / hard) + 30 : chance_pit;
            chance_enemy = (int)(100 / hard) + 30 >= 10 ? (int)(80 / hard) + 20 : chance_enemy;
            min_pit_distance = 15 / hard >= 3.5f ? 10 / hard : min_pit_distance;
            min_enemy_distance = 15 / hard >= 3.5f ? 15 / hard : min_enemy_distance;
            chance_bomb = (int)(120 / hard) >= 10 ? (int)(140 / hard) : chance_bomb;
            hard_correct = hard;
            Debug.Log("chance_pit = " + chance_pit);
            Debug.Log("chance_enemy = " + chance_enemy);
            Debug.Log("min_pit_distance = " + min_pit_distance);
            Debug.Log("min_enemy_distance = " + min_enemy_distance);
            Debug.Log("chance_bomb = " + chance_bomb);
        }   
    }

    void bonus_generate()
    {

        float x = Mathf.Max(last_enemy, last_pit) + 4f;
        if (x <= transform.position.x + 11)
        {
            x += transform.position.x + 11 - x;
        }

        int rand = Random.Range(0, 101);
        if (rand >= 80)
        {
            rand = Random.Range(0, 101);
            if (rand >= 51)
            {
                generate("bonus/BonusС", x, -3.429f);
            }
            else
            {
                generate("bonus/BonusС", x, -4.557f);
            }
        }
        else
        {
            rand = Random.Range(0, 101);
            if (rand >= 51)
            {
                generate("bonus/BonusR", x, -3.429f);
            }
            else
            {
                generate("bonus/BonusR", x, -4.557f);
            }

        }
        last_bonus = transform.position.x;

    }

    void enemy_generate() {
        int rand = Random.Range(0, 101);
        GameObject enemy;
        if (rand >= 50)
        {
            enemy = generate("lat/enemy1", transform.position.x + 15, -2.22f);
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }
        else {
            enemy = generate("lat/enemy1", transform.position.x + 15, -3.13f);
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 15;

        }

        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(Set_skin_enemy());
        last_enemy = transform.position.x + 15;
    }

    string Set_skin_enemy() {

        int rand = Random.Range(1, 10);
        return "enemy_controll/enemy_controll" + rand;

    }

    void pit_generate() {
        int rand = Random.Range(0, 101);
        if (rand >= 50)
        {

            generate("lat/pit1", transform.position.x + 15, -3.635f);
        }
        else
        {
            generate("lat/pit1", transform.position.x + 15, -4.554f);

        }
        last_pit = transform.position.x + 15;
    }

}
