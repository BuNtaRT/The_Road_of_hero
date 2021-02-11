using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevCanvas : MonoBehaviour
{
    public Text buildNum;
    public Text Fps;
    void Start()
    {
        buildNum.text = "Build " + Application.version;

        QualitySettings.vSyncCount = 1;
    }

    int countUpdate = 0;
    int TempFps = 0;
    void Update()
    {
        countUpdate++;
        TempFps += (int)(1f / Time.unscaledDeltaTime);
        if (countUpdate >= 30)
        {
            FpsMetr();
        }

    }


    void FpsMetr()
    {
        Fps.text = (TempFps / 30).ToString();
        TempFps = 0;
        countUpdate = 0;
    }
}
