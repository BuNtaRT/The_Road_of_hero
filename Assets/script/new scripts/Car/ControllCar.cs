using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllCar : MonoBehaviour
{
    private void Start()
    {
        car = GameObject.Find("Car").GetComponent<Transform>().GetChild(0);
        Menu_UI.singleton.onPaused += onPaused;
    }

    #region Pause
    private bool pause = true;
    private void onPaused(bool pause)
    {
        this.pause = pause;
    }
    #endregion


    Transform car;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;

    bool mode = false;

    void Update()
    {
        //тут обработка Touch для передвижения 
        if (Input.touchCount > 0 && !pause)
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
        }
    }

    void Up_Down_car()
    {
        if (mode)       // down
        {
            StartCoroutine(Switch_car_pos(-3.288148f, -4.092148f));
            StartCoroutine(Scale_car(1f, 1.11f));
            car.GetComponent<SpriteRenderer>().sortingOrder = 14;
        }
        else
        {          // up
            StartCoroutine(Switch_car_pos(-4.092148f, -3.288148f));
            StartCoroutine(Scale_car(1.11f, 1f));
            car.GetComponent<SpriteRenderer>().sortingOrder = 10;


        }
    }


    IEnumerator Switch_car_pos(float p1, float p2)
    {

        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 0.2f;
            car.position = Vector3.Lerp(new Vector3(car.position.x, p1), new Vector3(car.position.x, p2), timeStep);
            yield return null;
        }
    }

    IEnumerator Scale_car(float S1, float S2)
    {

        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 0.2f;
            car.localScale = Vector3.Lerp(new Vector3(S1, S1, S1), new Vector3(S2, S2, S2), timeStep);
            yield return null;
        }

    }

}
