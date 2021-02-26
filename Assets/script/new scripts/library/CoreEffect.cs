﻿using System.Collections;
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
        GameObject temp = CoreGenerate.GenerateObj("effect/"+path, x, y, parent, world);
            
        temp.name = path;

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

    public static GameObject Effect_die(Transform obj, string sound,string effect) 
    {

        CoreAudio.Create_audio_eff(sound);
        return UnityEngine.Object.Instantiate(Resources.Load<GameObject>("effect/Die/" + effect), obj.position, new Quaternion());
    }
}
