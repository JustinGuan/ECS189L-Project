using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefab;
    private LocationTracker tracker;
    // How far the enemies should spawn around the player.
    private float maxRadius = 10.0f;
    // Holds info about the size of our play area.
    private MeshRenderer meshRenderer;
    // List to hold our spawners.
    private List<GameObject> spawnPoints;

    void Awake()
    {
        tracker = GetComponent<LocationTracker>();
        meshRenderer = GameObject.Find("Floor").GetComponent<MeshRenderer>();
    }

    void Start()
    {
        spawnPoints = new List<GameObject>();
        CreateSpawners();
    }

    // Creates the where the center of our spawnpoints will be at.
    void CreateSpawners()
    {
        float degreesOfSpawn = (2.0f * Mathf.PI) / 5.0f;
        // This gets 1/4 of our width and length, which effectively becomes our radius.
        float dist = (meshRenderer.bounds.size.x / 4) + (this.maxRadius);
        // Create our spawnpoints and store them into a list.
        for(int i = 0; i < 5; i++)
        {
            // Results in the angles being used (in degrees): 0, 72, 144, 216, 288
            float tempDegree = degreesOfSpawn * i;
            float newX = dist * Mathf.Cos(tempDegree);
            float newZ = dist * Mathf.Sin(tempDegree);
            // Math.
            if(tempDegree > 90.0f || tempDegree < 270.0f)
            {
                newX *= -1;
            }
            // Math.
            if(tempDegree > 180)
            {
                newZ *= -1;
            }
            Vector3 newPos = new Vector3(newX, tracker.GetFireplacePos().y, newZ);
            // Create the new Gamobject then update its position.
            GameObject newSpawnPoint = new GameObject();
            newSpawnPoint.transform.position = newPos;
            // Add the spawner to our gameobject.
            spawnPoints.Add(newSpawnPoint);
        }
        int listCount = spawnPoints.Count - 1;
        // Loop through each spawnpoint and add/modify components to it.
        foreach (GameObject go in spawnPoints)
        {
            // Adds the SphereCollider component onto the newly created gameobject.
            go.AddComponent(typeof(SphereCollider));
            // Changes variables within our SphereCollider component.
            go.GetComponent<SphereCollider>().isTrigger = true;
            go.GetComponent<SphereCollider>().radius = (meshRenderer.bounds.size.x / 2) - (this.maxRadius);
            // Add the script that will handle the spawning of our enemies.
            go.AddComponent<SpawnEnemy>();
            go.GetComponent<SpawnEnemy>().SetEnemyPrefab(enemyPrefab[listCount--]);
        }
    }
}
