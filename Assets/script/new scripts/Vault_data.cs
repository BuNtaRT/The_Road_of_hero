﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Vault_data : MonoBehaviour
{


    public static Vault_data singleton { get; private set; }


    #region PacBonus
    public delegate void pacBonus(bool val);
    public event pacBonus Onpac;
    bool pac = false;
    void OnpacChange(bool val)
    {
        Debug.Log("changePac " + val);
        pac = val;
        Onpac?.Invoke(val);
    }

    public void PacOnSet(bool val) 
    {
        OnpacChange(val);
    }
    public bool StartGetPac()
    {
        return pac;
    }
    #endregion

    void Awake()
    {
        singleton = this;
        Application.targetFrameRate = 60;
        //Initialized_Weapon();

    }


    #region Weapon 

    List<int> Weapon = new List<int>() 
    {
        0,
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10,
        11,
        12,
        13,
        14,
        15,
        16,
        17,
        18,
        //19
        19,
        20,
        21,
        22,
        23,
        24

    };     // открытое орудие 
    List<float> TimeWeapon = new List<float>() 
    {
       0f,
       2f,
       2f,
       0,
       0,
       2.8f,
       0,
       0,
       0,
       2.4f,
       0,
       2.9f,
       2.9f,
       0f,
       2.9f,
       2f,
       3f,
       2.5f,
       2.9f,
       //19
       0f,
       2.5f,
       3.3f,
       3f,
       0,
       2f
    };     // его тайминг cooldown


    //int TrealerI = -1;   // для трейлер 
    public Tuple<int,float> GetRandomGunFromList() 
    {
        //int rand = UnityEngine.Random.Range(0, PlayerPrefs.GetInt("Car_index"));
        //return Tuple.Create(Weapon[rand],TimeWeapon[rand]);

        //int rand = UnityEngine.Random.Range(0, 19);
        //return Tuple.Create(Weapon[rand], TimeWeapon[rand]);

        //TrealerI++;
        //switch (TrealerI) 
        //{
        //    case 0:
        //        return Tuple.Create(2, 0f);
        //    case 1:
        //        return Tuple.Create(6, 0f);
        //    case 2:
        //        return Tuple.Create(9, 0f);
        //    case 3:
        //        return Tuple.Create(12, 0f);
        //    case 4:
        //        return Tuple.Create(16, 0f);
        //    case 5:
        //        return Tuple.Create(14, 0f);
        //    case 6:
        //        return Tuple.Create(13, 0f);

        //}

        if (PlayerPrefs.GetInt("PremNow") == 1)             // если премиум машина 
        {
            int preobr = 0;
            if (UnityEngine.Random.Range(0, 101) >= 50)             // то с 50% шансом возвращяем ее оружие
            {
                switch (PlayerPrefs.GetInt("Cur_car"))
                {
                    case 3:
                        preobr = 20;
                        break;
                    case 4:
                        preobr = 21;
                        break;
                    case 5:
                        preobr = 22;
                        break;
                    case 6:
                        preobr = 23;
                        break;
                    case 7:
                        preobr = 24;
                        break;

                }
                return Tuple.Create(Weapon[preobr], TimeWeapon[preobr]);
            }
            else
            {                                                       // или обычное 
                int rand = UnityEngine.Random.Range(0, 19);
                return Tuple.Create(Weapon[rand], TimeWeapon[rand]);
            }

        }
        else {
            int rand = UnityEngine.Random.Range(0, 19);
            return Tuple.Create(Weapon[rand], TimeWeapon[rand]);
        }

    }

    #endregion




    #region Monster

    // тут монстры которые определенны от карты
    int[,] Maps_and_Monsters = new int[,] 
    {
        { 0, 13, 5 },
        { 1, 2, 8 },
        { 2, 14, 10 },
        { 3, 6, 2 },
        { 4, 11, 7 },
        { 5, 1, 12 },
        { 6, 9, 4 },
        { 7, 3, 2 },
        { 8, 6, 7 },
        { 9, 4, 13 },
        { 10, 15, 9 }
    };

    List<int> monsters = new List<int>();

    // составление листа монстров на основе используемых карт (их может быть несколько одновременно)
    public void CreateMontersList(int maps_num) 
    {
        monsters.Clear();
        monsters.Add(Maps_and_Monsters[maps_num,1]);
        monsters.Add(Maps_and_Monsters[maps_num,2]);
    }

    public int GetRandomMonster() {
        return monsters[UnityEngine.Random.Range(0, monsters.Count)];
    }

    #endregion

    #region PremCar

    public void InitPremCar(Transform Scroll_car_content) 
    {
        if (PlayerPrefs.GetInt("PremCar") == 1) 
        {
            for (int i = 3; i <= 7; i++)
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("cars/car_scrol_content"), Scroll_car_content);
                temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("cars/Prem/" + i);
                temp.name = "Premcar" + "-" + i;
            }
        }
    }

    #endregion


    #region Car

    float[] lvl_car = new float[]
    {
        1,
        4,
        //27,
        //27,
        //27,
        //27,
        //27,
        8,
        10,
        12,
        14,
        16,
        17,
        18,
        19,
        19.9f,
        20.8f,
        21.7f,
        22.6f,
        23.4f,
        24.2f,
        24.8f,
        25.1f,
        25.2f,
        29,
        27,
        27,
        27,
        27,
        27,
    };
    int[] car_price = new int[] 
    {
        100,
        200,
        350,
        600,
        1200,
        2500,
        3000,
        3500,
        4400,
        5600,
        7000,
        7300,
        7600,
        7900,
        8100,
        8500,
        8700,
        9000,
        9200,
        10000
    };

    List<int> Buyed_car = new List<int>();

    // считывание из файла и заполенение horizontal group (scroll) уже полученными машинами
    public void Initialized_Car(Transform Scroll_car_content) 
    {
        InitPremCar(Scroll_car_content);
        Initialized(Scroll_car_content, true, "C", "cars/Sprite/");
        Constr_car();
    }

    public bool CheckCar()
    {
        if (PlayerPrefs.GetInt("Car_index") >= car_price.Length-1)
            return true;
        else
            return false;

    }

    public void Constr_car()       // собирает машину 
    {
        
        Transform car = GameObject.FindGameObjectWithTag("Player").transform;
        if (PlayerPrefs.GetInt("PremNow") == 0)
            car.GetComponent<Car>().lvl = PlayerPrefs.GetFloat("Cur_car_lvl");
        else
            car.GetComponent<Car>().lvl = 27;

        car.GetComponent<Car>().num_car = PlayerPrefs.GetInt("Cur_car");
        //car.GetComponent<Car>().num_car = 4;    //убрать
        car.GetComponent<Car>().Reload_animation();
        GameObject.FindGameObjectWithTag("CarP").GetComponent<CarHelper>().CheckHelp(PlayerPrefs.GetInt("Cur_car"));

    }



    public void Buy_car(int skinID) 
    {
        Buyed_car.Add(skinID);
        PlayerPrefs.SetInt("Car_index", PlayerPrefs.GetInt("Car_index") + 1);
        PlayerPrefs.SetFloat("Cur_car_lvl", lvl_car[PlayerPrefs.GetInt("Car_index")]);
        PlayerPrefs.SetInt("Cur_car", skinID);
        PlayerPrefs.Save();
        SaveSkin(skinID,"C", "Market_UI/sc/bg_right/Scroll_car/Viewport/Content","car");
    }


    public void Pic_car(string car) 
    {
        int car_skin = Convert.ToInt32(car);

        if (car_skin >= 3 && car_skin <= 7)
            PlayerPrefs.SetInt("PremNow", 1);
        else
            PlayerPrefs.SetInt("PremNow", 0);


        PlayerPrefs.SetInt("Cur_car", car_skin);
        PlayerPrefs.Save();
        Debug.Log("Prem  = " + PlayerPrefs.GetInt("PremNow"));
        Constr_car();
        UI.singleton.Set_ico_car();
    }

    public int GetCarCurPrice() 
    {
        return car_price[PlayerPrefs.GetInt("Car_index")];
    }

    public int GetCar()
    {
        List<int> Mas_aval_car = new List<int>();

        if (PlayerPrefs.GetInt("Car_index") < 2)
        {
            return PlayerPrefs.GetInt("Car_index") + 1 ;
        }
        else
        {
            for (int i = 8; i <= 24; i++)
            {
                if (!Buyed_car.Contains(i))
                    Mas_aval_car.Add(i);
            }
            return Mas_aval_car[UnityEngine.Random.Range(0, Mas_aval_car.Count)];
        }
    }

    #endregion


    #region Map



    public void PremMap(Transform Scroll_map_content) 
    {
        GameObject temp = Instantiate(Resources.Load<GameObject>("cars/car_scrol_content"), Scroll_map_content);
        temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("map/ico/10");
        temp.transform.localScale = new Vector3(1.3f, 1.3f, 1);
        temp.name = "map-10";
        Buyed_map.Add(10);
    }

    List<int> Buyed_map = new List<int>();

    public void Initialized_Map(Transform Scroll_map_content)
    {
        if (PlayerPrefs.GetInt("PremCar") == 1) 
        {
            PremMap(Scroll_map_content);
        }

        Initialized(Scroll_map_content, false, "M", "map/ico/");
        Pic_map(PlayerPrefs.GetInt("Cur_map").ToString());
    }

    public void Pic_map(string map)
    {
        int map_skin = Convert.ToInt32(map);
        PlayerPrefs.SetInt("Cur_map", map_skin);
        PlayerPrefs.Save();
        Destroy(GameObject.FindGameObjectWithTag("Background"));
        GameObject temp = Instantiate(Resources.Load<GameObject>("map/"+map));
        temp.name = map.ToString();
        UI.singleton.Set_ico_map();
    }

    public bool CheckMap() 
    {
        if (Buyed_map.Count >= 9)               // РАСШИРЕНИЕ если добавлять карту надо прибавить +1
            return true;
        else
            return false;
    }

    public void Buy_Map(int skinID)
    {
        Buyed_map.Add(skinID);
        PlayerPrefs.SetFloat("Cur_map_lvl", PlayerPrefs.GetFloat("Cur_map_lvl") * 1.15f);
        PlayerPrefs.SetInt("Cur_map", skinID);
        PlayerPrefs.Save();
        SaveSkin(skinID, "M", "Market_UI/sc/bg_right/Scroll_map/Viewport/Content", "map");
    }

    public int GetMap()                 // доступная карта для покупки 
    {
        List<int> Mas_aval_map = new List<int>();
        for (int i = 0; i <= 9; i++)
        {
            if (!Buyed_map.Contains(i))
                Mas_aval_map.Add(i);
        }
        return Mas_aval_map[UnityEngine.Random.Range(0, Mas_aval_map.Count)];

    }

    public int GetBuyedMap(int map_now) 
    {
        int map = Buyed_map[UnityEngine.Random.Range(0, Buyed_map.Count)];
        if (map == map_now)
            return GetBuyedMap(map_now);
        else
            return map;
    }

    public bool LightOnMap(int numMap) 
    {
        if (numMap == 5 || numMap == 6 || numMap == 8 || numMap == 9)
            return true;
        else
            return false;
    } 

    public int GetMapCurPrice()
    {
        return (int)(PlayerPrefs.GetFloat("Cur_map_lvl") * 220f);
    }

    #endregion



    #region MarketCore

    void Initialized(Transform Scroll,bool car, string FIleName, string IcoParh)
    {
        string sp,name;

        if (car)
        {
            sp = "/frame0";
            name = "car";
        }
        else
        {
            sp = "";
            name = "map";
        }

        // создаем наш лист для открытых орудий
        if (!File.Exists(Application.persistentDataPath + "/"+ FIleName + ".is"))
        {
            if (car)
            {
                Buyed_car.Add(0);
                PlayerPrefs.SetInt("Cur_car", 0);
                PlayerPrefs.SetFloat("Cur_car_lvl", 1);
                PlayerPrefs.SetInt("Car_index", 0);
                Debug.Log(PlayerPrefs.GetFloat("Cur_car_lvl") + " Initial lvl car");
            }
            else
            {
                Buyed_map.Add(0);
                PlayerPrefs.SetInt("Cur_map", 0);
                PlayerPrefs.SetFloat("Cur_map_lvl", 1);
            }
            PlayerPrefs.Save();
            using (var file = File.CreateText(Application.persistentDataPath + "/" + FIleName + ".is"))
            {
                file.WriteLine(0.ToString());
                GameObject temp = Instantiate(Resources.Load<GameObject>("cars/car_scrol_content"), Scroll);
                temp.GetComponent<Image>().sprite = Resources.Load<Sprite>(IcoParh + 0 + sp);
                if (!car) {
                    temp.transform.localScale = new Vector3(1.3f, 1.3f, 1);
                }
                temp.name = name + "-" + 0;
            }
        }
        else
        {
            using (StreamReader file = new StreamReader(Application.persistentDataPath + "/" + FIleName + ".is"))
            {
                while (file.Peek() >= 0)
                {
                    GameObject temp = Instantiate(Resources.Load<GameObject>("cars/car_scrol_content"), Scroll);
                    string line = file.ReadLine();
                    if (car)
                    {
                        Buyed_car.Add(Convert.ToInt32(line));
                    }
                    else
                    {
                        temp.transform.localScale = new Vector3(1.3f, 1.3f, 1);
                        Buyed_map.Add(Convert.ToInt32(line));
                    }
                    temp.name = name+ "-" + Convert.ToInt32(line);
                    temp.GetComponent<Image>().sprite = Resources.Load<Sprite>(IcoParh + line + sp);
                }
            }
        }
    }

    void SaveSkin(int skin, string FIleName, string scroll,string name) 
    {
        using (StreamWriter file = new StreamWriter(Application.persistentDataPath + "/"+ FIleName + ".is", true))
        {
            file.WriteLine(skin);
            GameObject temp = Instantiate(Resources.Load<GameObject>("cars/car_scrol_content"), GameObject.Find(scroll).transform);
            temp.name = name + "-" + skin;
            if (name.Contains("map")) {
                temp.transform.localScale = new Vector3(1.3f,1.3f,1);
            }
            if (name.Contains("car"))
            {
                temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("cars/Sprite/" + skin + "/frame0");
            }
            else
            {
                temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("map/ico/" + skin );
            }
        }
    }


    #endregion

}
