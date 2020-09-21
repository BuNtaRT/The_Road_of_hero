using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_game : MonoBehaviour
{
    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
