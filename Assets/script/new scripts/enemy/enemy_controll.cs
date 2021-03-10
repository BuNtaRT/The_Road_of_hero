
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controll : MonoBehaviour
{

    private bool paused = false;
    public bool undestroy = false;

    bool MoveMonsterModern = false;

    private void Start()
    {
        if (Random.Range(0, 101) >= 0 && gameObject.name.Contains("enemy1"))
        {
            MoveMonsterModern = true;
            switchColor(new Color(0.8113208f, 0.4080425f, 0.2717159f));
            StartCoroutine(moveTimer(Random.Range(2f,4f)));
        }
        else
        {
            onPac(Vault_data.singleton.StartGetPac());
            Vault_data.singleton.Onpac += onPac;
        }

        UI.singleton.onPaused += onPause;

    }

    IEnumerator moveTimer(float wait) 
    {
        float[,] Ypos = { { -1.91f,10 },
                          {-2.553f,12 },
                          {-3.249f,14 } };
        int line = 0;
        yield return new WaitForSeconds(wait);
        switch (gameObject.GetComponent<SpriteRenderer>().sortingOrder) 
        {
            case 10:
                line = 0;
                break;
            case 12:
                line = 1;
                break;
            case 14:
                line = 2;
                break;
        }
        int newline = -1;
        do {
            newline = Random.Range(0, 3);
        } while (line == newline);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)Ypos[newline, 1];

        float timeStep = 0f;
        float startY = Ypos[line,0], endY = Ypos[newline,0];
        Vector3 StartV, EndV;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / 1f;
            StartV = new Vector3(transform.localPosition.x, startY, transform.localPosition.z);
            EndV = new Vector3(transform.localPosition.x, endY, transform.localPosition.z);
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
    }

    bool disable = false;

    public void DisableMove() 
    {
        disable = true;
    }
}
