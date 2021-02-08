using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Vector3 startVector, endVector;
    string effectDie = "";
    string SoundDie = "";
    public SpriteRenderer CustomSP;
    private void Start()
    {
        transform.SetParent(null);
        startVector = transform.position;
        endVector = new Vector3(transform.position.x + 50, transform.position.y);
        StartCoroutine(Lerp_rocket());
        var gunConf = StartAndDieEffForGun.Get_weap_content(gameObject.GetComponent<GunName>().Name);
        CoreAudio.Create_audio_eff(gunConf.Item1);
        effectDie = gunConf.Item2;
        SoundDie = gunConf.Item3;
    }

    IEnumerator Lerp_rocket()
    {
        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 1f;
            gameObject.transform.position = Vector3.Lerp(startVector, endVector /*cubeBezier3(startVector, P1, P2, endVector, timeStep)*/, timeStep);
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagMonster.Monsters.Contains(collision.tag)) 
        {
            if (gameObject.transform.Find("Particle System") != null) 
            {
                gameObject.transform.Find("Particle System").GetComponent<ParticleSystem>().Stop();
            }
            if (gameObject.GetComponent<BoxCollider2D>() != null) 
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            if (gameObject.GetComponent<SpriteRenderer>() != null)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (CustomSP != null) 
            {
                CustomSP.enabled = false;
            }

            MonstaersDie.DieMonster(collision.gameObject, effectDie,SoundDie);
            StopAllCoroutines();
            Destroy(gameObject,1f);
        }
    }


    private void OnDestroy()
    {
        foreach (Transform temp in gameObject.transform)
        {
            if (temp.GetComponent<ParticleSystem>() != null)
                temp.gameObject.AddComponent<DestroyParticle>();
        }
    }
}
