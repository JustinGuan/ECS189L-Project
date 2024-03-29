using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float maxRadius;
    [SerializeField] private float maxEnemies = 5.0f;
    private LocationTracker tracker;
    private SphereCollider sCollider;
    private WorldGenerator wg;
    // Keeps track of current number of enemies.
    private float numEnemies = 0;
    private float timeSinceSpawn = 0;
    // Whether or not the player is in the cirlce still.
    private bool inRange = true;

    void Awake()
    {
        tracker = GameObject.Find("World Generator").GetComponent<LocationTracker>();
        wg = GameObject.Find("World Generator").GetComponent<WorldGenerator>();
    }

    void Start()
    {
        this.maxRadius = wg.worldSize / 4;
    }

    void Update()
    {
        if(tracker.fireplace == null)
        {
            tracker = GameObject.Find("World Generator").GetComponent<LocationTracker>();
            return;
        }
        // If the player is not inside the radius or numEnemies is reached, do nothing.
        if (!inRange || numEnemies == maxEnemies)
        {
            return;
        }
        // Dont do anything if the player is near the fireplace.
        if (Vector3.Distance(tracker.GetPlayerPos(), tracker.GetFireplacePos()) < 100.0f)
        {
            return;
        }
        // Update our timer first.
        timeSinceSpawn += Time.deltaTime;
        // If there are no enemies, we spawn in the enemies.
        if (numEnemies == 0)
        {
            SpawnEnemies();
            timeSinceSpawn = 0;
        }
        // If there are enemies, then we spawn via a coroutine.
        // We spawn enemies until we reach a max number of enemies possible.
        else if ((numEnemies < this.maxEnemies) && (timeSinceSpawn >= 5.0f))
        {
            SpawnEnemies();
            timeSinceSpawn = 0;
        }
    }

    void SpawnEnemies()
    {
        Vector3 firePos = tracker.GetFireplacePos();
        Vector3 spawnerPos = this.transform.position;
        // Determine how many enemies to spawn.
        float enemiesLeft = this.maxEnemies - this.numEnemies;
        // Spawns the enemy. Let the scaling of the enemies be done in the Enemy script.
        for (int i = 0; i < enemiesLeft; i++)
        {
            float r1 = this.maxRadius * Random.Range(0f, 1f);
            float theta = Random.Range(0, 2.0f * Mathf.PI);
            float x = spawnerPos.x + (r1 * Mathf.Cos(theta));
            float z = spawnerPos.z + (r1 * Mathf.Sin(theta));
            float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0f, z));
            Instantiate(enemyPrefab, new Vector3(x, y, z), Quaternion.identity, this.transform);
        }
        this.numEnemies += enemiesLeft;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.inRange = false;
        }
    }

    public void Despawn(GameObject go)
    {
        numEnemies--;
        Destroy(go);
    }

    public void SetEnemyPrefab(GameObject prefab)
    {
        this.enemyPrefab = prefab;
    }
}