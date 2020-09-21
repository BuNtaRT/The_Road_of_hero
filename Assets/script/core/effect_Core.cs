using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_Core : MonoBehaviour
{

    public void Create_effect(string path, float x,float y) {
        Create(path, x, y, null);
    }

    public void Create_effect(string path, float x, float y, Transform parent) { 
        Create(path, x, y, parent);
    }

    private void Create(string path, float x, float y, Transform parent) {

        GameObject temp = new GameObject();
        temp = Instantiate(Resources.Load<GameObject>("effect/" + path));
        temp.name = path;

        if (parent != null)
        {
            temp.transform.SetParent(parent);
            temp.transform.localPosition = new Vector3(x, y, 0);
        }
        else {
            temp.transform.localPosition = new Vector3(x, y, 0);
        }


        Destroy(temp, temp.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
    }


    public void Effect_die(GameObject obj, string sound) {
        StartCoroutine(Deat_IE(obj, sound));
    }

    IEnumerator Deat_IE(GameObject obj,string sound)
    {

        GameObject.Find("script").GetComponent<AudioCore>().Create_audio_eff(sound);

        Color sprite_color = obj.GetComponent<SpriteRenderer>().color;

        var timeStep = 0.0f;

        for (int i = 0; i < 2; i++)
        {
            timeStep = 0.0f;
            while (timeStep < 1.0f)
            {
                timeStep += Time.deltaTime / 0.15f;
                obj.GetComponent<SpriteRenderer>().color = Color.Lerp(sprite_color, new Color(0.8867924f, 0.8867924f, 0.8867924f), timeStep);
                yield return null;
            }
            timeStep = 0.0f;
            while (timeStep < 1.0f)
            {
                timeStep += Time.deltaTime / 0.15f;
                obj.GetComponent<SpriteRenderer>().color = Color.Lerp(obj.GetComponent<SpriteRenderer>().color, sprite_color, timeStep);
                yield return null;
            }
        }
        timeStep = 0.0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 0.15f;
            obj.GetComponent<SpriteRenderer>().color = Color.Lerp(sprite_color, new Color(sprite_color.r, sprite_color.g, sprite_color.b, 0f), timeStep);
            yield return null;
        }
        Destroy(obj);

    }
}
