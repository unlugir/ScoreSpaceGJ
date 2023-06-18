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
        // Generate random color keys
        for (int i = 0; i < 2; i++)
        {
            colorKeys[i].color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.7f, 1f);
            colorKeys[i].color = new Color(colorKeys[i].color.r,
                colorKeys[i].color.g, colorKeys[i].color.b, 1);
        }

        // Generate random alpha keys

        gradient.colorKeys = colorKeys;
        gradient.alphaKeys[1].alpha = 0.25f;
        gradient.alphaKeys[0].alpha = 0.85f;

        return gradient;
    }
}
