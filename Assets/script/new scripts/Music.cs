using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    Coroutine music;
    bool audio;
    string name = null;

    public void SwitchToBoss(bool val) 
    {
        if (val && audio)
            name = "boss/" + Random.Range(0, 4);
        else if (!val && audio) 
            name = null;
        StopAllCoroutines();
        Destroy(GameObject.FindGameObjectWithTag("audio"));
        music = StartCoroutine(Controll_music());
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
            if (name == null)
            {
                int rand = 0;
                rand = Random.Range(0, 23);
                yield return new WaitForSeconds(CoreAudio.CreateMusic(rand) + 0.1f);
            }
            else 
                yield return new WaitForSeconds(CoreAudio.CreateMusic(name) + 0.1f);
        }
        while (audio);
    }
}
