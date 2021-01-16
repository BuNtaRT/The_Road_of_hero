using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_game : MonoBehaviour
{

    public Text rocket;
    public Text coinT;

    public Text paused_text;
    public Text paused_any_touch;

    public GameObject in_game_ui;

    public Animator menu_UI_animator;

    bool lg = false;

    public GameObject sound_off_button,sound_on_button;

    string paused_text_local = "";
    string any_touch = "";

    int coin;

    public bool sound = true;

    private void Start()
    {
        //lg = System.Convert.ToBoolean(PlayerPrefs.GetInt("lg"));
        if (lg)
        {
            paused_text_local = "Paused";
            any_touch = "Tap to continue";
        }
        else {
            paused_text_local = "Приостановлено";
            any_touch = "Коснитесь, чтобы продолжить";
        }

        //sound = System.Convert.ToBoolean(PlayerPrefs.GetInt("sound"));
        OffOnAudio(sound);

    }

    public void OffOnAudio(bool soundNow) {

        GameObject.Find("script").GetComponent<AudioCore>().on = soundNow;
        GameObject.Find("script").GetComponent<MusicManeger>().On(soundNow);

        if (!soundNow)
        {
            sound_off_button.SetActive(true);
            sound_on_button.SetActive(false);
            GameObject.Find("script").GetComponent<AudioCore>().DestroyAll();
        }
        else {
            sound_off_button.SetActive(false);
            sound_on_button.SetActive(true);
        }
        sound = soundNow;

    }

    public void Play() {

        GameObject.Find("Car").transform.GetChild(0).Find("Exhaust").GetComponent<ParticleSystem>().Play();
        GameObject.Find("Car").GetComponent<Car_controller>().enabled = true;
        //GameObject.Find("script").GetComponent<controll>()._die = false;
        menu_UI_animator.enabled = true;
        menu_UI_animator.Play("hide_ui_menu");
        in_game_ui.SetActive(true);
        in_game_ui.GetComponent<Animator>().enabled = true;
        //Camera.main.GetComponent<lat_controller>().enabled = true;
        StartCoroutine(Destroy_menu());
    }

    public void Die() {
        in_game_ui.GetComponent<Animator>().Play("in_game_ui_lose");
    }

    public void Go_State2() {
        in_game_ui.GetComponent<Animator>().Play("in_game_ui_state2");

    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Destroy_menu() {
        yield return new WaitForSeconds(1f);
        //Destroy(menu_UI_animator.gameObject);
    }


    Coroutine text_P;
    public void Pause()
    {
        in_game_ui.GetComponent<Animator>().Play("Paused");
        paused_any_touch.text = any_touch;
        text_P = StartCoroutine(Text_pause_anim());
        GameObject.Find("Car").GetComponent<Car_controller>().pause = true;
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy_interactive");

        foreach (GameObject temp in enemy) {
            temp.GetComponent<enemy_controll>().paused = true;
            temp.GetComponent<Animator>().enabled = false;
        }

        Camera.main.GetComponent<lat_controller>()._paused = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    public void Unpause() {
        in_game_ui.GetComponent<Animator>().Play("show_in_game_ui");
        StopCoroutine(text_P);
        GameObject.Find("Car").GetComponent<Car_controller>().pause = false;
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("enemy_interactive");
        Camera.main.GetComponent<lat_controller>()._paused = false;

        foreach (GameObject temp in enemy)
        {
            temp.GetComponent<enemy_controll>().paused = false;
            temp.GetComponent<Animator>().enabled = true;
        }

    }

    IEnumerator Text_pause_anim() {
        while (true) {
            paused_text.text = paused_text_local;
            yield return new WaitForSeconds(0.3f);
            paused_text.text = paused_text_local + ".";
            yield return new WaitForSeconds(0.3f);
            paused_text.text = paused_text_local + "..";
            yield return new WaitForSeconds(0.3f);
            paused_text.text = paused_text_local + "...";
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void Rocket(int count) {
        rocket.text = count.ToString();
    }

    public void Coin(int count) {
        coin += count;
        coinT.text = coin.ToString();
    }

    public void Shot() {
        GameObject.Find("script").GetComponent<car_event>().Car_Shot();
    }
}
