using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Vault_data : MonoBehaviour
{


    public static Vault_data singleton { get; private set; }


    void Awake()
    {
        PlayerPrefs.DeleteAll();    ///////////////////////////////////////////////////////////////////////// убрать!!!!!!!!
        singleton = this;

        Initialized_Weapon();

    }


    #region Weapon 

    List<int> OpenWeapon = new List<int>();     // открытое орудие 
    List<float> TimeWeapon = new List<float>();     // его тайминг cooldown


    // добавляем орудие в лист для использований и сохраняем в файл
    public void AddWeapon(int weapon,double time_weap)
    {
        using (StreamWriter file = new StreamWriter(Application.persistentDataPath + "/W.is")) {
            file.WriteLine(weapon.ToString()+"/"+time_weap);
        }
        OpenWeapon.Add(weapon);
    }


    // получаем случайное доступное оружие(его номер, cooldown)
    public Tuple<int,float> GetRandomGunFromList() 
    {
        int rand = UnityEngine.Random.Range(0, OpenWeapon.Count);
        return Tuple.Create(OpenWeapon[rand],TimeWeapon[rand]);
    }


    public void Initialized_Weapon() 
    {
        // создаем наш лист для открытых орудий
        if (!File.Exists(Application.persistentDataPath + "/W.is"))
        {
            using (var file = File.CreateText(Application.persistentDataPath + "/W.is"))
            {
                file.WriteLine(0.ToString() + "/2");
            }
        }

        using (StreamReader file = new StreamReader(Application.persistentDataPath + "/W.is"))
        {
            while (file.Peek() >= 0)
            {
                string line = file.ReadLine();
                OpenWeapon.Add(Convert.ToInt32(line.Split('/')[0]));
                TimeWeapon.Add((float)Convert.ToDouble(line.Split('/')[1]));
            }
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
        { 7, 13, 5 },       //три карты недоделаны !! убрать
        { 8, 13, 5 },
        { 9, 13, 5 }
    };

    List<int> monsters = new List<int>();

    // составление листа монстров на основе используемых карт (их может быть несколько одновременно)
    public void CreateMontersList(List<int> maps_num) 
    {
        monsters.Clear();

        foreach (int m in maps_num)
        {
            for (int i = 0; i <= Maps_and_Monsters.Length; i++) {
                if (i == m) {
                    monsters.Add(Maps_and_Monsters[i,1]);
                    monsters.Add(Maps_and_Monsters[i,2]);
                }
            }
        }
    }

    public int GetRandomMonster() {
        return UnityEngine.Random.Range(0, monsters.Count);
    }

    #endregion


    #region Car

    float[] lvl_car = new float[]
    {
        1,
        4,
        27,
        27,
        27,
        27,
        27,
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
        29
    };

    Transform Curret_car_content;


    // считывание из файла и заполенение horizontal group (scroll) уже полученными машинами
    public void Initialized_Car(Transform Scroll_car_content) 
    {
        Curret_car_content = Scroll_car_content;

        // создаем наш лист для открытых орудий
        if (!File.Exists(Application.persistentDataPath + "/C.is"))
        {
            PlayerPrefs.SetInt("Cur_car",0);
            PlayerPrefs.SetInt("Cur_car_lvl",1);
            PlayerPrefs.Save();
            using (var file = File.CreateText(Application.persistentDataPath + "/C.is"))
            {
                file.WriteLine(0.ToString() + "/1");
                GameObject temp = Instantiate(Resources.Load<GameObject>("cars/car_scrol_content0"), Scroll_car_content);
                temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("cars/Sprite/" + 0 + "/frame0");
            }
        }

        using (StreamReader file = new StreamReader(Application.persistentDataPath + "/C.is"))
        {
            while (file.Peek() >= 0)
            {
                GameObject temp = Instantiate(Resources.Load<GameObject>("cars/car_scrol_content"),Scroll_car_content);
                string line = file.ReadLine();
                temp.name = "car-" + Convert.ToInt32(line.Split('/')[0]) + "/" + (float)Convert.ToDouble(line.Split('/')[1]);
                temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("cars/Sprite/" + line.Split('/')[0] + "/frame0");
            }
        }

    }

    void Constr_car()       // собирает машину 
    {
        Transform car = GameObject.FindGameObjectWithTag("Player").transform;
        car.GetComponent<Car>().lvl = PlayerPrefs.GetInt("Cur_car_lvl");
        car.GetComponent<Car>().num_car = PlayerPrefs.GetInt("Cur_car");
        car.GetComponent<Car>().Reload_animation();
    }

    public void Pic_car(string car) 
    {
        int car_skin = Convert.ToInt32(car.Split('/')[0]);
        PlayerPrefs.SetInt("Cur_car", car_skin);
        PlayerPrefs.Save();
        Constr_car();
        UI.singleton.Set_ico();
    }

    #endregion

}
