using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Tooltip("The object to spawn.")]
    public GameObject objectToSpawn;
    [Tooltip("The minimum time between spawns.")]
    public float minSpawnTime = 100f;
    [Tooltip("The maximum time between spawns.")]
    public float maxSpawnTime = 100f;
    [Tooltip("The range of the spawn point in the x-axis.")]
    public float xRange = 2f;
    [Tooltip("The range of the spawn point in the z-axis.")]
    public float zRange = 2f;

    private float nextSpawnTime;

    private void Start()
    {
        // Set the initial spawn time.
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void Update()
    {
        // Check if it's time to spawn.
        if (Time.time >= nextSpawnTime)
        {
            // Generate a random spawn point.
            Vector3 spawnPoint = new Vector3(Random.Range(-xRange, xRange), transform.position.y, Random.Range(-zRange, zRange));

            // Spawn the object.
            Instantiate(objectToSpawn, this.transform.localPosition+spawnPoint, Quaternion.identity);

            // Set the next spawn time.
            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
    }
}
