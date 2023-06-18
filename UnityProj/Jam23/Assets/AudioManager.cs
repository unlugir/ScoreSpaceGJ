using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool enableSound = true;
    public static AudioManager Instance { get; private set; }
    [SerializeField] AudioSource pickSound;
    private void Awake()
    {
        Instance = this;
    }

    public void PlayPickSound()
    {
        if (!enableSound) return;
        pickSound.Play();
    }
    
}
