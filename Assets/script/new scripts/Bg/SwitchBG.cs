using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBG : MonoBehaviour
{
    // Конструктор для смены карты
    public void Switch(int num) {
        Destroy(GameObject.FindGameObjectWithTag("Background"));
        GameObject map = Instantiate(Resources.Load<GameObject>("map/"+num));
    }
}
