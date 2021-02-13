using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiefromLazerSmall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            CoreEffect.Create_effect("LazerSmallP",0,-1.32f,gameObject.transform);
            Destroy(gameObject, 2);
        }
    }
}
