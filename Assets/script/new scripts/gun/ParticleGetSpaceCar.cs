using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGetSpaceCar : MonoBehaviour
{
    // Делает Particle только вертикальными не подвеженными горизонтальным перемещению
    void Start()
    {
        var part = gameObject.GetComponent<ParticleSystem>().main;
        part.simulationSpace = ParticleSystemSimulationSpace.Custom;
        part.customSimulationSpace = GameObject.FindGameObjectWithTag("CarP").transform;
    }

}
