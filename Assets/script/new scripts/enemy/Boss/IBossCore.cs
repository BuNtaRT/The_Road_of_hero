using System.Collections;
using UnityEngine;

interface IBossCore
{
    IEnumerator AnimDie();

    void minusHP();

    void init(Transform carP, ControllCar car, float[] PosYEnemy, float[] PosYBoss, string namemonster, BoxCollider2D BossColider, BossShow bossShow);
    void initHP(int tempHp, int hp, int hpSteap);
}
