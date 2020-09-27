using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controll : MonoBehaviour
{
    private Vector2 startPos;                                                  
    private Vector2 endPos;                                                     
    private Vector2 direction;

    public Transform curret_car;

    public float hard = 1;

    public bool _die = true;
    bool mode = false;

    void Update()
    {
        if (Input.touchCount > 0 && !_die)
        {
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startPos = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {

                endPos = Input.GetTouch(0).position;
                direction = startPos - endPos;
                if (direction.magnitude >= 15)
                {
                    if (direction.y >= 0 && !mode)
                    {
                        mode = true;
                        Up_Down_car();
                    }
                    else if (direction.y <= 0 && mode)
                    {
                        mode = false;
                        Up_Down_car();
                    }
                }


            }
            
            //if (ray.collider && ray.collider.tag == "enemy_interactive" && ray.collider && direction.magnitude <= 15)
            //{
            //    ray.collider.GetComponent<BoxCollider2D>().enabled = false;
            //    GameObject.Find("script").GetComponent<car_event>().Car_Shot(ray.collider.gameObject);
            //}

        }

    }

    void Up_Down_car() {
        if (mode)       // down
        {
            StartCoroutine(Switch_car_pos(-3.288148f, -4.092148f));
            StartCoroutine(Scale_car(1f, 1.11f));
            curret_car.GetComponent<SpriteRenderer>().sortingOrder = 14;
        }
        else {          // up
            StartCoroutine(Switch_car_pos(-4.092148f, -3.288148f));
            StartCoroutine(Scale_car(1.11f, 1f));
            curret_car.GetComponent<SpriteRenderer>().sortingOrder = 10;


        }
    }

    IEnumerator Switch_car_pos(float p1, float p2)
    {

        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 0.2f;
            curret_car.position = Vector3.Lerp(new Vector3(curret_car.position.x,p1), new Vector3(curret_car.position.x, p2), timeStep);
            yield return null;
        }
    }

    IEnumerator Scale_car(float S1,float S2) {

        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 0.2f;
            curret_car.localScale = Vector3.Lerp(new Vector3(S1, S1,S1), new Vector3(S2, S2, S2), timeStep);
            yield return null;
        }

    }
}
