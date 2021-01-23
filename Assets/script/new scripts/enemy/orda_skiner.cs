using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orda_skiner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform temp in transform) {
            temp.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("enemy_controll/enemy_controll" + Vault_data.singleton.GetRandomMonster()) ;
        }
        dell();
    }

    void dell() {
        Destroy(this);
    }
}
