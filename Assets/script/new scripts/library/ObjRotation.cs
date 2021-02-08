using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotation : MonoBehaviour
{
    public bool Repeate = false;
    public float x = 0f, y = 0f, z = 0f;
    public float Speed = 0.5f;
    Coroutine cor;

    void Start()
    {
        gameObject.AddComponent<TransformObj>();
        cor = gameObject.GetComponent<TransformObj>().TransformLocalRotation(gameObject.transform.localEulerAngles, new Vector3(x, y, z), Speed, Repeate);
    }

    private void OnDestroy()
    {
        StopCoroutine(cor);
    }
}
