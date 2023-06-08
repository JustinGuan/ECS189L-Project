using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float damage = 5.0f;
    [SerializeField] private float maxDistance = 40.0f;
    // Scales this depending on the distance b/w player and fireplace.
    private float hpScaling;
    private float dmgScaling;
    private LocationTracker locTracker;
    private GameObject goParent;

    void Awake()
    {
        locTracker = GameObject.Find("GameArea").GetComponent<LocationTracker>();
        goParent = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Use the two coordinates to determine whether or not to despawn the enemy.
        float dist = Vector3.Distance(locTracker.GetPlayerPos(), this.transform.position);
        // If the player runs too far from the enemy, we despawn the enemy.
        if(dist >= maxDistance)
        {
            goParent.GetComponent<SpawnEnemy>().Despawn(this.gameObject);
        }
    }
}
