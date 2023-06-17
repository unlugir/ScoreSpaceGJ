using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coherence;
using Cinemachine;
using Coherence.Toolkit;

public class DebugController : MonoBehaviour
{
    Transform planet;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject model;
    CoherenceSync sync;
    public Transform cameraFollow; 
    public float fuel;
    public float fuelConsumption;
    public bool isAlive;

    void Start()
    {
        if (planet == null)
            planet = GameObject.Find("Planet").transform;
        sync = GetComponent<CoherenceSync>();
    }

    [Command]
    public void ResetPlane()
    {
        fuel = 100;
        isAlive = true;
        explosion.gameObject.SetActive(false);
        model.gameObject.SetActive(true);
    }
    [Command]
    public void KillPlane()
    {
        fuel = 0;
        isAlive = false;
        explosion.gameObject.SetActive(true);
        model.gameObject.SetActive(false);
        explosion.Play();
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
        fuel -= fuelConsumption * Time.deltaTime;
        if (fuel <= 0)
        {
            sync.SendCommand(typeof(DebugController), nameof(KillPlane), MessageTarget.All);
        }
    }
    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.StartMenu();
    }
}
