using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_controller : MonoBehaviour
{

    Vector3 startVector, endVector;
    Vector3 P1, P2;

    public void StartRocket(Vector3 start, Vector3 end) {


        //startVector = start;
        startVector = new Vector3(start.x,start.y,start.z);
        endVector = end;
        P1 = new Vector3(startVector.x - 0.2f, startVector.y + 5.5f, 0);
        P2 = new Vector3(endVector.x - 1, endVector.y + 4, 0);
        StartCoroutine(Lerp_rocket());
        StartCoroutine(Lerp_rocket_rotation());
    }


    IEnumerator Lerp_rocket() {
        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime * 0.8f;
            gameObject.transform.position = Vector3.Lerp(transform.position, cubeBezier3(startVector, P1, P2, endVector, timeStep), timeStep);
            yield return null;
        }
    }
    IEnumerator Lerp_rocket_rotation()
    {
        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime * 1.5f;
            gameObject.transform.eulerAngles = Vector3.Lerp(new Vector3(0,0,66f), new Vector3(0, 0, 0), timeStep);
            yield return null;
        }
    }

    static Vector3 cubeBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return (((-p0 + 3 * (p1 - p2) + p3) * t + (3 * (p0 + p2) - 6 * p1)) * t + 3 * (p1 - p0)) * t + p0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy") {

            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            GameObject.Find("script").GetComponent<effect_Core>().Effect_die(collision.gameObject, "expl");
            GameObject.Find("script").GetComponent<effect_Core>().Create_effect("explosion_enemy",0,1.63f,collision.gameObject.transform);

            GameObject.Find("script").GetComponent<effect_Core>().Effect_die(gameObject, "expl");
        }
    }
}
