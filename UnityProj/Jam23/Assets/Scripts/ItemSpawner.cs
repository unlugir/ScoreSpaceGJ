using UnityEngine;
using System.Collections.Generic;
using  System.Collections;
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
    [SerializeField] private PostCollector _postCollector;
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
            SpawnItemInRandomCountryExcept(null);
        }
    }

    public void OnContryItemPickUp(Country country)
    {
        _postCollector.ShowAnimation(country.GetRandomPost());
        AudioManager.Instance.PlayClip(country.GetRandomClip());
        StartCoroutine(Delay(1, ()=> SpawnItemInRandomCountryExcept(country)));
    }
    IEnumerator Delay(float time, System.Action action)
    {
        yield return new WaitForSeconds(time + Random.Range(0, 2));
        action?.Invoke();
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
    public void SpawnItemInRandomCountryExcept(Country c)
    {
        var spawnableContries = countries.Where(c => c.hasItem == false).ToList();
        if (c != null && spawnableContries.Contains(c))
            spawnableContries.Remove(c);
        var country = spawnableContries[Random.Range(0, spawnableContries.Count)];
        country.hasItem = true;
        var spawnedItem = Instantiate(stamp, country.stampPosition.position, Quaternion.identity, gameObject.transform);
        spawnedItem.transform.LookAt(sphereCollider.transform.position, Vector3.right);
        spawnedItem.country = country;
        
    }
}
