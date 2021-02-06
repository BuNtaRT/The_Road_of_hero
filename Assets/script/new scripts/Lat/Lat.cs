using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lat : MonoBehaviour
{
    #region pause
    bool pause = false;
    void PauseCar(bool pause) {
        this.pause = pause;
    }
    #endregion

    void Start()
    {
        //Debug.Log("Start Lat (" + GameObject.Find("Car").transform.GetChild(0).GetComponent<Car>().lvl + ")-car (" + PlayerPrefs.GetFloat("Cur_map_lvl") + ")-map");

        UI.singleton.onPaused += PauseCar;
        gameObject.GetComponent<CombinateBG>().OnStopLat += PauseCar;
        if (PlayerPrefs.GetInt("Car_index") <= 3)
        {
            switch (PlayerPrefs.GetInt("Car_index"))
            {
                case 0:
                    InvokeTime = 6;
                    break;
                case 1:
                    InvokeTime = 7f;
                    break;
                case 2:
                    InvokeTime = 8f;
                    break;
                case 3:
                    InvokeTime = 9f;
                    break;
            }
        }
        else
        {
            InvokeTime = 12f - ((GameObject.Find("Car").transform.GetChild(0).GetComponent<Car>().lvl + PlayerPrefs.GetFloat("Cur_map_lvl")) / 5.5f);
        }
        Vault_data.singleton.CreateMontersList(Convert.ToInt32(GameObject.FindGameObjectWithTag("Background").name));   // запрашиваем запитсь монстров ответственных за текущую карту 
        Invoke("SpawnWhat", 5);
    }

    float InvokeTime = 10f;  // max 10 min 2
    int chance = 70;
    int Line2_content = 0;     // зжанятость верхней линии 
    bool one_prop = false;           // нельзя две бомбы и два пита


    //void SpawnWhat()                    // что спавним ??
    //{
    //    if (!pause)
    //    {

    //        Line2_content = 0;          // обнуляем линии
    //        one_prop = false;                // обнуляем яму

    //        InvokeTime = InvokeTime - 0.3f >= 1.5f ? InvokeTime - 0.3f : 1.5f;      // уменьшаем частоту вызова 
    //        chance = chance - 3 >= 10 ? chance - 3 : 10;                      // а так же шанс 

    //        Debug.Log("spawn time =" + InvokeTime + " chace = " + chance);

    //        if (UnityEngine.Random.Range(0, 100) >= chance || TryCount)                     // если повезло заспавнить 
    //        {
    //            Invoke(GetSpanwMethodsingle(), 0);

    //            TryCount = false;

    //            if (CarShoot.singleton.ammo >= 1)
    //            {
    //                if (UnityEngine.Random.Range(0, 200) >= chance / 4)
    //                {
    //                    Invoke(GetSpanwMethodMulti(), 0);
    //                }
    //            }
    //        }
    //        else if (InvokeTime >= 6)                                       // или же наша частоты вызовов еще не разогналась
    //        {
    //            TryCount = false;
    //            Spawn_pit();

    //            if (UnityEngine.Random.Range(0, 100) >= 50)             // и возможно спавним монстра
    //            {
    //                if (CarShoot.singleton.ammo >= 1)
    //                {
    //                    if (UnityEngine.Random.Range(0, 200) >= chance / 4)
    //                    {
    //                        Spawn_monster();
    //                    }
    //                }
    //            }

    //            //Spawn_monster();
    //        }
    //        else
    //        {
    //            TryCount = true;
    //        }
    //    }
    //    Invoke("SpawnWhat", InvokeTime);                //invoke repead не поддерживает динамическое значение (((
    //}

    void SpawnWhat()                    // что спавним ??
    {
        if (!pause)
        {

            Line2_content = 0;          // обнуляем линии
            one_prop = false;                // обнуляем яму

            InvokeTime = InvokeTime - 0.4f >= 1.3f ? InvokeTime - 0.4f : 1.3f;      // уменьшаем частоту вызова 
            chance = chance - 3 >= 10 ? chance - 3 : 10;                      // а так же шанс 

            Debug.Log("spawn time =" + InvokeTime + " chace = " + chance);

            if (InvokeTime >= 6)                                       // или же наша частоты вызовов еще не разогналась
            {
                Spawn_pit();

                if (UnityEngine.Random.Range(0, 100) >= 50)             // и возможно спавним монстра
                {
                    if (CarShoot.singleton.ammo >= 1)
                    {
                        if (UnityEngine.Random.Range(0, 200) >= chance / 4)
                        {
                            Spawn_monster();
                        }
                    }
                }
            }
            else
            {
                Invoke(GetSpanwMethodsingle(), 0);

                if (CarShoot.singleton.ammo >= 1)
                {
                    if (UnityEngine.Random.Range(0, 200) >= chance / 4)
                    {
                        Invoke(GetSpanwMethodMulti(), 0);
                    }
                }
            }
        }
        if (InvokeTime <= 1.31f) 
            Invoke("SpawnWhat", UnityEngine.Random.Range(1.1f,1.4f));
        else
            Invoke("SpawnWhat", InvokeTime);
    }

    #region spawn method
    string[] ListMethodMulti = { "Spawn_monster", "Spawn_pit", "Spawn_orda", "Spawn_bomb" };        // лист методов для второй линии 

    string[] ListMethodSingle = { "Spawn_monster", "Spawn_pit", "Spawn_bomb"};         // лист методов если занята одна линия 

    string GetSpanwMethod(string[] tempMass) 
    {
        return tempMass[UnityEngine.Random.Range(0, tempMass.Length)];
    }

    string GetSpanwMethodsingle()
    {
        return GetSpanwMethod(ListMethodSingle);
    }

    string GetSpanwMethodMulti()
    {
        return GetSpanwMethod(ListMethodMulti);
    }

    #endregion



    #region Spawn
    void Spawn_pit()
    {
        int plus = 0;
        if (one_prop)
        {
            plus = 8;
        }
        one_prop = true;
        if ((UnityEngine.Random.Range(0, 100) >= 50 && Line2_content != 2) || Line2_content == 1)       // если выпал шанс и линия 2 не занята или же если линия 1 занята (спавнить куда то же надо)
        {
            Line2_content = 2;
            generate("lat/pit1", transform.position.x + 15+plus, -3.635f,0);
        }
        else
        {
            Line2_content = 1;
            generate("lat/pit1", transform.position.x + 15+plus, -4.554f,0);
        }
        

    }

    void Spawn_monster()
    {
        GameObject enemy;
        if ((UnityEngine.Random.Range(0, 100) >= 50 && Line2_content != 2) || Line2_content == 1)
        {
            enemy = generate("lat/enemy1", transform.position.x + 17, -2.044f,11);
            Line2_content = 2;
        }
        else
        {
            Line2_content = 1;
            enemy = generate("lat/enemy1", transform.position.x + 17, -3.13f,15);
        }
        
        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("enemy_controll/enemy_controll" + GetRandomMonster());

    }

    void Spawn_orda() {
        generate("lat/orda", transform.position.x + 20, -2.9f,0);
    }

    void Spawn_bomb()
    {
        int plus = 0;
        if (one_prop) {
            plus = 8;
        }
        one_prop = true;
        if ((UnityEngine.Random.Range(0, 101) >= 50 && Line2_content != 2) || Line2_content == 1)
        {
            Line2_content = 2;
            generate("lat/Bomb", transform.position.x + 15+plus, 0f,0);
        }
        else
        {
            Line2_content = 1;
            generate("lat/Bomb", transform.position.x + 15+plus, -1f,0);

        }
    }

    #endregion




    int GetRandomMonster() 
    {
        return Vault_data.singleton.GetRandomMonster();
    }



    GameObject generate(string path, float posX, float posY, int layout)
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>(path));
        temp.transform.position = new Vector3(posX, posY, 0);
        if(layout != 0)
            temp.GetComponent<SpriteRenderer>().sortingOrder = layout;      // нужно что бы поставить например монстра вверх или вниз 
        return temp;

    }
}
