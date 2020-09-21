using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lat_controller : Generate
{

    // этот скрипт висит на камере и по прошествию растояния запрашивает генерацию чего либо, а так же решает что будет созданно

    float last_let = 0;
    

    int chance = 0; 
    int min_let_distance = 10;

    void Start()
    {
        InvokeRepeating("What_now", 0f, 1f);
    }

    void What_now() {

            
        int rand = Random.Range(0, 101);
        if (rand >= chance && transform.position.x >= (min_let_distance + last_let))
        {
            Lat_generate();
        }
        else {
            
        }
    }

    void Lat_generate() {


        pit_generate();

    }

    void pit_generate() {

        generate("lat/pit1", transform.position.x + 13, -3.75f);

        //generate("lat/pit", transform.position.x + 13, -3.75f);
        last_let = transform.position.x + 13;
    }

    void Update()
    {


        //if (nowX <= transform.position.x) {
        //    What_now();
        //}
    }
}
