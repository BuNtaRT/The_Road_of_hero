using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoreAudio 
{
    public static bool on = true;

    #region Music
    public static float CreateMusic(string name)
    {
        // PlayerPrefs.LoadFloat("vol_music");
        return Create("music/" + name, 0.3f, 128); 
    }
    public static float CreateMusic(int name)
    {
        return CreateMusic(name.ToString());
    }
    #endregion

    #region Audio Effects
    public static float Create_audio_eff(string name)
    {
        // PlayerPrefs.LoadFloat("vol_eff");
        return Create("audio_effect/" + name, 1f, 250);
    }

    public static float Create_audio_eff(int name)
    {
        return Create_audio_eff(name.ToString());
    }
    #endregion


    public static void DestroyAll()
    {
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("audio"))
        {
            UnityEngine.Object.Destroy(temp);
        }
    }


    private static float Create(string path, float vol, int priority)
    {
        vol = 0.05f; // Убрать

        if (on && (GameObject.FindGameObjectWithTag("audio") == null || path.Contains("audio_effect")))
        {
            GameObject temp = new GameObject();
            temp.AddComponent<AudioSource>();
            temp.transform.SetParent(Camera.main.transform);
            temp.transform.localPosition = new Vector3(1, 0, 0);
            temp.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(path);
            temp.GetComponent<AudioSource>().Play();
            temp.GetComponent<AudioSource>().volume = vol;
            temp.GetComponent<AudioSource>().priority = priority;
            temp.name = path + "-vol(" + vol + ")";

            if (path.Contains("music"))
                temp.tag = "audio";
            UnityEngine.Object.Destroy(temp, temp.GetComponent<AudioSource>().clip.length);

            return temp.GetComponent<AudioSource>().clip.length;
        }
        else
        {
            return 5;
        }
    }
}
