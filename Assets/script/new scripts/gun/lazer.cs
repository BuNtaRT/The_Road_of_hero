using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagMonster.Monsters.Contains(collision.gameObject.tag))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (collision.gameObject.tag == "enemy")
            {
                float time = CoreEffect.Create_effect("explosion_enemy", 0, 0.75f, collision.gameObject.transform.parent, false);
                CoreEffect.Effect_die(collision.gameObject.transform.parent.gameObject, "expl",time);

            }
            else if (collision.gameObject.tag == "orda")
            {
                float time= CoreEffect.Create_effect("explosion_car", 0, -2.28f, collision.gameObject.transform, false);
                CoreEffect.Create_effect("explosion", 0.06f, -3.89f, collision.gameObject.transform, false);
                CoreEffect.Create_effect("explosion", 0.67f, -2.949f, collision.gameObject.transform, false);
                Destroy(collision.gameObject, time + 0.2f);
            }
        }
    }
}

static class TagMonster
{
    public static List<string> Monsters = new List<string>()
    {
        "enemy",
        "orda"
    };
}
