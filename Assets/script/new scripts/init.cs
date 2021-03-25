using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour
{
    public GameObject set;
    public GameObject undestroyObj;
    private void Start()
    {
        DontDestroyOnLoad(undestroyObj);
        if (PlayerPrefs.GetInt("First") == 1)
        {
            PlayerPrefs.SetInt("Open", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(1);
        }
        else
        {
            set.SetActive(true);
        }
    }

    public void lang(int lg) 
    {
        PlayerPrefs.SetInt("lg", lg);
        PlayerPrefs.SetInt("Open", 1);
        PlayerPrefs.SetFloat("Cur_map_lvl", 1);
        PlayerPrefs.SetInt("sound",1);
        PlayerPrefs.SetInt("music", 1);
        if(Random.Range(0,101)>=50)
            PlayerPrefs.SetInt("Boss1", 228);
        else
            PlayerPrefs.SetInt("Boss2", 228);

        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

}
