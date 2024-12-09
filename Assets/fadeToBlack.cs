using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Color startColor;
    public Color endColor;

    public float timeToChange;
    public Image img;


    // Start is called before the first frame update
    void Start()
    {
        img.color = startColor;
    }

    public void fade(AudioSource ac)
    {
        StartCoroutine(fadeCo(ac));
    }

    IEnumerator fadeCo(AudioSource ac)
    {
        float maxVolume = ac.volume;
        for (float t = 0; t <= timeToChange; t += Time.deltaTime)
        {
            ac.volume = Mathf.Lerp(maxVolume, 0, t / timeToChange);
            img.color = Color.Lerp(startColor, endColor, t / timeToChange);
            yield return 0;
        }
        img.color = endColor;
        yield return new WaitForSeconds(0.25f);
    }
}
