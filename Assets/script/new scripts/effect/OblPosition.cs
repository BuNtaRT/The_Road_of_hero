using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OblPosition : MonoBehaviour
{
    public bool Repeate = false;
    public float x = 0f, y = 0f, z = 0f;
    public float Speed = 0.5f;

    void Start()
    {
        StartCoroutine(TransformP());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    Vector3 StartPos, EndPos;

    IEnumerator TransformP()
    {
        StartPos = gameObject.transform.localPosition;
        EndPos = gameObject.transform.localPosition;
        EndPos = new Vector3(StartPos.x +x, StartPos.y + y, StartPos.z+z);

        do
        {
            for (float time = 0; time < Speed * 3; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, Speed) / (Speed);
                gameObject.transform.localPosition = Vector3.Lerp(StartPos, EndPos, progress);
                yield return null;
            }
            for (float time = 0; time < Speed * 3; time += Time.deltaTime)
            {
                float progress = Mathf.PingPong(time, Speed) / (Speed);
                gameObject.transform.localPosition = Vector3.Lerp(EndPos, StartPos, progress);
                yield return null;
            }
        } while (Repeate);

    }

}
