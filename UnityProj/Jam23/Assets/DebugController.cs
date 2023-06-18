using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Coherence;
using Cinemachine;
using Coherence.Toolkit;
using Coherence.UI;

public class DebugController : MonoBehaviour
{
    [Sync]
    [Tooltip("Gets loaded with `Coherence.UI.NetworkDialog.PlayerName` (from the owner of this Entity).")]
    public string playerName;

    Transform planet;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject model;
    CoherenceSync sync;
    public Transform cameraFollow; 
    public float fuel;
    public float startFuel;
    public float fuelConsumption;
    public bool isAlive;

    void Start()
    {
        if (planet == null)
            planet = GameObject.Find("Planet").transform;
        sync = GetComponent<CoherenceSync>();
        if (sync != null && sync.IsMyClientConnection)
        {
            playerName = NetworkDialog.PlayerName;
        }
    }

    [Command]
    public void ResetPlane()
    {
        fuel = startFuel;
        fuelConsumption = 1;
        isAlive = true;
        explosion.gameObject.SetActive(false);
        model.gameObject.SetActive(true);
    }
    [Command]
    public void KillPlane()
    {
        fuel = 0;
        fuelConsumption = 1;
        isAlive = false;
        explosion.gameObject.SetActive(true);
        model.gameObject.SetActive(false);
        explosion.Play();
        if(sync != null && sync.IsMyClientConnection)
            StartCoroutine(DeathCoroutine());
    }

    void Update()
    {
        if (!isAlive) return;

        var hor = Input.GetAxis("Horizontal");
        if (hor != 0)
        {
            transform.Rotate(new Vector3(0, hor * rotateSpeed * Time.deltaTime, 0));
        }

        transform.RotateAround(planet.transform.position ,this.transform.right, speed * Time.deltaTime);
        UpdateFuel();
    }
    void UpdateFuel()
    {
        fuelConsumption += 0.05f * Time.deltaTime;
        fuel -= fuelConsumption * Time.deltaTime;
        if (fuel <= 0)
        {
            sync.SendCommand(typeof(DebugController), nameof(KillPlane), MessageTarget.All);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (sync == null || !sync.IsMyClientConnection) return;
        Debug.Log(collider.gameObject.name);
        if (collider.TryGetComponent<Item>(out var collectedItem))
        {
            collectedItem.OnItemPickedUp(this);
            collider.gameObject.transform.DOScale(Vector3.zero, 0.2f).OnComplete(
                ()=> Destroy(collider.gameObject));
        }
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.StartMenu();
        GameManager.Instance.SaveScore();
    }
}
