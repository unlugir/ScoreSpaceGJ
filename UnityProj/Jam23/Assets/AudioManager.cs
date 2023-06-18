using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool enableSound = true;
    public static AudioManager Instance { get; private set; }
    [SerializeField] AudioSource pickSound;
    [SerializeField] AudioSource takeoff;
    [SerializeField] AudioSource clipPlayer;
    private void Awake()
    {
        Instance = this;
    }

    public void PlayTakeOffSound()
    {
        if (!enableSound) return;
        takeoff.Play();
    }
    public void PlayPickSound()
    {
        if (!enableSound) return;
        pickSound.Play();
    }
    public void PlayClip(AudioClip clip)
    {
        clipPlayer.clip = clip;
        clipPlayer.Play();
    }
    
}
