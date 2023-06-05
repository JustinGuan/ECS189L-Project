using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private LocationTracker tracker;
    private GameObject enemyPrefab;
    private SphereCollider sCollider;
    private float maxEnemies = 5.0f;
    // How far the enemies should spawn around the player.
    private float maxRadius;
    // Keeps track of current number of enemies.
    private float numEnemies = 0;
    private float timeSinceSpawn = 0;
    // Whether or not the player is in the cirlce still.
    private bool inRange = false;

    void Awake()
    {
        tracker = GameObject.Find("GameArea").GetComponent<LocationTracker>();
        sCollider = gameObject.GetComponent<SphereCollider>();
        this.maxRadius = sCollider.radius;
    }


    void Update()
    {
        // If the player is not inside the radius or numEnemies is reached, do nothing.
        if(!inRange || numEnemies == maxEnemies)
        {
            return;
        }
        // Dont do anything if the player is near the fireplace.
        if(Vector3.Distance(tracker.GetPlayerPos(), tracker.GetFireplacePos()) < 10.0f)
        {
            return;
        }
        // Update our timer first.
        timeSinceSpawn += Time.deltaTime;
        // If there are no enemies, we spawn in the enemies.
        if(numEnemies == 0)
        {
            SpawnEnemies();
            timeSinceSpawn = 0;
        }
        // If there are enemies, then we spawn via a coroutine.
        // We spawn enemies until we reach a max number of enemies possible.
        else if((numEnemies < this.maxEnemies) && (timeSinceSpawn >= 5.0f))
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
        for(int i = 0; i < enemiesLeft; i++)
        {
            float r1 = this.maxRadius * Random.Range(0f, 1f);
            float theta = Random.Range(0, 2.0f * Mathf.PI);
            float x = spawnerPos.x + (r1 * Mathf.Cos(theta));
            float z = spawnerPos.z + (r1 * Mathf.Sin(theta));
            Debug.Log(r1);
            // Prevents enemy from spawning near the fireplace.
            while(Vector3.Distance(new Vector3(x, 0, z), firePos) < 10.0f)
            {
                // Recalculate our values until we get something not in range of the fire.
                r1 = this.maxRadius * Random.Range(0, 1);
                theta = Random.Range(0, 2.0f * Mathf.PI);
                x = spawnerPos.x + (r1 * Mathf.Cos(theta));
                z = spawnerPos.z + (r1 * Mathf.Sin(theta));
            }
            Instantiate(enemyPrefab, new Vector3(x, -0.5f, z), Quaternion.identity, this.transform);
        }        
        this.numEnemies += enemiesLeft;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
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
