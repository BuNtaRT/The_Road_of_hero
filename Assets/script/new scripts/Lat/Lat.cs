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
            NextGen = transform.position.x + steap_spawn + 1;
    }

    bool eventMap = false;
    void EventChange(bool val) 
    {
        eventMap = val;
        PauseCar(val);
    }

    #endregion

    ControllCar PlayerCar;

    private void Start()
    {
        int carIndex = 24;
        if (PlayerPrefs.GetInt("PremNow") == 0)
            carIndex = PlayerPrefs.GetInt("Car_index");
        PlayerCar = GameObject.FindGameObjectWithTag("Scripts").GetComponent<ControllCar>();
        singleMetods.Add(Spawn_monster);
        singleMetods.Add(Spawn_pit);
        singleMetods.Add(Spawn_bomb);
        foreach (Methods temp in singleMetods)
            multiMetods.Add(temp);
        multiMetods.Add(Spawn_orda);
        UI.singleton.onPaused += PauseCar;
        gameObject.GetComponent<CombinateBG>().OnStopLat += EventChange;       // если происходит событие то отключает спавнер
        Vault_data.singleton.CreateMontersList(Convert.ToInt32(GameObject.FindGameObjectWithTag("Background").name));   // запрашиваем запитсь монстров ответственных за текущую карту 
        steap_spawn = steap_spawn - carIndex >= 12 ? steap_spawn - carIndex : 12;
        chance = chance - carIndex >= 15 ? chance - carIndex : 15;
        BonusGen = gameObject.GetComponent<bonus_generate>();
        lineCar = PlayerCar.GetLine();
    }


    float NextGen = 30f;        // следующая генерация
    private void Update()
    {
        if (NextGen <= transform.position.x+steap_spawn) 
        {
            SpawnWhat();
        }
    }

    int chance = 70;
    float steap_spawn = 30;
    bonus_generate BonusGen;

    int lineCar;
    void SpawnWhat()                    // что спавним ??
    {
        if (!pause && !eventMap)
        {
            lineCar = PlayerCar.GetLine();
            roadBox = false;
            MultiTrue = false;
            chance = chance - 3 >= 10 ? chance - 3 : 10; // а так же шанс коректируем

            if (UnityEngine.Random.Range(0, 100) >= chance)     // тут мы можем забить хоть три линии от шанса зависит количество монстров
            {
                SpawnLineCount();
            }
            else                            // или же мы хоть что то заспавним
            {
                
                if (UnityEngine.Random.Range(0, 101) >= 50)         //если рандом то спавним не туда где магшина
                    lineCar = UnityEngine.Random.Range(0, 3);

                BonusGen.MaybeBonus(NextGen);
                if (CarShoot.singleton.ammo >= 1)
                {
                    Methods temp = GetSpanwMethodMulti();
                    temp(lineCar);
                }
                else
                {
                    Methods temp = GetSpanwMethodsingle();
                    temp(lineCar);
                }
            }

            if(steap_spawn >=12)
                steap_spawn-=0.1f;

            NextGen += UnityEngine.Random.Range(steap_spawn-2,steap_spawn+3);
        }
    }

    bool MultiTrue = false;         // bool который обозначает что есть обьект (противник) занимающий все полосы
    float plus = 0;
    int ExitPitBombAndTD = 0;       // линия куда не будут спавнится неуничтожаемые препятствия 
    void SpawnLineCount()                   // на скольки линиях надо спавнить и гворит на какой именно
    {

        do 
        {
            ExitPitBombAndTD = UnityEngine.Random.Range(0, 3);
        } while (ExitPitBombAndTD == lineCar);      // назначение линии куда не будут спанится яма и пит и тд

        int dontSpawnLine = -1;
        if (CarShoot.singleton.ammo == 0)   // если припасов 0 то 
        {
            dontSpawnLine = ExitPitBombAndTD;     // то выбираем линию куда спавнится ничего не будет
            BonusGen.generateLine("bonus/BonusR", dontSpawnLine,NextGen);
        }
        else 
        {
            BonusGen.MaybeBonus(NextGen);
        }

        for (int i = 0; i <= 2; i++)
        {
            if (i != dontSpawnLine)             // если на этой линии не надо спавнить - пропускаем 
            {
                plus = UnityEngine.Random.Range(0.4f, 3f);          // смещение что бы не спавнилось все в ряд 
                if (MultiTrue)                                      // если есть штука занимающая всю дорогу 
                    plus = UnityEngine.Random.Range(3, 7);          // то след спавн будет смещен

                if (UnityEngine.Random.Range(0, 101) >= chance || i == lineCar)     // упаваем на шанс или точно спавним если на этой линии авто игрока
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


        if (UnityEngine.Random.Range(0, 101) >= 50  && !MultiTrue)
        {
            Spawn_RoadBox(ExitPitBombAndTD);

            for (int i = 0; i <= 2; i++)
            {
                if (i != ExitPitBombAndTD) 
                {
                    Spawn_RoadBox(i);
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
    bool roadBox = false; // спавнили мы уже дорожный ящик ??
    void Spawn_RoadBox(int line_now) 
    {
        Color colorBox;
        string tagB = "";
        if (!roadBox)
        {
            colorBox = new Color(0.4603121f, 0.5f, 0.4504717f);
            roadBox = true;
            tagB = "BoxAccept";
        }
        else 
        {
            colorBox = new Color(0.3867925f, 0.3484781f, 0.3571604f);
            tagB = "BoxFail";

        }
        GameObject box;
        if (line_now == 0)
            box = generate("lat/road_box", NextGen + plus-6, -3.073f, 9);
        else if (line_now == 1)
            box = generate("lat/road_box", NextGen + plus-6, -3.724f, 11);
        else
            box = generate("lat/road_box", NextGen + plus-6, -4.429f, 13);
        box.GetComponent<SpriteRenderer>().color = colorBox;
        box.tag = tagB;
    }

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
            GameObject temp;
            int lay = 0;
            if (line_now == 0) {
                temp = generate("lat/Bomb", NextGen + plus, 0.19f, 0);
                lay = 10;
            }
            else if (line_now == 1)
            {
                temp = generate("lat/Bomb", NextGen + plus, -0.4f, 0);
                lay = 12;

            }
            else
            {
                temp = generate("lat/Bomb", NextGen + plus, -1.15f, 0);
                lay = 14;

            }
            temp.transform.Find("bomb").GetComponent<bomb_controller>().SetLay(lay);
        }
    }

    #endregion



    int GetRandomMonster()
    {
        return Vault_data.singleton.GetRandomMonster();
    }

    GameObject generate(string path, float posX, float posY, int layout)
    {
        return CoreGenerate.GenerateObj(path,posX+10,posY,layout);
    }


}
