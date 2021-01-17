using System;
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
        PlayerPrefs.DeleteAll();
        B_Audio(Convert.ToBoolean(PlayerPrefs.GetInt("Music")));
        car = GameObject.Find("Car").transform;
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
        GameObject.Find("Car").GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
        Destroy(gameObject,0.4f);
    }

    public void B_Pause(bool pause) {
        SetPause(pause);
    }

    #region Score
    int score = 0;
    public Text score_text;
    Transform car;
    string ScoreText() {
        score = (int)car.position.x;
        return score.ToString();
    }
    #endregion


    void Update()
    {
        score_text.text = ScoreText();
    }
}
