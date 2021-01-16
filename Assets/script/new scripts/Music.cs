using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    bool audio = true;
    Coroutine music;
    void Start()
    {
        //audio = System.Convert.ToBoolean(PlayerPrefs.GetInt("sound"));

        //music = StartCoroutine(Controll_music());

    }

    public void On_Off(bool on)
    {
        if ((audio == true) && (on != audio))       //если музыка включена и мы хотим выключить её
        {
            audio = on;
            if(music != null)
                StopCoroutine(music);
        }
        else if ((audio == false) && (on != audio)) // или же выключена и мы хотим включить
        {
            audio = on;
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
