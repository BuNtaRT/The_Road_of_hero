using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Lens : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LensC());
    }

    float speed = 0.1f;

    IEnumerator LensC()
    {
        LensDistortion Lens = null;
        Camera.main.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out Lens);
        
        

        for (float time = 0; time < (speed) * 3; time += Time.deltaTime)
        {
            float progress = time / speed;
            Lens.intensity.value = Mathf.Lerp(0, -50, progress);
            yield return null;
        }

        for (float time = 0; time < (speed) * 3; time += Time.deltaTime)
        {
            float progress = time / speed;
            Lens.intensity.value = Mathf.Lerp(-50, 0, progress);
            yield return null;
        }



    }
}
