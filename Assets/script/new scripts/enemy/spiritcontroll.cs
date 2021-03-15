using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiritcontroll : MonoBehaviour
{
    public SpriteRenderer spriteRender;
    public void GetSkin(Sprite sp,int lay)
    {
        spriteRender.sprite = sp;
        spriteRender.sortingOrder = lay;
    }
    private void Start()
    {
        Debug.Log("SpiritCreate");
        if(Camera.main.transform.position.x <= transform.position.x-5)
        CoreAudio.Create_audio_eff("spirit");
    }
    private void FixedUpdate()
    {
        Vector3 to = new Vector3(transform.localPosition.x - 2f, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, to, Time.deltaTime);
    }
}
