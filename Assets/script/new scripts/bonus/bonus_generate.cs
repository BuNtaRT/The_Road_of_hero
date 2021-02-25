using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class bonus_generate : MonoBehaviour
{
    //обращение из Lat каждый раз когда генерируем препятствия, мы возможно генерируем бонус 
    public void MaybeBonus(float genX) 
    {
        int rand = Random.Range(0, 100);
        if (rand >= 50) 
        {
            int line = Random.Range(0, 3);
            generateLine(bonus_path(), line, genX);
            if (rand >= 85) 
            {
                int line2;
                while (true) 
                {
                    line2 = Random.Range(0, 3);
                    if (line != line2) 
                    {
                        break;
                    }
                }
                generateLine(bonus_path(), line2, genX);
            }
        }
    }

    string bonus_path() 
    {
        int rand = Random.Range(0, 101);
        if (rand >= 80)
        {
            if (Random.Range(0, 101) >= 50) 
                return "bonus/BonusPac";
            else
                return "bonus/BonusS";
        }
        else if (rand >= 65)
        {
            return "bonus/BonusС";
        }
        else 
        {
            return "bonus/BonusR";
        }
    }

    public void generateLine(string path, int line,float genX) 
    {
        float plus = Random.Range(0,8);

        if (line == 0)
        {
            generateBonus(path, genX + plus, -3.299f,11);
        }
        else if (line == 1)
        {
            generateBonus(path, genX+plus, -3.979f,13);
        }
        else if (line == 2) 
        {
            generateBonus(path, genX+plus, -4.593f,16);
        }
    }


    void generateBonus(string path,float x,float y,int layout) 
    {
        GameObject temp =  CoreGenerate.GenerateObj(path,x,y,0);
        temp.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = layout;
    }
}
