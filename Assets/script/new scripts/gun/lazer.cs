using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    private string effectDie;
    private string soundDie;

    private void Start()
    {
        var gunConf = StartAndDieEffForGun.Get_weap_content(gameObject.GetComponent<GunName>().Name);
        Debug.Log(gunConf.Item1 +  " Name lazer ");
        gameObject.transform.Find("Audio").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("audio_effect/" + gunConf.Item1);
        gameObject.transform.Find("Audio").GetComponent<AudioSource>().Play();
        effectDie = gunConf.Item2;
        soundDie = gunConf.Item3;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagMonster.Monsters.Contains(collision.gameObject.tag))
        {
            MonstaersDie.DieMonster(collision.gameObject, effectDie,soundDie);
        }
    }
}


