using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoreGenerate 
{

    public static GameObject GenerateObj(string path, float posX, float posY, int layout) 
    {
        return generate(path,posX,posY,0,layout,null);
    }
    public static GameObject GenerateObj(string path, float posX, float posY, Transform parent, int layout)
    {
        return generate(path, posX, posY,0, layout,parent);
    }
    public static GameObject GenerateObj(string path, float posX, float posY, Transform parent)
    {
        return generate(path, posX, posY,0, 0,parent);
    }
    public static GameObject GenerateObj(string path, float posX, float posY,float posZ, Transform parent)
    {
        return generate(path, posX, posY, posZ, 0,parent);
    }

    public static GameObject GenerateObj(string path,float posX,float posY, Transform parentCoordinate, bool world) 
    {
        GameObject temp = generate(path, posX, posY,0, 0, parentCoordinate);
        if (world)
        {
            temp.transform.SetParent(null);
            temp.transform.position = new Vector3(parentCoordinate.position.x + posX, parentCoordinate.position.y + posY, parentCoordinate.position.z);
            return temp;
        }
        else
        {
            return temp;
        }

    }
    public static GameObject GenerateObj(string path, float posX, float posY, float posZ)
    {
        return generate(path, posX, posY,posZ, 0,null);
    }


    static GameObject generate(string path, float posX, float posY, float posZ, int layout, Transform parent)
    {
        GameObject temp = null;
        if (parent != null)
        {
            temp = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(path),parent);
        }
        else 
        {
            temp = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(path));
        }
        temp.transform.localPosition = new Vector3(posX, posY, posZ);
        if (layout != 0)
            temp.GetComponent<SpriteRenderer>().sortingOrder = layout;      // нужно что бы поставить например монстра вверх или вниз 
        return temp;

    }
}
