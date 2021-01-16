using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_controller : MonoBehaviour
{

    float startPos = 0;
    float endPos = 0;
    public Transform zone_end;
    Transform car;
    Vector3 start_bomb_pos;
    SpriteRenderer end_sone_SP;
    Color start_color_bomb_zone;

    void Start()
    {
        start_color_bomb_zone = zone_end.GetComponent<SpriteRenderer>().color;
        end_sone_SP = zone_end.GetComponent<SpriteRenderer>();
        try
        {
            car = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
            startPos = car.position.x;
            endPos = zone_end.position.x;
            start_bomb_pos = transform.localPosition;
            StartCoroutine(Lerp_bomb());
        }
        catch
        {
            Destroy(transform.parent.gameObject);
        }
        

    }

    IEnumerator Lerp_bomb() {

        float leght = endPos - startPos;
        float tick;
        while (true && car.GetComponent<BoxCollider2D>().enabled)
        {
            tick = 1 - ((endPos - car.position.x) / leght);
            end_sone_SP.color = Color.Lerp(start_color_bomb_zone,new Color(start_color_bomb_zone.r, start_color_bomb_zone.g, start_color_bomb_zone.b, 0.40f),tick);
            transform.localPosition = Vector3.Lerp(start_bomb_pos, new Vector3(start_bomb_pos.x, -3),tick);
            yield return null;
        }
        if (!car.GetComponent<BoxCollider2D>().enabled) {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "bomb_zone") {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Find("script").GetComponent<AudioCore>().Create_audio_eff("expl");
            GameObject.Find("script").GetComponent<effect_Core>().Create_effect("explosion_enemy", 0, -1.16f, gameObject.transform.parent, true);
            Destroy(gameObject.transform.parent.gameObject);
        }

    }
}
