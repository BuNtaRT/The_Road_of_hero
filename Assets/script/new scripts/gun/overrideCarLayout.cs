using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overrideCarLayout : MonoBehaviour
{
    public int custom = 0;
    public bool subscribe = false;
    public bool overParticle = false;
    public bool overSprite = false;

    void Start()
    {
        int startLayout = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sortingOrder;
        ChLayout(startLayout);
        if (subscribe) 
            GameObject.FindGameObjectWithTag("Scripts").GetComponent<ControllCar>().OnChangeLayout += ChLayout;
    }

    void ChLayout(int val) 
    {
        if (overParticle)
            gameObject.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = val + custom;

        if (overSprite)
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = val + custom;
    }

    private void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("Scripts") != null)
        {
            GameObject.FindGameObjectWithTag("Scripts").GetComponent<ControllCar>().OnChangeLayout -= ChLayout;
        }
    }

}
