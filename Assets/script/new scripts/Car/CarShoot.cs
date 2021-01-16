using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShoot : MonoBehaviour
{
    #region ammo
    int ammo = 10;
    public void PlusAmmo() {
        ammo++;
    }
    #endregion



    public void GoShot() {
        if (ammo >= 1) {
            Shot();
            ammo--;
        }
    }


    void Shot() {
        GameObject gameObject = Instantiate(Resources.Load<GameObject>("weapon/"+ StartupFirst.singleton.GetRandomFromList()));
        gameObject.GetComponent<Transform>().SetParent(GameObject.Find("Car").transform);
    } 
}
