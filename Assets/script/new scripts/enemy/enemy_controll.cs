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

    public void Unsub() {
        UI.singleton.onPaused -= onPause;
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x - 1f, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);
        }
    }
}
