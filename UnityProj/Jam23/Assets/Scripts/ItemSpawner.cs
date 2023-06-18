using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Item petrol;
    [SerializeField] private StampItem stamp;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private List<Country> countries;
    [SerializeField] int petrolCount;
    [SerializeField] int contriesCount;

    public void Start()
    {
        for (int inex = 0; inex < petrolCount; inex++)
        {
            SpawnItemInRandomPoint(petrol.gameObject);
        }
        for(int i = 0; i < contriesCount; i++)
        {
            SpawnItemInRandomCountry();
        }
    }

    public void OnContryItemPickUp(Country country)
    {
        AudioManager.Instance.PlayClip(country.GetRandomClip());
        SpawnItemInRandomCountry();
    }
    public void OnPetrolPickUp()
    {
        SpawnItemInRandomPoint(petrol.gameObject);
    }

    public void SpawnItemInRandomPoint(GameObject item)
    {
        Vector3 randomPoint = Random.onUnitSphere * sphereCollider.radius * (sphereCollider.gameObject.transform.localScale.x + 1.3f);
            
        var spawnedItem = Instantiate(item, randomPoint, Quaternion.identity, gameObject.transform);
        spawnedItem.transform.LookAt(sphereCollider.transform.position,Vector3.right);
    }
    public void SpawnItemInRandomCountry()
    {
        var spawnableContries = countries.Where(c => c.hasItem == false).ToList();
        var country = countries[Random.Range(0, spawnableContries.Count - 1)];
        var spawnedItem = Instantiate(stamp, country.transform.position, Quaternion.identity, gameObject.transform);

        spawnedItem.transform.LookAt(sphereCollider.transform.position, Vector3.right);
        stamp.country = country;
        country.hasItem = true;
    }
}
