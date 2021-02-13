using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOutMaskChildren : MonoBehaviour
{
    void Start()
    {
        foreach (Transform temp in gameObject.transform)
        {
            if (temp.GetComponent<SpriteRenderer>() != null)
            {
                temp.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }
            else 
            {
                foreach (Transform tempInChild in temp)
                {
                    if (temp.GetComponent<SpriteRenderer>() != null)
                    {
                        temp.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                    }
                }
            }
        }
        Destroy(this);
    }

}
