using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StartAndDieEffForGun
{
    // Тут перечисления оружия, по имени запрашивается оружие а возвращает звук старта, эффект смери и звук смерти от оружия 
    // звуки и эффекты смерти
    static string[,] Sound_and_eff_dieSound = new string[,]
    {
        {"rocket_gun","Rocket_effect_die","expl"},
        {"lazer_gun","Lazer_effect_die","lazer_die"},
        {"Napalm_gun","fire_effect_die","FlameDie"},
        {"BugTostar","bugtoStar_effect_die","FlameDie"},        //смерть
        {"GoToStar","GoToStar_effect_die","FlameDie"},          //смерть
        {"saw_gun","saw_effect_die","fail"},                   
        {"unicorn_gun","unicorn_effect_die","unicorn_die"},
        {"whell_gun","wheel_effect_die","fireball"},             
        {"fireball","fireball_effect_die","fireball"},             
        {"FreezLazer","frezee_effect_die","frezeeDie"},
        {"ShadowCarGun","Shadow_effect_die","ShadowCarGun_die"},
        {"lazerTobig","LazerToBig_effect_die","lazerTobig_die"},   
        {"GuitarGun","GuitarGun_effect_die","GuitarGun_die"},   
        {"SharkGun","Shark_effect_die","shark_die"},   
        {"lazerToSmal","lazerToSmal_effect_die","lazerToSmal_die"},   
        {"MegafonGun","MegafonGun_effect_die","ScreamNO"},   
        {"EnergySphereGun","SphereEnergy_effect_die","EnergySphereDie"},   
        {"VoronkaGun","Voronka_effect_die","EnergySphereDie"},   
        {"SpaceReqestGun","SpaceReqest_effect_die","SpaceReqestGun_die"},
        {"SirenaGun","Sirena_effect_die","fail"},
        {"GuitarBus","Bus_effect_die","GuitarBusDie"},
        {"HzRocket","frezee_effect_die","frezeeDie"},                
        {"FireCarGun","FireCar_effect_die","DieWhater"},
        {"HotDogCar","HotDogCar_effect_die","HotdogDie"},
        {"PoliceCar","Sirena_effect_die","HotdogDie"},
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
