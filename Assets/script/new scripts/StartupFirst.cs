using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StartupFirst : MonoBehaviour
{
    List<int> OpenWeapon = new List<int>();

    public static StartupFirst singleton { get; private set; }


    void Awake()
    {
        singleton = this;
        // создаем наш лист для открытых орудий
        if (!File.Exists(Application.persistentDataPath + "/W.is"))
        {
            using (var file = File.CreateText(Application.persistentDataPath + "/W.is"))
            {
                file.WriteLine(0.ToString());
            }
        }
        else {
            using (StreamReader file = new StreamReader(Application.persistentDataPath + "/W.is"))
            {
                while(file.Peek() >= 0) { 
                    OpenWeapon.Add(Convert.ToInt32(file.ReadLine()));
                }
            }
        }
    }

    // добавляем орудие в лист для использований и сохраняем в файл
    public void AddWeapon(int weapon) {
        using (StreamWriter file = new StreamWriter(Application.persistentDataPath + "/W.is")) {
            file.WriteLine(weapon.ToString());
        }
        OpenWeapon.Add(weapon);
    }

    public int GetRandomFromList() {
         return UnityEngine.Random.Range(0, OpenWeapon.Count);
    }

}
