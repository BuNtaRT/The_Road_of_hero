using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoreGenerate 
{

    public static GameObject GenerateObj(string path, float posX, float posY, int layout) 
    {
        return generate(path,posX,posY,layout,null);
    }
    public static GameObject GenerateObj(string path, float posX, float posY, Transform parent, int layout)
    {
        return generate(path, posX, posY, layout,parent);
    }
    public static GameObject GenerateObj(string path, float posX, float posY, Transform parent)
    {
        return generate(path, posX, posY, 0,parent);
    }
    public static GameObject GenerateObj(string path, float posX, float posY, Transform parent, bool world)
    {
        GameObject temp = generate(path, posX, posY, 0,parent);
        if (world)
        {
            temp.transform.SetParent(null);
            return temp;
        }
        else 
        {
            return temp;
        }
    }
    public static GameObject GenerateObj(string path, float posX, float posY)
    {
        return generate(path, posX, posY, 0,null);
    }


    static GameObject generate(string path, float posX, float posY, int layout, Transform parent)
    {
        GameObject temp = null;
        if (parent != null)
        {
            temp = Object.Instantiate(Resources.Load<GameObject>(path));
        }
        else 
        {
            temp = Object.Instantiate(Resources.Load<GameObject>(path),parent);
        }
        temp.transform.position = new Vector3(posX + 10, posY, 0);
        if (layout != 0)
            temp.GetComponent<SpriteRenderer>().sortingOrder = layout;      // нужно что бы поставить например монстра вверх или вниз 
        return temp;

    }
}
