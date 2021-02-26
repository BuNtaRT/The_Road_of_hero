using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        this.pause = pause;
        onPaused?.Invoke(pause);    // всем кто в делегате, летит значение pause (?. - если они есть)
    }
    public bool pause;
    #endregion

    public Transform CarContent, MapContent;
    public Image Ico_car, Ico_Map;
    public Text Price_car_text, Price_map_text;
    public Text Coin_men;

    private void Start()
    {
        //PlayerPrefs.SetInt("money", 1000000);
        //PlayerPrefs.DeleteAll();

        GameObject.Find("Scripts").GetComponent<Music>().On_Off(Convert.ToBoolean(PlayerPrefs.GetInt("Music")));


        Vault_data.singleton.Initialized_Car(CarContent);             // инициализируем машины для меню покупки и вообще 
        Vault_data.singleton.Initialized_Map(MapContent);             // инициализируем карты для меню покупки и вообще 

        Price_car_text.text = Vault_data.singleton.GetCarCurPrice().ToString();
        Price_map_text.text = Vault_data.singleton.GetMapCurPrice().ToString();
        Set_ico_car();

        CarShoot.singleton.OnAmmo += OnAmmo;

        SetPause(true);

        B_Audio(Convert.ToBoolean(PlayerPrefs.GetInt("Music")));
        car = GameObject.Find("Car").transform;                                                 // ссылка на трансформ обьекта car для позиции и как следствие score
        lvl_car = GameObject.FindGameObjectWithTag("Player").GetComponent<Car>().lvl + PlayerPrefs.GetFloat("Cur_map_lvl");

        CheckButtonBuy();
        Coin_men.text = Money_maneger.GetMoney().ToString();
        CoinInmarket.text = Money_maneger.GetMoney().ToString();    
        Money_maneger.ResetTempMoney();

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
        CoreAudio.Create_audio_eff("play");
        SetPause(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Car>().OnDie += DieCar;
        gameObject.GetComponent<Animator>().enabled = true;
        GameObject.Find("Car").transform.GetChild(0).GetComponent<Car>().Start_play();
        Destroy(gameObject, 0.4f);
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

    string ScoreText()
    {
        score = (int)(car.position.x * lvl_car);

        if (!scale_on && score >= lvl_car * 350)        // включаем увеличение текста
        {
            scale_on = true;
            steap = lvl_car * 155;
            StartCoroutine(Scale_score());              // но только по разу
        }
        if (!rotat_on && score >= lvl_car * 550)        // вклдючаем повороты текста 
        {
            rotat_on = true;
            speed += 0.05f;
            StartCoroutine(Rotation_score());
        }

        if (scale_on && steap <= score && speed >= 0.3)
        {
            steap = score + ((speed / score) + 200f);
            speed -= 0.1f;

        }

        return score.ToString() + "m";

    }

    IEnumerator Rotation_score()
    {
        Vector3 start = new Vector3(0f, 0f, 6.8f);
        Vector3 end = new Vector3(0f, 0f, -7.6f);

        while (true)
        {
            for (float time = 0; time < (speed + 0.2f) * 2; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, speed + 0.2f) / (speed + 0.2f);
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


    #region In Game

    public Text AmmoText;

    public void B_Pause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            CoreAudio.Create_audio_eff("pause");
        }
        else
        {
            Time.timeScale = 1;

            CoreAudio.Create_audio_eff("unpause");
        }
        SetPause(pause);
    }


    private void OnAmmo(int ammo)
    {
        AmmoText.text = ammo.ToString();
    }

    #endregion



    #region market

    public GameObject ItemOpen;
    public GameObject LightMap;
    public Text CoinInmarket;

    public void CheckButtonBuy()
    {
        if (Vault_data.singleton.CheckCar()) 
        {
            Price_car_text.text = "ALL";
            Price_car_text.color = new Color(0.3465709f, 0.8490566f, 0.1722143f);

            Price_car_text.transform.parent.parent.Find("icoBox").GetComponent<Image>().color = new Color(0.4366488f, 0.6698113f, 0.2495995f);
            Price_car_text.transform.parent.parent.Find("icoBox").GetChild(0).GetComponent<Text>().enabled = false; 

        }
        if (Vault_data.singleton.CheckMap()) {
            Price_map_text.text = "ALL";
            Price_map_text.color = new Color(0.3465709f, 0.8490566f, 0.1722143f);

            Price_map_text.transform.parent.parent.Find("icoBox").GetComponent<Image>().color = new Color(0.4366488f, 0.6698113f, 0.2495995f);
            Price_map_text.transform.parent.parent.Find("icoBox").GetChild(0).GetComponent<Text>().enabled = false; 
        }
    }

    public void Comlite()
    {
        CoinInmarket.text = Money_maneger.GetMoney().ToString();
        Coin_men.text = Money_maneger.GetMoney().ToString();
        Vault_data.singleton.Constr_car();
        Set_ico_car();
        Set_ico_map();
        Vault_data.singleton.Pic_map(PlayerPrefs.GetInt("Cur_map").ToString());
        Coin_men.text = Money_maneger.GetMoney().ToString();
        CheckButtonBuy();
    }
    //////////////////////////////  CAR
    public void Buy_car()
    {
        if (!Price_car_text.text.Contains("ALL"))
        {
            int price = Convert.ToInt32(Price_car_text.text);
            if (Money_maneger.Minus_Money(price))
            {
                int soult_car = Vault_data.singleton.GetCar();
                ItemOpen.SetActive(true);
                ItemOpen.transform.Find("GameObject").transform.Find("car_ico").GetComponent<Image>().sprite = Resources.Load<Sprite>("cars/Sprite/" + soult_car + "/frame0");
                Vault_data.singleton.Buy_car(soult_car);
                Price_car_text.text = Vault_data.singleton.GetCarCurPrice().ToString();
            }
        }
    }

    public void Set_ico_car()
    {
        Ico_car.sprite = Resources.Load<Sprite>("cars/Sprite/" + PlayerPrefs.GetInt("Cur_car") + "/frame0");
    }

    //////////////////////////////  MAP

    public void Buy_map()
    {
        if (!Price_map_text.text.Contains("ALL"))
        {
            int price = Convert.ToInt32(Price_map_text.text);
            if (Money_maneger.Minus_Money(price))
            {
                int map = Vault_data.singleton.GetMap();
                ItemOpen.SetActive(true);
                ItemOpen.transform.Find("GameObject").transform.Find("car_ico").GetComponent<Image>().sprite = Resources.Load<Sprite>("map/ico/" + map);
                Vault_data.singleton.Buy_Map(map);
                Price_map_text.text = Vault_data.singleton.GetMapCurPrice().ToString();
            }
        }
    }

    public void Set_ico_map()
    {
        Ico_Map.sprite = Resources.Load<Sprite>("map/ico/" + PlayerPrefs.GetInt("Cur_map"));
        LightMap.SetActive(Vault_data.singleton.LightOnMap(PlayerPrefs.GetInt("Cur_map")));
    }

    #endregion


    #region End Game
    public Text MoneyEND,ShootCollectedCoinsEND,ScoreEND,RecordEND;

    private void DieCar()
    {
        SetPause(true);
        GameObject.Find("In_game_ui").GetComponent<Animator>().Play("LoseGameState1");
        StateOneEndGame();
    }

    public void StateOneEndGame() 
    {
        string forShoot, forCollect,newRekord,allcoin;
        if (PlayerPrefs.GetInt("lg") == 0)
        {
            forShoot = "For liquidation ";
            forCollect = "Collected coins ";
            newRekord = "New Record!";
            allcoin = "Total coins +";
        }
        else 
        {
            forShoot = "За устранение ";
            forCollect = "Собрано монет ";
            newRekord = "Новый рекорд!";
            allcoin = "Всего монет +";

        }

        Money_maneger.temp_money = (int)(car.position.x / 2) + Money_maneger.money_monster ;
        MoneyEND.text = allcoin + (Money_maneger.temp_money + Money_maneger.money_coll).ToString();
        ShootCollectedCoinsEND.text = forShoot + " +" + Money_maneger.money_monster + "\n" + forCollect + " +" + Money_maneger.money_coll;


        ScoreEND.text = score.ToString() + "m";

        if (score > PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("score", score);
            RecordEND.text = newRekord + "\n" + score.ToString() + "m";
        }
        else
        {
            RecordEND.text = PlayerPrefs.GetInt("score").ToString() + "m";
        }
    }

    public void End() 
    {
        Money_maneger.SaveEndGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void reburnCar()
    {
        SetPause(false);

        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("prep"))
        {
            Destroy(temp);
        }
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("pit"))
        {
            Destroy(temp);
        }

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        CoreGenerate.GenerateObj("bonus/Sheld", 0, 0, player);
        SetPause(false);
        GameObject.Find("In_game_ui").GetComponent<Animator>().enabled = false;
        GameObject.Find("In_game_ui").GetComponent<Animator>().Play("show_in_game_ui");
        GameObject.Find("In_game_ui").GetComponent<Animator>().enabled = true;

    }

    #endregion


    void Update()
    {
        if (!pause)
        {
            score_text.text = ScoreText();
        }
    }
}
