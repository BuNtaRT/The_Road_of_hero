﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    #region Singleton
    // Singleton - пока что патерн, который поможет по многу не искать, не создавать экземпляр если нужен именно этот
    public static UI singleton { get; private set; }
    private void Awake()
    {
        singleton = this;   //инициализация на awake (до создания сцены)
    }
    #endregion

    #region Events

    public delegate void Pause(bool pause);     // назначение делегата (делегат - ссылка на методы, может содержать много методов), а ну еще задает сигнатуру принимаемых методов
    public event Pause onPaused;                // наш event по его срабатывании (ниже) все кто подписался на event получат срабатывание подписанных методов
    void SetPause(bool pause)              
    {
        onPaused?.Invoke(pause);    // всем кто в делегате, летит значение pause (?. - если они есть)
    }

    #endregion



    private void Start()
    {
        PlayerPrefs.DeleteAll();    ///////////////////////////////////////////////////////////////////////// убрать!!!!!!!!

        B_Audio(Convert.ToBoolean(PlayerPrefs.GetInt("Music")));
        car = GameObject.Find("Car").transform;                                                 // ссылка на трансформ обьекта car для позиции и как следствие score
        lvl_car = GameObject.FindGameObjectWithTag("Player").GetComponent<Car>().lvl;           // получаем уровень машины

    }


    public GameObject B_SoundOn;
    public GameObject B_SoundOff;

    public void B_Audio(bool Audio) {

        GameObject.Find("Scripts").GetComponent<Music>().On_Off(Audio);

        if (Audio)
        {
            B_SoundOff.SetActive(false);
            B_SoundOn.SetActive(true);
        }
        else {
            B_SoundOff.SetActive(true);
            B_SoundOn.SetActive(false);
            CoreAudio.DestroyAll();
        }
        PlayerPrefs.SetInt("Music", Convert.ToInt32(Audio));
        PlayerPrefs.Save();
    }

    public void B_StartGame(GameObject gameObject) {
        SetPause(false);
        gameObject.GetComponent<Animator>().enabled = true;
        GameObject.Find("Car").transform.GetChild(0).GetComponent<Car>().Start_play();
        Destroy(gameObject,0.4f);
    }

    public void B_Pause(bool pause) {
        SetPause(pause);
    }

    #region Score
    float speed = 1.5f;         // скорость анимаций 
    float lvl_car;
    int score = 0;
    public Text score_text;
    Transform car;
    bool scale_on = false;
    bool rotat_on = false;
    float steap;

    string ScoreText() {

        score = (int)(car.position.x * lvl_car);

        if (!scale_on && score >= lvl_car * 150)        // включаем увеличение текста
        {      
            scale_on = true;
            steap = lvl_car * 155;
            StartCoroutine(Scale_score());              // но только по разу
        }
        if (!rotat_on && score >= lvl_car * 250)        // вклдючаем повороты текста 
        {
            rotat_on = true;
            speed += 0.05f;
            StartCoroutine(Rotation_score());
        }

        if (scale_on && steap <= score && speed >= 0.3)
        {
            steap =  score + ((speed / score)+100f);
            speed -= 0.1f;
            Debug.Log("steap = "+ steap + "  speed  =" + speed);

        }

        return score.ToString()+"m";
        
    }

    IEnumerator Rotation_score()
    {
        Vector3 start = new Vector3(0f, 0f, 6.8f);
        Vector3 end = new Vector3(0f, 0f, -7.6f);

        while (true)
        {
            for (float time = 0; time < (speed+0.2f) * 2; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, speed+0.2f) / (speed+0.2f);
                score_text.transform.localEulerAngles = Vector3.Lerp(start, end, progress);
                yield return null;
            }
        }
    }

    IEnumerator Scale_score() {

        Vector3 start = new Vector3(0.86936f, 0.86936f, 0.86936f);
        Vector3 end = new Vector3(1.151f, 1.151f, 1.151f);

        while (true)
        {
            for (float time = 0; time < speed * 2; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, speed) / speed;
                score_text.transform.localScale = Vector3.Lerp(start, end, progress);
                yield return null;
            }
        }
    }

    #endregion


    void Update()
    {
        score_text.text = ScoreText();
    }
}
