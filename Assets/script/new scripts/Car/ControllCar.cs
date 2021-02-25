using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllCar : MonoBehaviour
{
    // Тут происходит обработка свайпов на экране и перемешение машины 

    private void Start()
    {

        //PlayerPrefs.DeleteAll();
        car = GameObject.Find("Car").GetComponent<Transform>().GetChild(0);
        UI.singleton.onPaused += onPaused;
    }

    #region Pause
    private bool pause = true;
    private void onPaused(bool pause)
    {
        this.pause = pause;
    }
    #endregion

    #region EVENTS
    public delegate void LayoutSub(int lay);
    public event LayoutSub OnChangeLayout;

    void ChageLayput(int lay) 
    {
        OnChangeLayout?.Invoke(lay);
    }
    #endregion

    public int GetLine() => mode; 

    Transform car;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 direction;

    bool LockMoved = false;
    int mode = 1;


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
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && !LockMoved)
            {

                endPos = Input.GetTouch(0).position;
                direction = startPos - endPos;
                if (direction.magnitude >= 15)
                {
                    LockMoved = true;
                    if (direction.y >= 0 && mode > 0)
                    {
                        DownCar();
                    }
                    else if (direction.y <= 0 && mode < 2)
                    {
                        UpCar();
                    }
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
                LockMoved = false;
        }
    }

    void UpCar()
    {
        if (mode == 0)
        {
            StartCoroutine(Switch_car_pos(-4.3f, -3.63f));
            car.GetComponent<SpriteRenderer>().sortingOrder = 12;
            ChageLayput(12);
        }
        else if (mode == 1 ) 
        {
            StartCoroutine(Switch_car_pos(-3.63f, -2.95f));
            car.GetComponent<SpriteRenderer>().sortingOrder = 10;
            ChageLayput(10);
        }
        mode++;
        StartCoroutine(Scale_car(car.localScale.x, car.localScale.x - 0.11f));

    }
    void DownCar() 
    {
        if (mode == 1)
        {
            StartCoroutine(Switch_car_pos(-3.63f, -4.3f));
            car.GetComponent<SpriteRenderer>().sortingOrder = 14;
            ChageLayput(14);
        }
        else if (mode == 2)
        {
            StartCoroutine(Switch_car_pos(-2.95f, -3.63f));
            car.GetComponent<SpriteRenderer>().sortingOrder = 12;
            ChageLayput(12);

        }
        mode--;
        StartCoroutine(Scale_car(car.localScale.x, car.localScale.x + 0.11f));

    }



    IEnumerator Switch_car_pos(float p1, float p2)
    {

        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 0.13f;
            car.position = Vector3.Lerp(new Vector3(car.position.x, p1), new Vector3(car.position.x, p2), timeStep);
            yield return null;
        }
    }

    IEnumerator Scale_car(float S1, float S2)
    {

        float timeStep = 0f;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 0.13f;
            car.localScale = Vector3.Lerp(new Vector3(S1, S1, S1), new Vector3(S2, S2, S2), timeStep);
            yield return null;
        }

    }

}
