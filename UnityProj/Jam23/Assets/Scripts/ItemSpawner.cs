using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawn;
    [SerializeField] private SphereCollider sphereCollider;

    public void Start()
    {
        for (int inex = 0; inex < 10; inex++)
        {
            SpawnItemInRandomPoint(spawn);
        }
    }

    public void SpawnItemInRandomPoint(GameObject item)
    {
        Vector3 randomPoint = Random.onUnitSphere * sphereCollider.radius * (sphereCollider.gameObject.transform.localScale.x + 1.2f);
            
        var spawnedItem = Instantiate(item, randomPoint, Quaternion.identity);
        spawnedItem.transform.LookAt(sphereCollider.transform.position);
    }
}