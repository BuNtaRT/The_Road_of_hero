using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossShow : MonoBehaviour
{
    public Image bg, main;
    public GameObject bar;
    public int allhp;
    float one = 0;

    public void SetColor(Color bgHp,Color mainHp) 
    {
        one = (float)1/allhp;
        bg.color = bgHp;
        main.color = mainHp;
        bar.SetActive(true);
        StartCoroutine(wait());
        //main.fillAmount = 1;
    }
    IEnumerator wait() 
    {
        yield return new WaitForSeconds(1.35f);
        bar.GetComponent<Animator>().enabled = false;
    }

    public void MInusHp() 
    {
        Debug.Log("MInusHp");
        Debug.Log("FillAmount before = " + main.fillAmount);
        Debug.Log("one  = " + one);

        main.fillAmount -= one;
        Debug.Log("FillAmount = " + main.fillAmount);
    }

    public void Hide() 
    {
        bar.GetComponent<Animator>().enabled = true;
        main.fillAmount = 1;
        bar.SetActive(false);
    }
}
