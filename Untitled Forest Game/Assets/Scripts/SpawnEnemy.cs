using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Embers;

public class SpawnEnemy : MonoBehaviour
{
    private LocationTracker tracker;
    private GameObject enemyPrefab;
    private SphereCollider sCollider;
    private float maxEnemies = 5.0f;
    // The radius where the enemies should spawn in.
    private float maxRadius;
    // Keeps track of current number of enemies.
    private float numEnemies = 0;
    private float timeSinceSpawn = 0;
    // Whether or not the player is in the cirlce still.
    private bool inRange = false;
    private List<Transform> patrolPoints;
    // Find the radius of our fire.
    private FireplaceMechanic fireMechanic;

    void Awake()
    {
        tracker = GameObject.Find("GameArea").GetComponent<LocationTracker>();
        fireMechanic = GameObject.Find("Flame").GetComponent<FireplaceMechanic>();
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
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, enemyPrefab.transform.position.y, z), Quaternion.identity, this.transform);
            // Adds the script enemy controller here, so we can mess with the patrol points.
            enemy.AddComponent<EnemyController>();
            // Gives the enemy its patrol positions.
            SetPatrolPoints(enemy);
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

    private void SetPatrolPoints(GameObject enemy)
    {
        // List to hold our new patrol points.
        Transform[] patrolPoints = new Transform[4];
        // get our spawner's and fire's x and z coords.
        float spawnerX = this.transform.position.x;
        float spawnerZ = this.transform.position.z;
        float fireX = tracker.GetFireplacePos().x;
        float fireZ = tracker.GetFireplacePos().z;
        // Get the fire's radius.
        float curFireRadius = fireMechanic.GetFireRadius();
        // Set the 4 patrol points around the spawner.
        // The radii will be set at 0, pi/2, pi, and 3pi/2 degrees.
        for(int i = 0; i < 4; i++)
        {
            // There will be 4 patrol points, each at 2/3 of its max radius spawn.
            float patrolRadius = this.maxRadius * 0.67f;
            float theta = ((float)i * Mathf.PI) / 2.0f;
            float x = spawnerX + (patrolRadius * Mathf.Cos(theta));
            float z = spawnerZ + (patrolRadius * Mathf.Sin(theta));
            // Edge case: patrol point is within the fire radius, change the value accordingly.
            Vector3 newPatrolPoint = new Vector3(x, this.transform.position.y, z);
            // If our patrol point falls within the fire's safe zone, change either x or z value.
            if(Vector3.Distance(newPatrolPoint, tracker.GetFireplacePos()) <= curFireRadius)
            {
                // Update the patrol radius, so that it's at the edge of the fire radius.
                patrolRadius -= curFireRadius;
                // Determine which value (x or z) needs to be modfied.
                if(Mathf.Abs(fireX - spawnerX) > Mathf.Abs(fireZ - spawnerZ))
                {
                    x = spawnerX + (patrolRadius * Mathf.Cos(theta));
                }
                else
                {
                    z = spawnerZ + (patrolRadius * Mathf.Sin(theta));
                }
                // Re-initialize one of our patrol points.
                newPatrolPoint = new Vector3(x, this.transform.position.y, z);
            }
            // Create a new gameobject, and grab it's transform.
            var patrolPoint = new GameObject().transform;
            // Set the new game object's transform to the newly created patrol point.
            patrolPoint.localPosition = newPatrolPoint;
            // Store that value into our Transform[].
            patrolPoints[i] = patrolPoint;
        }
        // Initialize the enemy's patrol points.
        enemy.GetComponent<EnemyController>().patrolBehavior.SetPatrol(patrolPoints);
    }
}
