using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField] Transform planet;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject model;
    
    public float fuel;
    public float startFuel;
    public float fuelConsumption;
    public bool isAlive;
    
    
    void Start()
    {
    }

    public void ResetPlane()
    {
        fuel = startFuel;
        isAlive = true;
        explosion.gameObject.SetActive(false);
        model.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;
        fuel -= fuelConsumption * Time.deltaTime;
        if (fuel <= 0)
        {
            fuel = 0;
            isAlive = false;
            explosion.gameObject.SetActive(true);
            model.gameObject.SetActive(false);
            explosion.Play();
            StartCoroutine(DeathCoroutine());
            return;
        }       
        var hor = Input.GetAxis("Horizontal");

        if (hor != 0)
        {
            transform.Rotate(new Vector3(0, hor * rotateSpeed * Time.deltaTime, 0));
        }

        transform.RotateAround(planet.transform.position ,this.transform.right, speed * Time.deltaTime);
        //transform.LookAt(planet.transform, -this.transform.up);
    }

    public void OnTriggerEnter(Collider collider)
    {
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
    }
}
