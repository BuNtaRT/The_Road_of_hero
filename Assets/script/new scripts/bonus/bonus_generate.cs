using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class bonus_generate : MonoBehaviour
{
    //обращение из Lat каждый раз когда генерируем препятствия, мы возможно генерируем бонус 
    public void MaybeBonus(float genX) 
    {
        
    }

    public void generateLine(string path, int line,float genX) 
    {
        float plus = Random.Range(-5,5);

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
        GameObject temp =  CoreGenerate.GenerateObj(path,x,y);
        temp.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = layout;
    }
}
