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

        for(int i  = 0;  i < 1; i++) { 
            for (float time = 0; time < (speed ) * 3; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, speed) / (speed );
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Startcolor,Endcolor,progress);
                yield return null;
            }
        }

        for (float time = 0; time < (speed) * 3; time += Time.deltaTime)
        {
            float progress = time / speed;
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Startcolor, new Color(Startcolor.r, Startcolor.g, Startcolor.b, 0), progress);
            yield return null;
        }
    }
}
