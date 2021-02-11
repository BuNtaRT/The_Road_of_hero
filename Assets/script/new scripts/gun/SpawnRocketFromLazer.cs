using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocketFromLazer : MonoBehaviour
{
    public GameObject obj;
    private void Start()
    {
        StartCoroutine(Fire());
    }

    void OnDestroy() 
    {
        StopAllCoroutines();
    }

    IEnumerator Fire()
    {
        while (true)
        {
            Instantiate(obj, transform.position, new Quaternion());
            yield return new WaitForSeconds(0.3f);
        }
    }
}
