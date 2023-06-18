using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    public string contryName;
    public bool hasItem;
    public Transform stampPosition;
    [SerializeField] Sound[] clips;

    private void Start()
    {
        if (stampPosition == null)
            stampPosition = this.transform;
    }
    private void OnDrawGizmos()
    {
        if (stampPosition == null) return;
        Gizmos.DrawSphere(stampPosition.position, 1f);
    }
    public Sound GetRandomClip()
    {
        if (clips == null || clips.Length == 0) return null;
        return clips[Random.Range(0, clips.Length)];
    }
}
