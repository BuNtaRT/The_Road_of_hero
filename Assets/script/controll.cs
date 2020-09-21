using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controll : MonoBehaviour
{
    Collider2D last_enemy = null;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("touch");

            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (ray.collider && ray.collider.tag == "inter_zone")
            {
                Debug.Log("ray_collider");
                ray.collider.GetComponent<road_fix>().drop();
            }
            else if(ray.collider && ray.collider.tag == "enemy" && ray.collider != last_enemy) {
                last_enemy = ray.collider;
                GameObject.Find("script").GetComponent<car_event>().Car_Shot(ray.collider.gameObject);
            }
        }
    }





}
