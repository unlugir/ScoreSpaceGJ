using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "Sound", menuName = "Sound")]
public class Sound : ScriptableObject
{
    public AudioClip clip;
    public float volume;
}
