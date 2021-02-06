using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGetSpaceCar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var part = gameObject.GetComponent<ParticleSystem>().main;
        part.simulationSpace = ParticleSystemSimulationSpace.Custom;
        part.customSimulationSpace = GameObject.FindGameObjectWithTag("CarP").transform;
    }

}
