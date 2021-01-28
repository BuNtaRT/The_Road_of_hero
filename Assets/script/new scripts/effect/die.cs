using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class die : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Die());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    float speed = 0.2f;
    Color Startcolor, Endcolor;

    IEnumerator Die()
    {
        Startcolor = gameObject.GetComponent<SpriteRenderer>().color;
        Endcolor = new Color(Startcolor.r,Startcolor.g,Startcolor.b,0.2f);

        while (true)
        {
            for (float time = 0; time < (speed ) * 2; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, speed) / (speed );
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Startcolor,Endcolor,progress);
                yield return null;
            }
        }
    }
}
