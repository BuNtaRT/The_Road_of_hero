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

    public List<int> monsters = new List<int>();

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

}
