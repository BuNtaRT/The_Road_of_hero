using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class road_fix : MonoBehaviour
{
    public Transform fix;
    public Transform my_rope;

    public void drop() {
        GameObject.Find("script").GetComponent<AudioCore>().Create_audio_eff("fix_fall");

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        fix.SetParent(null);
        fix.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        my_rope.GetComponent<Animator>().SetBool("end", true);
    }
}
