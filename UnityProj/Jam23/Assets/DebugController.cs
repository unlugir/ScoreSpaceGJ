using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField] Transform planet;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var hor = Input.GetAxis("Horizontal");

        if (hor != 0)
        {
            transform.Rotate(new Vector3(0, 0, -hor * rotateSpeed * Time.deltaTime));
        }

        transform.RotateAround(planet.transform.position ,this.transform.right, speed * Time.deltaTime);
        
    }
}
