using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Тут описывается автомобиль, его номер, множитель (выше > больше очков)

    public int num_car;

    public event Action OnDie;

    public float lvl;       // коэффицент для умножения счета со стороны машины 

    public bool DevUndie = true;

    [Obsolete]
    void Start()
    {
        Reload_animation();

        //цвет машины от карты
    }

    public void Start_play()
    {
        foreach (Transform chil in transform)
        {
            if (chil.name.Contains("Exhaust"))
                chil.GetComponent<ParticleSystem>().Play();
        }
    }

    [System.Obsolete]
    public void Reload_animation() 
    {
        gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("cars/Player_car" + num_car);
        gameObject.SetActive(false);
        gameObject.SetActive(true);


        foreach (Transform chil in transform)
        {
            if (chil.name.Contains("Exhaust"))
                chil.GetComponent<ParticleSystem>().startColor = GameObject.Find("Scripts").GetComponent<Colors>().color_exthose_car[num_car];
        }

    }


    void Die() {
        //Time.timeScale = 0;
        GameObject.Find("Scripts").GetComponent<Donat>().PLyerDie();
        OnDie?.Invoke();
    }



    public void DieStat1()          // когда мы врезаемся во что то (monster/orda)
    {
        CoreEffect.Create_effect("explosion_car",0,1.4f,gameObject.transform,false, "Die_car");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy" || other.gameObject.tag == "pit" || other.gameObject.tag == "bomb_zone" || other.gameObject.tag =="orda" || other.gameObject.tag == "Boss"  )
        {
                string tag = other.gameObject.tag;
            if (tag.Contains("enemy") || tag.Contains("orda") || other.gameObject.tag == "Boss" )
            {
                if (Vault_data.singleton.StartGetPac())
                {
                    MonstaersDie.DieMonster(other.gameObject, "Rocket_effect_die", "click");
                }
                else
                {
                    if (!DevUndie)
                    {
                        Die();
                        DieStat1();
                    }
                }
            }
            else
            {
                if (!DevUndie)
                {
                    Die();
                }
            }
        }
        if (other.gameObject.tag == "BoxAccept")
        {
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject temp = CoreEffect.Effect_die(other.gameObject.transform, "fix_fail", "saw_effect_die");
            other.gameObject.transform.SetParent(temp.transform.Find("Interact"));
        }
        else if (other.gameObject.tag == "BoxFail")
        {
            Die();
            DieStat1();
        }
    }

}
