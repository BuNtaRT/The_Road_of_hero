using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controll : MonoBehaviour
{

    private bool paused = false;



    private void Start()
    {
        UI.singleton.onPaused += onPause;
        onPac(Vault_data.singleton.StartGetPac());
        Vault_data.singleton.Onpac += onPac;

    }

    private bool Pac;
    private void onPac(bool val)
    {
        if (val)
        {
            switchColor(new Color(0.382031f, 0.4601866f, 0.9528302f));
        }
        else
        {
            switchColor(new Color(1,1,1));
        }
    }

    void switchColor(Color SWcolor)
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = SWcolor;
        }
        else
        {
            foreach (Transform temp in transform)
            {
                if (temp.GetComponent<SpriteRenderer>() != null)
                {
                    temp.GetComponent<SpriteRenderer>().color = SWcolor;
                }
            }
        }
    }

    private void onPause(bool pause)
    {
        paused = pause;
    }

    private void OnDestroy()
    {
        UI.singleton.onPaused -= onPause;
        Vault_data.singleton.Onpac -= onPac;
    }

    private void FixedUpdate()
    {
        if (!paused && !disable)
        {
            Vector3 to = new Vector3(transform.localPosition.x - 0.5f, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, to, Time.deltaTime);
        }
    }

    bool disable = false;

    public void DisableMove() 
    {
        disable = true;
    }
}
