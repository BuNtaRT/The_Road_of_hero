using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConetnPic : MonoBehaviour
{
    public void Pic_this(GameObject temp) 
    {
        if (temp.name.Contains("car")) 
        {
            Vault_data.singleton.Pic_car(temp.name.Split('-')[1]);
        }
    }
}
