using UnityEngine;

static class MonstaersDie
{
    //тута все смерти для всех препятствий
    public static void DieMonster(GameObject Monster, string effect,string sound)
    {
        Monster.GetComponent<BoxCollider2D>().enabled = false;

        if (Monster.transform.parent.gameObject.GetComponent<SpriteRenderer>() != null)
        {
            Monster.transform.parent.gameObject.AddComponent<die>();
        }
        else if (Monster.tag == "orda")
        {
            foreach (Transform temp in Monster.transform.parent) 
            {
                if (temp.tag != "orda") 
                {
                    temp.gameObject.AddComponent<die>();
                }
            }
        }


        if (Monster.transform.parent.GetComponent<enemy_controll>() != null)
            Monster.transform.parent.GetComponent<enemy_controll>().DisableMove();



        Transform EffectDie = CoreEffect.Effect_die(Monster.transform.parent, sound, effect).transform;             //создаем эффект и запихиваем обьект в него

        if (EffectDie.Find("Interact") != null)                                 // Interact - это опциональная часть анимации уничтожения (сжатие обьекта,расширение, перемещение)
            Monster.transform.parent.SetParent(EffectDie.Find("Interact"));
        else
            Monster.transform.parent.SetParent(EffectDie);


        // тут можно в логику если надо !!
        //{
    
        //}
    }

}
