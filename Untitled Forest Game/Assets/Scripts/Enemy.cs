using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float damage = 5.0f;
    [SerializeField] private float maxDistance = 10.0f;
    private GameObject player;
    // Scales this depending on the distance b/w player and fireplace.
    private float hpScaling;
    private float dmgScaling;

    private EnemySpawner eSpawner;

    void Awake()
    {
        eSpawner = GameObject.Find("GameArea").GetComponent<EnemySpawner>();
        player = GameObject.Find("Capsule");
    }

    // Update is called once per frame
    void Update()
    {
        // Use the two coordinates to determine whether or not to despawn the enemy.
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        // If the player runs too far from the enemy, we despawn the enemy.
        if(dist >= maxDistance)
        {
            eSpawner.UpdateEnemyCount();
            Destroy(gameObject);
        }
    }
}
