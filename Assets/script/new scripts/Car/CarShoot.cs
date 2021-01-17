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

    bool locked = false;


    public void GoShot() {
        if (ammo >= 1 && !locked) {
            ammo--;
            Shot();
        }
    }


    void Shot() {
        locked = true;                                                  // lock fire
        var tuple = StartupFirst.singleton.GetRandomFromList();         // tuple get
        GameObject gameObject = Instantiate(Resources.Load<GameObject>("weapon/"+ tuple.Item1));
        gameObject.GetComponent<Transform>().SetParent(GameObject.Find("Car").transform);
        StartCoroutine(Wait(tuple.Item2));
    }

    IEnumerator Wait(float wait)        // wait cooldown
    {
        yield return new WaitForSecondsRealtime(wait);
        locked = false;

    }
}
