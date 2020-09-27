using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_controller : MonoBehaviour
{

    Vector3 startVector, endVector;
    //Vector3 P1, P2;

    public void StartRocket() {

        startVector = transform.position;
        endVector = new Vector3(transform.position.x + 50,transform.position.y);
        //P1 = new Vector3(startVector.x - 0.2f, startVector.y + 5.5f, 0);
        //P2 = new Vector3(endVector.x - 1, endVector.y + 4, 0);
        StartCoroutine(Lerp_rocket());
        //StartCoroutine(Lerp_rocket_rotation());
    }


    IEnumerator Lerp_rocket() {
        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 2f;
            gameObject.transform.position = Vector3.Lerp(startVector, endVector /*cubeBezier3(startVector, P1, P2, endVector, timeStep)*/, timeStep);
            yield return null;
        }
        Destroy(gameObject);
    }
    //IEnumerator Lerp_rocket_rotation()
    //{
    //    float timeStep = 0f;
    //    while (timeStep < 1.0f)
    //    {
    //        timeStep += Time.deltaTime / 0.5f;
    //        gameObject.transform.eulerAngles = Vector3.Lerp(new Vector3(0,0,66f), new Vector3(0, 0, -10f), timeStep);
    //        yield return null;
    //    }
    //}

    //static Vector3 cubeBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    //{
    //    return (((-p0 + 3 * (p1 - p2) + p3) * t + (3 * (p0 + p2) - 6 * p1)) * t + 3 * (p1 - p0)) * t + p0;
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "enemy") {

            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            GameObject.Find("script").GetComponent<effect_Core>().Effect_die(collision.gameObject.transform.parent.gameObject, "expl");
            GameObject.Find("script").GetComponent<effect_Core>().Create_effect("explosion_enemy",0,1.63f,collision.gameObject.transform.parent);
            GameObject.Find("script").GetComponent<effect_Core>().Create_effect("Blod", -0.04f, -0.43f, collision.gameObject.transform.parent,true);

            //GameObject.Find("script").GetComponent<effect_Core>().Effect_die(gameObject, "expl");

            gameObject.transform.Find("Particle System").GetComponent<ParticleSystem>().Stop();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("script").GetComponent<AudioCore>().Create_audio_eff("expl");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject,3);
        }
    }
}
