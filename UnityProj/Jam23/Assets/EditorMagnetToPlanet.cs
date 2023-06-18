using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EditorMagnetToPlanet : MonoBehaviour
{
    [SerializeField] Transform planet;
    [SerializeField] bool magnetToPlanet;
    [SerializeField] bool raycastToPlanet;

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

    }

#endif
}
