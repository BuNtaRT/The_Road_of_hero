using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToRed : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ToRed());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    float speed = 0.2f;
    Color Startcolor, Endcolor;

    IEnumerator ToRed()
    {
        Startcolor = gameObject.GetComponent<SpriteRenderer>().color;
        Endcolor = new Color(0.8867924f, 0.4475792f, 0.4475792f, 0.9f);

        while(true)
        {
            for (float time = 0; time < (speed) * 3; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, speed) / (speed);
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Startcolor, Endcolor, progress);
                yield return null;
            }
        }

    }
}
