using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public bool enableSound = true;
    public static AudioManager Instance { get; private set; }
    [SerializeField] AudioSource pickSound;
    [SerializeField] AudioSource takeoff;
    [SerializeField] AudioSource explosion;
    [SerializeField] AudioSource clipPlayer;
    [SerializeField] UnityEngine.UI.Slider volumeSlider;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        volumeSlider.onValueChanged.AddListener((v) =>
        {
            AudioListener.volume = v;
        });
    }

    public void PlayTakeOffSound()
    {
        if (!enableSound) return;
        takeoff.Play();
    }
    public void PlayExplosion()
    {
        explosion.Play();
    }
    public void PlayPickSound()
    {
        if (!enableSound) return;
        pickSound.Play();
    }
    public void PlayClip(Sound sound)
    {
        clipPlayer.clip = sound.clip;
        clipPlayer.volume = sound.volume;
        clipPlayer.Play();
    }
    
}
