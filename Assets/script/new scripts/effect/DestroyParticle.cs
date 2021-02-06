using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.SetParent(null);
        gameObject.GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject, 2f);
    }


}
