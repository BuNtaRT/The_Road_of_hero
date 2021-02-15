using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    public Material lineMaterial;
    public Gradient gradien;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnergySphere")
        {
            GoDraw(collision.transform.parent.transform.Find("EnergySphere"));
        }
    }

    void GoDraw(Transform sphere) 
    {
        Vector3 start = sphere.position, end = gameObject.transform.position;

        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();

        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.colorGradient = gradien;
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
        lr.SetPosition(0, new Vector3(start.x+1f,start.y,start.z)) ;
        lr.SetPosition(1, end);
        lr.sortingOrder = 20;
        Destroy(myLine, 0.5f);
        Destroy(this);
    }
}
