using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StartAndDieEffForGun
{
    // звуки и эффекты смерти
    static string[,] Sound_and_eff_dieSound = new string[,]
    {
        {"rocket_gun","Rocket_effect_die","expl"},
        {"lazer_gun","Lazer_effect_die","lazer_die"},
        {"Napalm_gun","fire_effect_die","FlameDie"},
        {"BugTostar","bugtoStar_effect_die","FlameDie"},
        {"GoToStar","GoToStar_effect_die","FlameDie"}
    };
    public static Tuple<string,string,string> Get_weap_content(string name)
    {
        for (int i = 0;i <= Sound_and_eff_dieSound.Length/3 - 1;i++) 
        {
            if (Sound_and_eff_dieSound[i, 0] == name) 
            {
                return Tuple.Create(Sound_and_eff_dieSound[i, 0], Sound_and_eff_dieSound[i,1], Sound_and_eff_dieSound[i, 2]);
            }
        }

        return Tuple.Create("", "","");
    }
}
