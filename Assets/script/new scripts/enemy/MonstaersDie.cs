using UnityEngine;

static class MonstaersDie
{
    //тута все смерти для всех препятствий
    public static void DieMonster(GameObject Monster, string effect,string sound)
    {
        Monster.GetComponent<BoxCollider2D>().enabled = false;
        CoreAudio.Create_audio_eff(sound);
        UnityEngine.Object.Instantiate(Resources.Load("effect/Die/" + effect),Monster.transform.parent);
        if (Monster.transform.parent.gameObject.GetComponent<SpriteRenderer>() != null)
        {
            Monster.transform.parent.gameObject.AddComponent<die>();
        }

        // тут можно в логику если надо !!
        //{
    
        //}
    }

}
