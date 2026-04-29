using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    private Collider spawnArea;

    [System.Serializable]
    public class FruitSpawnData
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance = 0.3f;
    }

    public FruitSpawnData[] objectPrefabs;

    public GameObject bombPrefab;

    [Range(0f, 1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;
    public float minAngleX = -15f;
    public float maxAngleX = 15f;
    public float minAngleZ = -15f;
    public float maxAngleZ = 15f;
    public float minForce = 18f;
    public float maxForce = 22f;
    public float maxLifetime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {

        // Adds a 2 second delay before it starts
        yield return new WaitForSeconds(2f);

        while (enabled)
        {

            GameObject prefab = GetRandomFruit();

            if (Random.value < bombChance)
            {
                prefab = bombPrefab;
            }

            // Defines random spawn positions of objects
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // Rotates spawner randomly on Z axis between the 2 given values
            Quaternion rotation = Quaternion.Euler(Random.Range(minAngleX, maxAngleX), 0f, Random.Range(minAngleZ, maxAngleZ));

            GameObject spawnedObject = Instantiate(prefab, position, rotation);
            Destroy(spawnedObject, maxLifetime);

            // Launches objects up
            float force = Random.Range(minForce, maxForce);
            // Use world up so objects are launched vertically in world space
            spawnedObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);

            // Picks random spawn time between the 2 given values
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
    private GameObject GetRandomFruit()
    {
        float totalChance = 0f;
        foreach (var fruit in objectPrefabs)
        {
            totalChance += fruit.spawnChance;  // Sum up all chances
        }

        float randomPoint = Random.value * totalChance; // Get a random value in range [0, totalChance]
        float cumulative = 0f;

        foreach (var fruit in objectPrefabs)
        {
            cumulative += fruit.spawnChance;
            if (randomPoint <= cumulative)
            {
                return fruit.prefab;  // Pick the fruit when cumulative chance exceeds random value
            }
        }

        return objectPrefabs[0].prefab; // Fallback in case of an issue
    }

}