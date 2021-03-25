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
    private int ammo = 0;
    public void PlusAmmo() {
        ammo++;
        OnAmmo?.Invoke(ammo);
    }
    public int GetAmmoCount() 
    {
        return ammo;
    }

    #endregion


    bool locked = false;


    private void Start()
    {
        carGunObj = GameObject.Find("Car").transform.Find("Player_car/GunObj").transform;
        OnAmmo?.Invoke(ammo);
        Debug.Log(ammo);
    }


    public void GoShot() {
        if (ammo >= 1 && !locked) 
        {
            ammo--;
            OnAmmo?.Invoke(ammo);
            Shot();
        }
    }

    Transform carGunObj;
    void Shot() {
        locked = true;                                                  // lock fire
        var tuple = Vault_data.singleton.GetRandomGunFromList();         // tuple get

        GameObject gameObject = Instantiate(Resources.Load<GameObject>("weapon/"+ tuple.Item1), carGunObj);


        // Logic to prem car
        if (tuple.Item1 == 20)
        {
            GameObject temp =  Instantiate(Resources.Load<GameObject>("weapon/Prem20"), carGunObj.parent);
            Destroy(temp,3f);
        }
        if (tuple.Item1 == 21 || tuple.Item1 == 22)
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("weapon/Prem"+ tuple.Item1), carGunObj);
            Destroy(temp, 3.8f);
        }

        StartCoroutine(Wait(tuple.Item2,gameObject));
    }

    IEnumerator Wait(float wait, GameObject createt_weapon)        // wait cooldown
    {
        yield return new WaitForSeconds(wait);
        locked = false;
        Destroy(createt_weapon,5f);
    }
}
