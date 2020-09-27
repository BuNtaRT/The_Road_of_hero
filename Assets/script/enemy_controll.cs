using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controll : MonoBehaviour
{

    public bool paused = false;

    private void FixedUpdate()
    {
        if (!paused)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x - 1f, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);
        }
    }
}
