using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour
{
    public GameObject set;
    private void Start()
    {
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
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

}
