using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHelper : MonoBehaviour
{
    private void Start()
    {
    }

    Transform curHelper = null;
    public void CheckHelp(int numCar) 
    {
        if (curHelper != null)
            Destroy(curHelper.gameObject);

        if (Resources.Load<GameObject>("cars/Helper/Helper" + numCar) != null) 
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("cars/Helper/Helper" + numCar),transform.Find("Player_car/GunObj"));
            curHelper = temp.transform;
        }
    }
}
