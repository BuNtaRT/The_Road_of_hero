using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformObj : MonoBehaviour
{
    delegate void Transf(Vector3 start,Vector3 end,float progress);

    public Coroutine TransformLocalRotation(Vector3 StartV, Vector3 EndV, float Speed, bool repeat)
    {
        Transf tr = TransfRotation;
        return StartCoroutine(TransformP(StartV, EndV, Speed, repeat, tr));
    }

    public Coroutine TransformLocalPosition(Vector3 StartV,Vector3 EndV,float Speed, bool repeat) 
    {
        Transf tr = TransfPosition;
        return StartCoroutine(TransformP(StartV, EndV, Speed, repeat, tr));
    }


    IEnumerator TransformP(Vector3 StartV, Vector3 EndV, float Speed, bool repeat,Transf transformLepr)
    {
        Vector3 StartPos, EndPos;
        StartPos = StartV;
        EndPos = new Vector3(StartPos.x + EndV.x, StartPos.y + EndV.y, StartPos.z + EndV.z);

        do
        {
            for (float time = 0; time < Speed * 3; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, Speed) / (Speed);
                transformLepr(StartPos, EndPos, progress);
                yield return null;
            }
            for (float time = 0; time < Speed * 3; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, Speed) / (Speed);
                transformLepr(EndPos, StartPos, progress);
                yield return null;
            }
        } while (repeat);
    }

    void TransfPosition(Vector3 start, Vector3 end, float progress)
    {
        gameObject.transform.localPosition = Vector3.Lerp(start, end, progress);
    }
    void TransfRotation(Vector3 start, Vector3 end, float progress)
    {
        gameObject.transform.localEulerAngles = Vector3.Lerp(start, end, progress);
    }
}
