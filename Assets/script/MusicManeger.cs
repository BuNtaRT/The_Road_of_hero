using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManeger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Controll_music());

    }

    IEnumerator Controll_music() {

        while (true)
        {
            int rand = Random.Range(0, 23);
            yield return new WaitForSeconds(GameObject.Find("script").GetComponent<AudioCore>().CreateMusic(rand.ToString()));
        
        }
    }
}
