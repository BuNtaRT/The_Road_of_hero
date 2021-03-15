
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controll : MonoBehaviour
{

    private bool paused = false;
    public bool undestroy = false;

    bool MoveMonsterModern = false;
    bool DieMonsterModern = false;
    Transform cameraT = null;

    private void Start()
    {
        if (Random.Range(0, 101) >= 0 && gameObject.name.Contains("enemy1"))
        {
            if (Random.Range(0, 101) >= 50)
            {
                MoveMonsterModern = true;
                switchColor(new Color(0.8113208f, 0.4080425f, 0.2717159f));
                cameraT = Camera.main.transform;
            }
            else
            {
                DieMonsterModern = true;
                switchColor(new Color(0.5801887f, 0.7120162f,1f));

            }
        }
        else
        {
            onPac(Vault_data.singleton.StartGetPac());
            Vault_data.singleton.Onpac += onPac;
        }

        UI.singleton.onPaused += onPause;

    }

    IEnumerator moveTimer() 
    {
        //yield return new WaitForSeconds(Random.Range(0.3f, 1.3f));
        List<int> AvalibleLine = new List<int>();
        float[,] Ypos = { { -1.91f,10 },
                          {-2.553f,12 },
                          {-3.249f,14 } };
        int line = 0;
        switch (gameObject.GetComponent<SpriteRenderer>().sortingOrder) 
        {
            case 10:
                line = 0;
                AvalibleLine.Add(1);
                break;
            case 12:
                line = 1;
                AvalibleLine.Add(0);
                AvalibleLine.Add(2);
                break;
            case 14:
                line = 2;
                AvalibleLine.Add(1);
                break;
        }

        Debug.Log(AvalibleLine[0]);
        int newline = AvalibleLine[Random.Range(0,AvalibleLine.Count)];

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)Ypos[newline, 1];

        float timeStep = 0f;
        float startY = Ypos[line,0], endY = Ypos[newline,0];
        Vector3 StartV, EndV;
        StartV = new Vector3(transform.localPosition.x, startY, transform.localPosition.z);
        EndV = new Vector3(transform.localPosition.x, endY, transform.localPosition.z);
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 0.5f;
            gameObject.transform.localPosition = Vector3.Lerp(StartV, EndV, timeStep);
            yield return null;
        }

    }


    private bool Pac;
    private void onPac(bool val)
    {
        if (!undestroy)
        {
            if (val)
            {
                switchColor(new Color(0.382031f, 0.4601866f, 0.9528302f));
            }
            else
            {
                switchColor(new Color(1, 1, 1));
            }
        }
    }

    void switchColor(Color SWcolor)
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = SWcolor;
        }
        else
        {
            foreach (Transform temp in transform)
            {
                if (temp.GetComponent<SpriteRenderer>() != null)
                {
                    temp.GetComponent<SpriteRenderer>().color = SWcolor;
                }
            }
        }
    }

    private void onPause(bool pause)
    {
        paused = pause;
    }

    public void DieEnemy() 
    {
        StopAllCoroutines();
        if (DieMonsterModern)
        {
            GameObject temp = CoreGenerate.GenerateObj("lat/SpiritEnemy", 15, 0, transform, true);
            temp.GetComponent<spiritcontroll>().GetSkin(gameObject.GetComponent<SpriteRenderer>().sprite, gameObject.GetComponent<SpriteRenderer>().sortingOrder);
        }
    }

    private void OnDestroy()
    {
        UI.singleton.onPaused -= onPause;
        Vault_data.singleton.Onpac -= onPac;
    }

    private void FixedUpdate()
    {
        if (!paused && !disable)
        {
            Vector3 to = new Vector3(transform.localPosition.x - 0.5f, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, to, Time.deltaTime);
        }
        if (MoveMonsterModern) 
        {
            if (cameraT.position.x >= gameObject.transform.position.x - 8) 
            {
                StartCoroutine(moveTimer());
                MoveMonsterModern = false;
            }
        }
    }

    bool disable = false;

    public void DisableMove() 
    {
        disable = true;
    }
}
