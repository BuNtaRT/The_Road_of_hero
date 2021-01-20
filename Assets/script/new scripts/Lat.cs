using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lat : MonoBehaviour
{
    #region pause
    bool pause = true;
    void PauseCar(bool pause) {
        this.pause = pause;
    }
    #endregion

    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player").transform;
        UI.singleton.onPaused += PauseCar;
        StartupFirst.singleton.CreateMontersList(new List<int> { Convert.ToInt32(GameObject.FindGameObjectWithTag("Background").name) });   // запрашиваем запитсь монстров ответственных за текущую карту 
        InvokeRepeating("SpawnWhat", 5, InvokeTime);
    }

    float InvokeTime = 1f;
    int chance = 0;
    Transform car;
    void SpawnWhat() {
        if (!pause) {
            //InvokeTime = 


            //Spawn_monster();
        }
    }


    void Spawn_monster()
    {
        int rand = UnityEngine.Random.Range(0, 101);
        GameObject enemy;
        if (rand >= 50)
        {
            enemy = generate("lat/enemy1", transform.position.x + 25, -2.044f);
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 11;
        }
        else
        {
            enemy = generate("lat/enemy1", transform.position.x + 25, -3.13f);
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 15;

        }

        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("enemy_controll/enemy_controll" + GetRandomMonster());
        //last_enemy = transform.position.x + 15;

    }

    int GetRandomMonster() 
    {
        return StartupFirst.singleton.monsters[UnityEngine.Random.Range(0, StartupFirst.singleton.monsters.Count)];
    }



    GameObject generate(string path, float posX, float posY)
    {

        GameObject temp = Instantiate<GameObject>(Resources.Load<GameObject>(path));
        temp.transform.position = new Vector3(posX, posY, 0);
        return temp;
    }
}
