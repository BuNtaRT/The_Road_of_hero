using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class road_fix_controller : MonoBehaviour
{
    bool _pit_collision = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pit")
        {
            _pit_collision = true;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.transform.Find("mask").gameObject.SetActive(false);
            GameObject.Find("script").GetComponent<AudioCore>().Create_audio_eff("fix_comp");

        }

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "road" && !_pit_collision)
        {
            StartCoroutine(Deat_road_fix());
        }
    }
    
    IEnumerator Deat_road_fix() {

        yield return new WaitForSeconds(0.25f);

        if (!_pit_collision)
        {
            GameObject.Find("script").GetComponent<effect_Core>().Effect_die(gameObject, "fix_fall");
        }
    }

}
