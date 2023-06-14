using UnityEngine;
using Unity.AI.Navigation;

public class WorldGenerator : MonoBehaviour
{
    public Transform worldTransform;
    // World generation parameters
    public int worldSize = 100;
    public float scale = 0.1f;
    public float heightMultiplier = 10f;
    public int octaves = 4;
    public float persistence = 0.5f;
    public float lacunarity = 2f;
    // Useful for calculations
    private int mapEdge;
    private NavMeshSurface navMeshSurface;

    // Prefabs to spawn in
    public GameObject[] enemySpawners;
    public GameObject[] treePrefabs;
    public GameObject[] rockPrefabs;
    public GameObject[] mushroomPrefabs;
    public GameObject[] grassPrefab;
    public GameObject[] passivePrefab;
    public GameObject flamePrefab;
    public int spawnerDistance = 250;
    public int maxTrees = 100;
    public int maxRocks = 100;
    public int maxMushrooms = 100;
    public int maxGrass = 100;
    public int maxPassives = 100;

    private void Start()
    {
        mapEdge = (int)(worldSize / 2f);
        worldTransform.position = new Vector3(-mapEdge, 0f, -mapEdge);
        GenerateTerrain();

        GenerateObjects(treePrefabs, maxTrees);
        GenerateObjects(rockPrefabs, maxRocks);
        GenerateObjects(mushroomPrefabs, maxMushrooms);
        GenerateObjects(grassPrefab, maxGrass);
        GenerateObjects(passivePrefab, maxPassives);
        GenerateFlame();

        // Access the NavMeshSurface component
        navMeshSurface = GetComponent<NavMeshSurface>();
        // Call the Bake function to update the NavMesh
        navMeshSurface.BuildNavMesh();
        
        GenerateSpawners();
    }

    private void GenerateTerrain()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainData terrainData = new TerrainData();

        // Set heightmap resolution and size
        terrainData.heightmapResolution = worldSize + 1;
        terrainData.size = new Vector3(worldSize, 100, worldSize);
        terrain.terrainData = terrainData;

        float[,] heightmap = new float[worldSize, worldSize];

        for (int x = -mapEdge; x <= mapEdge; x++)
        {
            for (int z = -mapEdge; z <= mapEdge; z++)
            {
                // Calculate height using Perlin noise
                float height = CalculateHeight(x, z);
                // Index starts at -500.  We offset by mapEdge to make it start at 0
                heightmap[x + mapEdge, z + mapEdge] = height; 
            }
        }

        for (int x = -mapEdge; x < mapEdge; x++)
        {
            heightmap[x + mapEdge, worldSize - 1] = 100f;
            heightmap[x + mapEdge, 0] = 100f;
        }
        for (int z = -mapEdge; z < mapEdge; z++)
        {
            heightmap[worldSize - 1, z + mapEdge] = 100f;
            heightmap[0, z + mapEdge] = 100f;
        }

            // Set heights of the terrain
            terrainData.SetHeights(0, 0, heightmap);

        // Assign TerrainData to TerrainCollider
        TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
        terrainCollider.terrainData = terrainData;
    }

    private float CalculateHeight(int x, int z)
    {
        // Calculate height using Perlin noise
        float height = 0f;
        float amplitude = 1f;
        float frequency = 1f;

        for (int i = 0; i < octaves; i++)
        {
            float perlinX = (float)x * scale * frequency;
            float perlinZ = (float)z * scale * frequency;
            float noiseValue = Mathf.PerlinNoise(perlinX, perlinZ) * 2f - 1f;

            height += noiseValue * amplitude;

            amplitude *= persistence;
            frequency *= lacunarity;
        }

        return height * heightMultiplier;
    }

    private void GenerateObjects(GameObject[] objectArray, int maxObjects)
    {
        // Generate objects (trees, rocks, mushrooms, grass, etc.)
        for (int i = 0; i < maxObjects; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            int randomIndex = Random.Range(0, objectArray.Length);
            GameObject prefab = objectArray[randomIndex];

            Instantiate(prefab, randomPosition, Quaternion.identity);
        }        
    }

    public Vector3 GetRandomPosition()
    {
        // Get a random position on the terrain
        float x = Random.Range(-mapEdge, mapEdge);
        float z = Random.Range(-mapEdge, mapEdge);
        float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0f, z));

        return new Vector3(x, y, z);
    }

    private void GenerateFlame()
    {
        // Generate flame object
        float flameHeight = Terrain.activeTerrain.SampleHeight(new Vector3(0f, 0f, 0f));
        Vector3 flamePosition = new Vector3(0f, flameHeight, 0f);
        GameObject go = Instantiate(flamePrefab, flamePosition, Quaternion.identity);
        GetComponent<LocationTracker>().fireplace = go;
    }

    private void GenerateSpawners()
    {
        float angle = (float)((2 * Mathf.PI) / 5);
        // Generate enemy spawners
        for (int n = 0; n < enemySpawners.Length; n++)
        {
            
            float x = spawnerDistance * Mathf.Sin(angle * n);
            float z = spawnerDistance * Mathf.Cos(angle * n);
            float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0f, z));
            Vector3 spawnerPosition = new Vector3(x, y, z);

            Instantiate(enemySpawners[n], spawnerPosition, Quaternion.identity);
        }
    }
}
