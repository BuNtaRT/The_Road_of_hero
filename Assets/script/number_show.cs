using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class number_show : MonoBehaviour
{
    public Transform plus, num1, num2;

    public void Num_get(int count) {
        plus.gameObject.SetActive(true);
        StartCoroutine(Num_transparen(plus.GetComponent<SpriteRenderer>()));

        if (count <= 9)
        {
            Num(num1.GetComponent<SpriteRenderer>(),count);
            StartCoroutine(Num_transparen(num1.GetComponent<SpriteRenderer>()));
        }
        else {
            int num1_sprite, num2_sprite;

            num2_sprite = count % 10;

            num1_sprite = (count - num2_sprite) / 10;

            Num(num1.GetComponent<SpriteRenderer>(), num1_sprite);
            Num(num2.GetComponent<SpriteRenderer>(), num2_sprite);
            num2.gameObject.SetActive(true);
            StartCoroutine(Num_transparen(num1.GetComponent<SpriteRenderer>()));
            StartCoroutine(Num_transparen(num2.GetComponent<SpriteRenderer>()));
        }
        num1.gameObject.SetActive(true);

    }

    void Num(SpriteRenderer currect_sprite, int num) {
        currect_sprite.sprite = Resources.Load<Sprite>("number/"+num);
    }

    IEnumerator Num_transparen(SpriteRenderer obj)
    {
        var timeStep = 0.0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 1f;
            obj.color = Color.Lerp(obj.color, new Color(obj.color.r, obj.color.g, obj.color.b, 0), timeStep);
            yield return null;
        }
    }
}
