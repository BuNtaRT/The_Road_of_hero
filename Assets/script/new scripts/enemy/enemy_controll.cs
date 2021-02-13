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
    }

    private void onPause(bool pause)
    {
        paused = pause;
    }

    private void OnDestroy()
    {
        UI.singleton.onPaused -= onPause;
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
