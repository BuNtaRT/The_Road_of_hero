﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    Coroutine music;
    bool audio;
    void Start()
    {
        //audio = System.Convert.ToBoolean(PlayerPrefs.GetInt("sound"));

        //music = StartCoroutine(Controll_music());

    }

    public void On_Off(bool on)
    {
        audio = on;
        if (!on)       //если музыка включена и мы хотим выключить её
        {
            if(music != null)
                StopCoroutine(music);
        }
        else  // или же выключена и мы хотим включить
        {
            music = StartCoroutine(Controll_music());
        }
    }

    IEnumerator Controll_music()
    {
        do
        {
            int rand = Random.Range(0, 23);
            yield return new WaitForSeconds(CoreAudio.CreateMusic(rand) + 0.1f);
        }
        while (audio);
    }
}
