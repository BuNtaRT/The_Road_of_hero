using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCore : MonoBehaviour
{
    public bool on = true;

    public float CreateMusic(string name)
    {
        return Create("music/" + name, 0.25f , 128); // PlayerPrefs.LoadFloat("vol_music");
    }



    public float Create_audio_eff(string name)
    {
        return Create("audio_effect/" + name, 0.25f , 200); // PlayerPrefs.LoadFloat("vol_eff");
    }

    public float Create_audio_eff(int name)
    {
        return Create_audio_eff(name.ToString()); 
    }
    public float CreateMusic(int name)
    {
        return CreateMusic(name.ToString());
    }

    public void DestroyAll() {
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("audio")) {
            Destroy(temp);
        }
    }

    private float Create(string path, float vol, int priority) {


        if (on)
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
            temp.tag = "audio";

            Destroy(temp, temp.GetComponent<AudioSource>().clip.length);

            return temp.GetComponent<AudioSource>().clip.length;
        }
        else {
            return 10000f;
        }
    }
}
