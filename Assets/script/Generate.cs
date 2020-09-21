using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public void generate(string path, float posX, float posY ) {

        GameObject gameObject = Instantiate<GameObject>(Resources.Load<GameObject>(path));
        gameObject.transform.position = new Vector3(posX,posY,0);
    }
}

