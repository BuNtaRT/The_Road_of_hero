using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShoot : MonoBehaviour
{
    #region singleton
    public static CarShoot singleton;

    private void Awake()
    {
        singleton = this;
    }
    #endregion

    #region Event
    public delegate void Ammo(int ammo);
    public event Ammo OnAmmo;
    #endregion

    // тут происходит стрельба по нажатию а так же контроль боезапаса 

    #region ammo
    public int ammo = 10;
    public void PlusAmmo() {
        ammo++;
        OnAmmo?.Invoke(ammo);
    }
    #endregion


    bool locked = false;


    private void Start()
    {
        car = GameObject.Find("Car").transform.GetChild(0);
        OnAmmo?.Invoke(ammo);
    }


    public void GoShot() {
        if (ammo >= 1 && !locked) {
            ammo--;
            OnAmmo?.Invoke(ammo);
            Shot();
        }
    }

    Transform car;
    void Shot() {
        locked = true;                                                  // lock fire
        var tuple = Vault_data.singleton.GetRandomGunFromList();         // tuple get

        GameObject gameObject = Instantiate(Resources.Load<GameObject>("weapon/"+ tuple.Item1),car);

        StartCoroutine(Wait(tuple.Item2,gameObject));
    }

    IEnumerator Wait(float wait, GameObject createt_weapon)        // wait cooldown
    {
        yield return new WaitForSecondsRealtime(wait);
        locked = false;
        Destroy(createt_weapon,5f);
    }
}
