using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoreEffect
{
    public static float Create_effect(string path, float x, float y)
    {
        return Create(path, x, y, null, false);
    }

    public static float Create_effect(string path, float x, float y, Transform parent)
    {
        return Create(path, x, y, parent, false);
    }
    public static float Create_effect(string path, float x, float y, Transform parent, bool world)
    {
        return Create(path, x, y, parent, world);
    }

    public static float Create_effect(string path, float x, float y, Transform parent, bool world,string audioEff)
    {
        CoreAudio.Create_audio_eff(audioEff);
        return Create(path, x, y, parent, world);
    }


    private static float Create(string path, float x, float y, Transform parent, bool world)
    {

        GameObject temp = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("effect/" + path));
        temp.name = path;

        if (parent != null)
        {
            temp.transform.SetParent(parent);
            temp.transform.localPosition = new Vector3(x, y, 0);
        }
        else
        {
            temp.transform.localPosition = new Vector3(x, y, 0);
        }
        if (world)
        {
            temp.transform.SetParent(null);
        }

        if (temp.GetComponent<Animator>())
        {

            UnityEngine.Object.Destroy(temp, temp.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
            return temp.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length;
        }
        else
        {
            UnityEngine.Object.Destroy(temp, 5);
            return 5;
        }

    }

    public static void Effect_die(GameObject obj, string sound,float time_deat) 
    {
        obj.AddComponent<die>();
        UnityEngine.Object.Destroy(obj, time_deat);
        CoreAudio.Create_audio_eff(sound);
    }

    //public static void Effect_die(GameObject obj, string sound)
    //{
    //    UnityEngine.Coroutine.StartCoroutine(Deat_IE(obj, sound));
    //}

    //public void Effect_bonus(GameObject obj, string sound, int count)
    //{

    //    obj.transform.GetChild(0).GetComponent<number_show>().Num_get(count);

    //    StartCoroutine(Bonus_eff(obj, sound));
    //    StartCoroutine(Bonus_eff_transparen(obj.GetComponent<SpriteRenderer>()));
    //}

    //static IEnumerator Bonus_eff(GameObject obj, string sound)
    //{
    //    float y = obj.transform.position.y + 2f;
    //    var timeStep = 0.0f;
    //    GameObject.Find("script").GetComponent<AudioCore>().Create_audio_eff(sound);
    //    timeStep = 0.0f;
    //    while (timeStep < 1.0f)
    //    {
    //        timeStep += Time.deltaTime / 0.8f;
    //        obj.transform.position = Vector3.Lerp(obj.transform.position, new Vector3(obj.transform.position.x, y), timeStep);
    //        yield return null;
    //    }
    //}
    //static IEnumerator Bonus_eff_transparen(SpriteRenderer obj)
    //{
    //    float y = obj.transform.position.y + 2f;
    //    var timeStep = 0.0f;
    //    while (timeStep < 1.0f)
    //    {
    //        timeStep += Time.deltaTime / 1f;
    //        obj.color = Color.Lerp(obj.color, new Color(obj.color.r, obj.color.g, obj.color.b, 0), timeStep);
    //        yield return null;
    //    }
    //}

    //static IEnumerator Deat_IE(GameObject obj, string sound)
    //{

    //    GameObject.Find("script").GetComponent<AudioCore>().Create_audio_eff(sound);

    //    Color sprite_color = obj.GetComponent<SpriteRenderer>().color;

    //    var timeStep = 0.0f;

    //    for (int i = 0; i < 2; i++)
    //    {
    //        timeStep = 0.0f;
    //        while (timeStep < 1.0f)
    //        {
    //            timeStep += Time.deltaTime / 0.1f;
    //            obj.GetComponent<SpriteRenderer>().color = Color.Lerp(sprite_color, new Color(0.8867924f, 0.8867924f, 0.8867924f), timeStep);
    //            yield return null;
    //        }
    //        timeStep = 0.0f;
    //        while (timeStep < 1.0f)
    //        {
    //            timeStep += Time.deltaTime / 0.1f;
    //            obj.GetComponent<SpriteRenderer>().color = Color.Lerp(obj.GetComponent<SpriteRenderer>().color, sprite_color, timeStep);
    //            yield return null;
    //        }
    //    }
    //    timeStep = 0.0f;
    //    while (timeStep < 1.0f)
    //    {
    //        timeStep += Time.deltaTime / 0.1f;
    //        obj.GetComponent<SpriteRenderer>().color = Color.Lerp(sprite_color, new Color(sprite_color.r, sprite_color.g, sprite_color.b, 0f), timeStep);
    //        yield return null;
    //    }
    //    UnityEngine.Object.Destroy(obj);

    //}
}
