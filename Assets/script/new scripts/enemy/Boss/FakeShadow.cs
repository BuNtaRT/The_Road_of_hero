using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeShadow : MonoBehaviour
{
    public BossDamage BD;

    private void OnDestroy()
    {
        BD.Damage();
        CoreEffect.Effect_die(transform, "ShadowCarGun", "Shadow_effect_die");
    }
}
