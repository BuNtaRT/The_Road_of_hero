using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    private string effectDie;
    private string soundDie;


    private void Start()
    {
        if (gameObject.GetComponent<GunName>().Name.Contains("BonusSheld"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Car>().DevUndie = true;
            effectDie = "SphereEnergy_effect_die";
            soundDie = "EnergySphereDie";
            Destroy(transform.parent.gameObject,6.01f);
        }
        else
        {
            var gunConf = StartAndDieEffForGun.Get_weap_content(gameObject.GetComponent<GunName>().Name);
            gameObject.transform.Find("Audio").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("audio_effect/" + gunConf.Item1);
            gameObject.transform.Find("Audio").GetComponent<AudioSource>().Play();
            effectDie = gunConf.Item2;
            soundDie = gunConf.Item3;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagMonster.Monsters.Contains(collision.gameObject.tag))
        {
            MonstaersDie.DieMonster(collision.gameObject, effectDie, soundDie);
        }
        else if (collision.gameObject.tag == "Boss") 
        {
            foreach (Transform temp in gameObject.transform) 
            {
                if (temp.GetComponent<BoxCollider2D>() != null) 
                {
                    temp.gameObject.SetActive(false);
                }
            }
            collision.gameObject.transform.parent.GetComponent<Bee>().minusHP();
        }
    }

    private void OnDestroy()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Car>().DevUndie = false;
    }
}


