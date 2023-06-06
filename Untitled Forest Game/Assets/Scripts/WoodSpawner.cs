using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int numRegions;
    [SerializeField] private float minRadius = 10;
    private Vector3 gameAreaSize;
    // Holds info about the size of our play area.
    private MeshRenderer meshRenderer;
    // This will help in disecting the different bounds of region.
    private float maxRadius;
    // Keeps track of the number of wood remaining, spawn more to meet some requirement.
    private float numWood;

    // Start is called before the first frame update
    void Awake()
    {
        // Get the size of our play area.
        meshRenderer = GetComponent<MeshRenderer>();
        gameAreaSize = meshRenderer.bounds.size; 
        this.maxRadius = gameAreaSize.x / 2;
    }

    void Start()
    {
        SpawnWood();
    }

    void Update()
    {

    }

    // Generate the different regions in our game, 
    // In this case 5 which equates to the number of different mobs we have.
    void SpawnWood()
    {
        float temp = this.minRadius;
        // Spawn a specific number of wood at different regions of the map.
        // Specific number can be decided later on.
        for(int i = 1; i <= numRegions; i++) {
            // Random scaler for now on how much wood to spawn.
            float woodToSpawn = i * 1;
            this.numWood += woodToSpawn;
            // Our max and min radius will change as needed.
            float r1 = (this.maxRadius / numRegions) * i;
            float r2 = ((this.maxRadius - this.minRadius) / numRegions) * i;
            // Spawn each wood prefab properly.
            for(int j = 0; j < woodToSpawn; j++) {
                // Calculate a random theta and distance to get our wood spawn position.
                float dist = Mathf.Sqrt((Mathf.Pow(r1, 2) - Mathf.Pow(r2, 2) + Mathf.Pow(r2, 2)));
                // Random value between 0 and 2pi
                float theta = Random.Range(0, 360);
                // These will be converted into cartesion coordinates.
                float x = dist * Mathf.Cos(theta);
                float z = dist * Mathf.Sin(theta);
                // Instantiate the wood.
                Instantiate(woodPrefab, new Vector3(x, this.transform.position.y, z), Quaternion.identity);
            }
        }
    }
}
