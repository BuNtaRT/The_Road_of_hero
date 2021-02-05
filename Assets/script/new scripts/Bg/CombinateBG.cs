using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinateBG : MonoBehaviour
{
    Transform car;
    float NextCall = 30;
    public Transform lightGroupRoad;

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
        NextCall = Random.Range(350, 600);
        car = GameObject.Find("Car").transform;
        if (PlayerPrefs.GetFloat("Cur_map_lvl") > 1.1f)
        {
            InvokeRepeating("CombinCheck", 10, 5);
        }
    }


    void CombinCheck() 
    {
        if (car.position.x >= NextCall)
        {

            NextCall = (NextCall * 2.5f)+100;
            StartCoroutine(StartEventChangeBg());
        }


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

        SetNewBg();
        yield return new WaitForSeconds(0.2f);
        sp.color = new Color(0.574f, 0.574f, 0.574f, 0);

        stopLat(false);

        yield return new WaitForSeconds(5f);
        bg_Ivent.GetComponent<Animator>().Play("EndEvent");
        yield return new WaitForSeconds(1f);
        Destroy(bg_Ivent);

    }

    void SetNewBg() 
    {
        int num = Vault_data.singleton.GetBuyedMap(System.Convert.ToInt32(GameObject.FindGameObjectWithTag("Background").name));
        Destroy(GameObject.FindGameObjectWithTag("Background"));
        GameObject map = Instantiate(Resources.Load<GameObject>("map/"+num));
        map.name = num.ToString();
        Vault_data.singleton.CreateMontersList(num);
        lightGroupRoad.gameObject.SetActive(Vault_data.singleton.LightOnMap(num));
    }




}
