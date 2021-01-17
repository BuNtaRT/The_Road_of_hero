using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StartupFirst : MonoBehaviour
{
    List<int> OpenWeapon = new List<int>();
    List<float> TimeWeapon = new List<float>();

    public static StartupFirst singleton { get; private set; }


    void Awake()
    {
        singleton = this;
        // создаем наш лист для открытых орудий
        if (!File.Exists(Application.persistentDataPath + "/W.is"))
        {
            using (var file = File.CreateText(Application.persistentDataPath + "/W.is"))
            {
                file.WriteLine(0.ToString()+"/2");
            }
        }

        using (StreamReader file = new StreamReader(Application.persistentDataPath + "/W.is"))
        {
            while (file.Peek() >= 0) {
                string line = file.ReadLine();
                OpenWeapon.Add(Convert.ToInt32(line.Split('/')[0]));
                TimeWeapon.Add((float)Convert.ToDouble(line.Split('/')[1]));
            }
        }

        Debug.Log(OpenWeapon.Count);
        Debug.Log(TimeWeapon[0]);
    }

    // добавляем орудие в лист для использований и сохраняем в файл
    public void AddWeapon(int weapon,double time_weap)
    {
        using (StreamWriter file = new StreamWriter(Application.persistentDataPath + "/W.is")) {
            file.WriteLine(weapon.ToString()+"/"+time_weap);
        }
        OpenWeapon.Add(weapon);
    }


    // получаем случайное доступное оружие(его номер)
    public Tuple<int,float> GetRandomFromList() 
    {
        int rand = UnityEngine.Random.Range(0, OpenWeapon.Count);
        return Tuple.Create(OpenWeapon[rand],TimeWeapon[rand]);
    }

}
