using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrail : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Gradient randomGradient = CreateRandomGradient();
        GetComponent<TrailRenderer>().colorGradient = randomGradient;
    }
    
    private Gradient CreateRandomGradient()
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];

        // Generate random color keys
        for (int i = 0; i < 2; i++)
        {
            colorKeys[i].color = Random.ColorHSV();
            colorKeys[i].time = i;
        }

        // Generate random alpha keys
        for (int i = 0; i < 2; i++)
        {
            alphaKeys[i].alpha = Random.Range(0f, 1f);
            alphaKeys[i].time = i;
        }

        gradient.colorKeys = colorKeys;
        gradient.alphaKeys = alphaKeys;

        return gradient;
    }
}
