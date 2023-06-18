using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCountriesSpawnPoints : MonoBehaviour
{
    [SerializeField] bool create;
    private void OnValidate()
    {
        if (create)
        {
            create = false;
            var coutries = FindObjectsOfType<Country>();
            foreach (var cou in coutries)
            {
                if(cou.stampPosition != null)
                {
                    DestroyImmediate(cou.stampPosition.gameObject);
                }
                var stampPos = new GameObject();
                stampPos.transform.parent = cou.transform;
                stampPos.transform.position = cou.transform.position + cou.transform.up - cou.transform.forward;
                cou.stampPosition = stampPos.transform;
            }
        }
    }
}
