using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

public class ObjPosition : MonoBehaviour
{
    public bool Repeate = false;
    public float x = 0f, y = 0f, z = 0f;
    public float Speed = 0.5f;
    Coroutine cor;

    public void SetParametr(bool Repeate,float x,float y,float z, float Speed ) 
    {
        this.Repeate = Repeate;
        this.x = x;
        this.y = y;
        this.z = z;
        this.Speed = Speed;
    }

    void Start()
    {
        TransformObj trojb = gameObject.AddComponent<TransformObj>();
        cor = trojb.TransformLocalPosition(gameObject.transform.localPosition, new Vector3(x, y, z), Speed, Repeate);
        if (!Repeate) 
        {
            Destroy(this,Speed+0.5f);
        }
    }


    private void OnDestroy()
    {
        if (Repeate)
            StopCoroutine(cor);
    }

}
