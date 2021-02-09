using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lat : MonoBehaviour
{

    #region past

    /*
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
        //if (PlayerPrefs.GetInt("Car_index") <= 3)
        //{
        //    switch (PlayerPrefs.GetInt("Car_index"))
        //    {
        //        case 0:
        //            InvokeTime = 6;
        //            break;
        //        case 1:
        //            InvokeTime = 7f;
        //            break;
        //        case 2:
        //            InvokeTime = 8f;
        //            break;
        //        case 3:
        //            InvokeTime = 9f;
        //            break;
        //    }
        //}
        //else
        //{
        //    InvokeTime = 12f - ((GameObject.Find("Car").transform.GetChild(0).GetComponent<Car>().lvl + PlayerPrefs.GetFloat("Cur_map_lvl")) / 5.5f);
        //}
        Vault_data.singleton.CreateMontersList(Convert.ToInt32(GameObject.FindGameObjectWithTag("Background").name));   // запрашиваем запитсь монстров ответственных за текущую карту 
        //Invoke("SpawnWhat", 5);
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

    private void FixedUpdate()
    {
        
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
            plus = 10;
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
    */
    #endregion

    #region pause
    bool pause = false;
    void PauseCar(bool pause)
    {
        this.pause = pause;
        if (pause == false)
            NextGen = transform.position.x + steap_spawn - 1;
    }
    #endregion
    //string[] ListMethodMulti = { "Spawn_monster", "Spawn_pit", "Spawn_orda", "Spawn_bomb" };        // лист где есть сущесва которые занимают все 

    //string[] ListMethodSingle = { "Spawn_monster", "Spawn_pit", "Spawn_bomb" };         // лист где существа занимают 1 линию 
    private void Start()
    {
        singleMetods.Add(Spawn_monster);
        singleMetods.Add(Spawn_pit);
        singleMetods.Add(Spawn_bomb);
        foreach (Methods temp in singleMetods)
            multiMetods.Add(temp);
        multiMetods.Add(Spawn_orda);
        UI.singleton.onPaused += PauseCar;
        gameObject.GetComponent<CombinateBG>().OnStopLat += PauseCar;       // если происходит событие то отключает спавнер
        Vault_data.singleton.CreateMontersList(Convert.ToInt32(GameObject.FindGameObjectWithTag("Background").name));   // запрашиваем запитсь монстров ответственных за текущую карту 
        steap_spawn = steap_spawn - PlayerPrefs.GetInt("Car_index") >= 9 ? steap_spawn - PlayerPrefs.GetInt("Car_index") : 9;
        chance = chance - PlayerPrefs.GetInt("Car_index") >= 15 ? chance - PlayerPrefs.GetInt("Car_index") : 15;
    }


    float NextGen = 30f;        // следующая генерация
    private void FixedUpdate()
    {
        if (NextGen <= transform.position.x+steap_spawn) 
        {
            SpawnWhat();
        }
    }

    int chance = 70;
    float steap_spawn = 30;

    void SpawnWhat()                    // что спавним ??
    {
        if (!pause)
        {
            MultiTrue = false;
            chance = chance - 3 >= 10 ? chance - 3 : 10; // а так же шанс коректируем

            if (UnityEngine.Random.Range(0, 100) >= chance)     // тут мы можем забить хоть три линии от шанса зависит количество монстров
            {
                SpawnLineCount();
            }
            else                            // или же мы хоть что то заспавним
            {

                if (CarShoot.singleton.ammo >= 1)
                {
                    Methods temp = GetSpanwMethodMulti();
                    temp(UnityEngine.Random.Range(0, 3));
                }
                else
                {
                    Methods temp = GetSpanwMethodsingle();
                    temp(UnityEngine.Random.Range(0, 3));
                }
            }

            if(steap_spawn >=12)
                steap_spawn-=0.5f;

            NextGen += UnityEngine.Random.Range(steap_spawn-2,steap_spawn+3);

        }
    }

    bool MultiTrue = false;         // bool который обозначает что есть обьект (противник) занимающий все полосы
    float plus = 0;
    int ExitPitBombAndTD = 0;       // линия куда не будут спавнится неуничтожаемые препятствия 
    void SpawnLineCount()                   // на скольки линиях надо спавнить и гворит на какой именно
    {
        ExitPitBombAndTD = UnityEngine.Random.Range(0, 3);      // назначение линии куда не будут спанится яма и пит и тд
        int dontSpawnLine = -1;
        if (CarShoot.singleton.ammo == 0)   // если припасов 0 то 
        {
            dontSpawnLine = UnityEngine.Random.Range(0, 3);     // то выбираем линию куда спавнится ничего не будет
        }

        for (int i = 0; i <= 2; i++)
        {
            if (i != dontSpawnLine)             // если на этой линии не надо спавнить - пропускаем 
            {
                plus = UnityEngine.Random.Range(0.4f, 3f);          // смещение что бы не спавнилось все в ряд 
                if (MultiTrue)                                      // если есть штука занимающая всю дорогу 
                    plus = UnityEngine.Random.Range(3, 7);          // то след спавн будет смещен

                if (UnityEngine.Random.Range(0, 101) >= chance)
                {
                    if (CarShoot.singleton.ammo >= 1 && MultiTrue == false)
                    {
                        Methods singleTemp = GetSpanwMethodMulti();
                        singleTemp(i);
                    }
                    else
                    {
                        Methods singleTemp = GetSpanwMethodsingle();
                        singleTemp(i);
                    }
                }
            }
        }
    }


    #region spawn method

    delegate void Methods(int line);

    List<Methods> singleMetods = new List<Methods>();// лист где есть сущесва которые занимают все 
    List<Methods> multiMetods = new List<Methods>();// лист где существа занимают 1 линию 
    Methods GetSpanwMethodsingle()
    {
        return singleMetods[UnityEngine.Random.Range(0,singleMetods.Count)];
    }

    Methods GetSpanwMethodMulti()
    {
        return multiMetods[UnityEngine.Random.Range(0, multiMetods.Count)];
    }
    #endregion


    #region Spawn
    void Spawn_pit(int line_now)
    {
        if (ExitPitBombAndTD != line_now)       // если щес линия куда нельзя спавнить такие штуки
        {
            if (line_now == 0)
                generate("lat/pit1", NextGen + plus, -3.52f, 0);
            else if (line_now == 1)
                generate("lat/pit1", NextGen + plus, -4.11f, 0);
            else
                generate("lat/pit1", NextGen + plus, -4.77f, 0);
        }
    }

    void Spawn_monster( int line_now)
    {
        GameObject enemy;
        if (line_now == 0)
            enemy = generate("lat/enemy1", NextGen +plus, -1.91f, 10);
        else if (line_now == 1)
            enemy = generate("lat/enemy1", NextGen + plus, -2.553f, 12);
        else
            enemy = generate("lat/enemy1", NextGen + plus, -3.249f, 14);

        enemy.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("enemy_controll/enemy_controll" + GetRandomMonster());

    }

    void Spawn_orda(int line_now)
    {
        MultiTrue = true;
        generate("lat/orda", NextGen-2, -2.9f, 0);
    }

    void Spawn_bomb(int line_now)
    {
        if (ExitPitBombAndTD != line_now)
        {
            if (line_now == 0)
                generate("lat/Bomb", NextGen + plus, 0.19f, 0);
            else if (line_now == 1)
                generate("lat/Bomb", NextGen + plus, -0.4f, 0);
            else
                generate("lat/Bomb", NextGen + plus, -1.15f, 0);
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
        temp.transform.position = new Vector3(posX+10, posY, 0);
        if (layout != 0)
            temp.GetComponent<SpriteRenderer>().sortingOrder = layout;      // нужно что бы поставить например монстра вверх или вниз 
        return temp;

    }


}
