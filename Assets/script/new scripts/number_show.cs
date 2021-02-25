using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class number_show : MonoBehaviour
{
    public Text textNum; 

    public void NumGet(int count) 
    {
        textNum.enabled = true;
        textNum.text = "+" + count.ToString();
        StartCoroutine(Num_transparen(textNum));
    }
    IEnumerator Num_transparen(Text Num)
    {
        Color past = Num.color;
        var timeStep = 0.0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 0.3f;
            Num.color = Color.Lerp(past, new Color(past.r, past.g, past.b, 0), timeStep);
            yield return null;
        }
    }
}
