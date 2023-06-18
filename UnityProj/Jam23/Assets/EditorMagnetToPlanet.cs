using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EditorMagnetToPlanet : MonoBehaviour
{
    [SerializeField] Transform planet;
    [SerializeField] bool magnetToPlanet;
    [SerializeField] bool raycastToPlanet;
    [Space]
    [SerializeField] bool setOnDistanceToPlanet;
    [SerializeField] float distanceToPlanet = 26;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (magnetToPlanet)
        {
            magnetToPlanet = false;
            transform.LookAt(planet);
        }
        if (raycastToPlanet)
        {
            raycastToPlanet = false;

            if (planet == null)
                planet = GameObject.Find("Planet").transform;

            var ray = new Ray(SceneView.lastActiveSceneView.camera.transform.position,
                SceneView.lastActiveSceneView.camera.transform.forward);
            
            if (Physics.Raycast(ray, out var hit, 100))
            {
                transform.position = hit.point;
                transform.LookAt(planet, -transform.up);
            }
        }
        if (setOnDistanceToPlanet)
        {
            setOnDistanceToPlanet = false;
            var diff = transform.position - planet.transform.position;
            Debug.Log(diff.sqrMagnitude);
            Debug.Log(diff.sqrMagnitude);
            transform.position += diff;
            transform.LookAt(planet);
        }

    }

#endif
}
