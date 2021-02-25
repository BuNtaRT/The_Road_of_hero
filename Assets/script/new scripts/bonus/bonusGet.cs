using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusGet : MonoBehaviour
{
    public string name;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            StartCoroutine(lerpDownA(transform.Find("Bonus").GetComponent<SpriteRenderer>()));
            Action(collision.transform);
        }

    }


    IEnumerator lerpDownA(SpriteRenderer sp)
    {
        Color Startcolor = sp.color;
        Color Endcolor = new Color(Startcolor.r, Startcolor.g, Startcolor.b, 0f);

        float speed = 0.5f;

        for (int i = 0; i < 1; i++)
        {
            for (float time = 0; time < (speed) * 3; time += Time.deltaTime)
            {
                float progress = time / speed;
                sp.color = Color.Lerp(Startcolor, Endcolor, progress);
                yield return null;
            }
        }
        Destroy(sp);
    }


    void Action(Transform player) 
    {
        gameObject.GetComponent<dell_obj>().CancelInvoke();
        gameObject.GetComponent<dell_obj>().enabled = false;
        if (name.Contains("BonusPac"))
        {
            StartCoroutine(Pac_bonus());
        }
        else if (name.Contains("BonusR"))
        {
            StartCoroutine(Rocket());
        }
        else if (name.Contains("BonusC"))
        {
            StartCoroutine(Coin());
        }
        else if (name.Contains("BonusS")) 
        {
            CoreGenerate.GenerateObj("bonus/Sheld", 0, 0, player);
            Destroy(gameObject, 0.5f);
        }
    }

    IEnumerator Coin()
    {
        int index = PlayerPrefs.GetInt("Car_index");
        int rand = Random.Range((int)0.2f*index,1*index);
        if (rand <= 5) 
        {
            rand = Random.Range(3, 7);
        }
        gameObject.transform.Find("Bonus/number").GetComponent<number_show>().NumGet(rand);
        Money_maneger.Plus_collect(rand);
        gameObject.AddComponent<ObjPosition>().SetParametr(false, 0, 1.5f, 0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject, 0.5f);
    }

    IEnumerator Rocket() 
    {
        GameObject.FindGameObjectWithTag("Scripts").GetComponent<CarShoot>().PlusAmmo();
        gameObject.transform.Find("Bonus/number").GetComponent<number_show>().NumGet(1);
        gameObject.AddComponent<ObjPosition>().SetParametr(false, 0, 1.5f, 0, 0.3f);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject, 0.5f);
    }

    IEnumerator Pac_bonus() 
    {
        CoreAudio.Create_audio_eff("pac");
        Vault_data.singleton.PacOnSet(true);
        yield return new WaitForSeconds(5f);
        Vault_data.singleton.PacOnSet(false);
        Destroy(gameObject, 0.5f);
    }
}
