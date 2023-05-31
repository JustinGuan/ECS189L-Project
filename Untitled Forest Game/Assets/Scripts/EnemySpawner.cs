using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float maxEnemies;
    private LocationTracker tracker;
    // Keeps track of current number of enemies.
    private float numEnemies;
    private float timeSinceSpawn;
    // How far the enemies should spawn around the player.
    private float maxRadius = 10.0f;
    private float minRadius = 3.0f;

    private void Start()
    {
        tracker = GetComponent<LocationTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        // Dont do anythign if the player is near the fireplace.
        if(Vector3.Distance(tracker.GetPlayerPos(), tracker.GetFireplacePos()) < 10.0f)
        {
            Debug.Log(tracker.GetFireplacePos());
            return;
        }
        // Update our timer first.
        timeSinceSpawn += Time.deltaTime;
        // If tehre are no enemies, we spawn in the enemies.
        if(numEnemies == 0)
        {
            SpawnEnemy();
            timeSinceSpawn = 0;
        }
        // If there are enemies, then we spawn via a coroutine.
        // We spawn enemies until we reach a max number of enemies possible.
        else if((numEnemies < this.maxEnemies) && (timeSinceSpawn >= 5.0f))
        {
            SpawnEnemy();
            timeSinceSpawn = 0;
        }
    }

    void SpawnEnemy()
    {
        float r1 = this.maxRadius;
        float r2 = this.minRadius;
        // Determine how many enemies to spawn.
        float enemiesLeft = this.maxEnemies - this.numEnemies;
        // Spawns the enemy. Let the scaling of the enemies be done in the Enemy script.
        // Type of enemy being spawned will be determined later.
        for(int i = 0; i < enemiesLeft; i++)
        {
            // Where to spawn near the player.
            float dist = Mathf.Sqrt((Mathf.Pow(r1, 2) - Mathf.Pow(r2, 2) + Mathf.Pow(r2, 2)));
            float theta = Random.Range(0, 360);
            float x = (dist * Mathf.Cos(theta)) + tracker.GetPlayerPos().x;
            float z = (dist * Mathf.Sin(theta)) + tracker.GetPlayerPos().z;
            while(Vector3.Distance(new Vector3(x, 0, z), tracker.GetFireplacePos()) < 10.0f)
            {
                // Recalculate our values until we get something not in range of the fire.
                theta = Random.Range(0, 360);
                x = (dist * Mathf.Cos(theta)) + tracker.GetPlayerPos().x;
                z = (dist * Mathf.Sin(theta)) + tracker.GetPlayerPos().z;
            }
            Instantiate(enemyPrefab, new Vector3(x, -0.446f, z), Quaternion.identity);
        }        
        this.numEnemies += enemiesLeft;
    }

    // Mainly used for despawning mobs.
    public void UpdateEnemyCount()
    {
        this.numEnemies--;
    }    
}
