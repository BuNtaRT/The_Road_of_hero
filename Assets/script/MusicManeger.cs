using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManeger : MonoBehaviour
{
    bool audio = true;

    Coroutine music;

    void Start()
    {
        //audio = System.Convert.ToBoolean(PlayerPrefs.GetInt("sound"));
        if (audio)
        {
            music = StartCoroutine(Controll_music());
        }

    }

    public void On(bool on) {

        if (on && audio != on)
        {
            audio = on;
            StartCoroutine(Controll_music());
        }
        else if (!on && audio != on) {
            StopCoroutine(music);
            audio = on;
        }

    }

    IEnumerator Controll_music() {
        while (audio)
        {

            int rand = Random.Range(0, 23);
            yield return new WaitForSeconds(GameObject.Find("script").GetComponent<AudioCore>().CreateMusic(rand.ToString()) + 0.1f);
            


        }
    }
}
