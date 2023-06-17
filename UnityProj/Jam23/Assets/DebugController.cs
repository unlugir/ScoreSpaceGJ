using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField] Transform planet;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject model;
    public float fuel;
    public float fuelConsumption;
    public bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ResetPlane()
    {
        fuel = 100;
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
    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.StartMenu();
    }
}
