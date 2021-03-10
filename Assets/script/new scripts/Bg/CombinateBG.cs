using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinateBG : MonoBehaviour
{
    Transform car;
    float NextCall = 30;
    public Transform lightGroupRoad;
    Music mus;

    #region Event 
    public delegate void StopLat(bool val);
    public event StopLat OnStopLat;

    void stopLat(bool val)
    {
        OnStopLat?.Invoke(val);
    }
    #endregion


    void Start()
    {
        mus=  GameObject.FindGameObjectWithTag("Scripts").GetComponent<Music>();
        NextCall = Random.Range(350, 600);
        //NextCall = 0; // УБРАТЬ
        car = GameObject.Find("Car").transform;
        if (PlayerPrefs.GetFloat("Cur_map_lvl") > 1.1f)
        {
            InvokeRepeating("CombinCheck", 5, 5);
        }
        PlayerPrefs.SetInt("Boss1", PlayerPrefs.GetInt("Boss1") + 1);
        PlayerPrefs.SetInt("Boss2", PlayerPrefs.GetInt("Boss2") + 1);
        PlayerPrefs.SetInt("Boss3", PlayerPrefs.GetInt("Boss3") + 1);
    }


    void CombinCheck() 
    {
        if (car.position.x >= NextCall && !bossFightOne)
        {
            NextCall = (NextCall * 2.5f)+100;
            StartCoroutine(StartEventChangeBg());
        }
    }

    public BossChoise BossC;

    bool bossFightOne = false;
    public void BossEND() 
    {
        NextCall = (car.position.x * 1.5f) + 100;
        mus.SwitchToBoss(false);

        StartCoroutine(StartEventChangeBg());
    }


    IEnumerator StartEventChangeBg() 
    {
        bool light = lightGroupRoad.gameObject.activeSelf;
        lightGroupRoad.gameObject.SetActive(!light);

        GameObject bg_Ivent = Instantiate(Resources.Load<GameObject>("event/EventPortal"),Camera.main.transform);
        bg_Ivent.transform.localPosition = new Vector3(0, 0, 10);
        bg_Ivent.GetComponent<Animator>().Play("StartEvent");

        stopLat(true);
        yield return new WaitForSeconds(5f);
        lightGroupRoad.gameObject.SetActive(light);
        yield return new WaitForSeconds(0.2f);
        lightGroupRoad.gameObject.SetActive(!light);
        yield return new WaitForSeconds(0.2f);
        lightGroupRoad.gameObject.SetActive(light);
        yield return new WaitForSeconds(0.2f);
        lightGroupRoad.gameObject.SetActive(!light);
        yield return new WaitForSeconds(0.2f);
        lightGroupRoad.gameObject.SetActive(light); 

        SpriteRenderer sp = bg_Ivent.transform.Find("Flash").GetComponent<SpriteRenderer>();
        sp.color = new Color(0.574f, 0.574f, 0.574f,1);

        if (Boss() && !bossFightOne)
        {
            mus.SwitchToBoss(true);
            bossFightOne = true;
            Debug.Log("BOSS EVENT SUKA num = " + boss);
            SetNewBg(10);
            yield return new WaitForSeconds(0.2f);
            sp.color = new Color(0.574f, 0.574f, 0.574f, 0);
            BossC.BossFightStart(boss);
        }
        else
        {
            SetNewBg(-1);
            yield return new WaitForSeconds(0.2f);
            sp.color = new Color(0.574f, 0.574f, 0.574f, 0);
            stopLat(false);
        }

        yield return new WaitForSeconds(5f);
        bg_Ivent.GetComponent<Animator>().Play("EndEvent");
        yield return new WaitForSeconds(1f);
        Destroy(bg_Ivent);

    }

    void SetNewBg(int LoadMap) 
    {
        int num = 0;
        if (LoadMap != -1)
        {
            num = LoadMap;
        }
        else
        {
            num = Vault_data.singleton.GetBuyedMap(System.Convert.ToInt32(GameObject.FindGameObjectWithTag("Background").name));
        }
        Destroy(GameObject.FindGameObjectWithTag("Background"));
        GameObject map = Instantiate(Resources.Load<GameObject>("map/"+num));
        map.name = num.ToString();
        Vault_data.singleton.CreateMontersList(num);
        lightGroupRoad.gameObject.SetActive(Vault_data.singleton.LightOnMap(num));
    }



    int boss = 1;   // если выбирается бос то вот его номер 
    bool bbbb = false;
    bool Boss() 
    {
        //if (!bbbb)
        //{
        //    bbbb = true;
        //    boss = 1;
        //    return true;
        //}
        if (Random.Range(0, 201) >= 150)
        {
            if (PlayerPrefs.GetInt("Boss1") >= 8)
            {
                boss = 1;
                return true;
            }
            else if (PlayerPrefs.GetInt("Boss2") >= 10)
            {
                boss = 2;
                return true;
            }
            else if (PlayerPrefs.GetInt("Boss3") >= 13)
            {
                boss = 3;
                return true;
            }
            return false;
        }
        else 
        {
            return false;
        }
    }


}
