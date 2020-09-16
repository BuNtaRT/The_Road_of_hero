using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    void generate(string path, float posX, float posY ) {
        GameObject gameObject = new GameObject();
        gameObject = Resources.Load<GameObject>(path);
        gameObject.transform.position = new Vector3(posX,posY,0);
    }
}

