using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lat : MonoBehaviour
{
    #region pause
    bool pause = false;
    void PauseCar(bool pause)
    {
        this.pause = pause;
        if (pause == false)
            NextGen = transform.position.x + steap_spawn - 1;
    }
    #endregion
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
