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
        if (gunConf.Item1 != "GuitarGun")
        {
            CoreAudio.Create_audio_eff(gunConf.Item1);
        }
        effectDie = gunConf.Item2;
        SoundDie = gunConf.Item3;
    }

    IEnumerator Lerp_rocket()
    {
        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 1f;
            gameObject.transform.position = Vector3.Lerp(startVector, endVector, timeStep);
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagMonster.Monsters.Contains(collision.tag)) 
        {
            if (collision.GetComponent<enemy_controll>() == null || (collision.GetComponent<enemy_controll>() != null && !collision.GetComponent<enemy_controll>().undestroy))
            {
                DeactivR();
                MonstaersDie.DieMonster(collision.gameObject, effectDie, SoundDie);
                StopAllCoroutines();
                Destroy(gameObject, 1f);
            }
            else if (collision.GetComponent<FakeShadow>() != null) 
            {
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Boss")
        {
            DeactivR();
            if (collision.gameObject.transform.parent.GetComponent<BossDamage>() != null)
                collision.gameObject.transform.parent.GetComponent<BossDamage>().Damage();
            StopAllCoroutines();
            Destroy(gameObject, 1f);
        }
    }

    void DeactivR() 
    {
        foreach (Transform temp in gameObject.transform)
        {
            if (temp.tag == "particle")
            {
                temp.gameObject.AddComponent<DestroyParticle>();
            }
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
