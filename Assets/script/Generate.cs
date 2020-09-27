using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public GameObject generate(string path, float posX, float posY ) {

        GameObject temp = Instantiate<GameObject>(Resources.Load<GameObject>(path));
        temp.transform.position = new Vector3(posX,posY,0);
        return temp;
    }
}

