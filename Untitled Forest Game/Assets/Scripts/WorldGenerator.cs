using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int worldSize = 100;
    public float scale = 0.1f;
    public float heightMultiplier = 10f;
    public int octaves = 4;
    public float persistence = 0.5f;
    public float lacunarity = 2f;

    
    public GameObject[] enemySpawners;
    public GameObject[] treePrefabs;
    public GameObject[] rockPrefabs;
    public GameObject[] mushroomPrefabs;
    public GameObject[] grassPrefab;
    public GameObject flamePrefab;
    public int spawnerDistance = 100;
    public int maxTrees = 100;
    public int maxRocks = 100;
    public int maxMushrooms = 100;
    public int maxGrass = 100;
    private int mapEdge;

    private void Start()
    {
        mapEdge = (int)(worldSize / 2f);
        GenerateTerrain();

        GenerateObjects(treePrefabs, maxTrees);
        GenerateObjects(rockPrefabs, maxRocks);
        GenerateObjects(mushroomPrefabs, maxMushrooms);
        GenerateObjects(grassPrefab, maxGrass);
        GenerateFlame();
        GenerateSpawners();
    }

    private void GenerateTerrain()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainData terrainData = new TerrainData();

        terrainData.heightmapResolution = worldSize + 1;
        terrainData.size = new Vector3(worldSize, 100, worldSize);
        terrain.terrainData = terrainData;

        float[,] heightmap = new float[worldSize, worldSize];

        for (int x = -mapEdge; x < mapEdge; x++)
        {
            for (int z = -mapEdge; z < mapEdge; z++)
            {
                float height = CalculateHeight(x, z);
                if (x*x < 100 && z*z < 100)
                {
                    height = 0f;
                }
                heightmap[x + mapEdge, z + mapEdge] = height; // Offset by mapEdge to make starting index 0 instead of -500
            }
        }

        terrainData.SetHeights(0, 0, heightmap);
    }

    private float CalculateHeight(int x, int z)
    {
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

    // Can avoid overlaps if you keep an array of all the positions.
    // If the random position chosen is to close to one that has already been used, choose a new one.
    private void GenerateObjects(GameObject[] objectArray, int maxObjects)
    {
        for (int i = 0; i < maxObjects; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            int randomIndex = Random.Range(0, objectArray.Length);
            GameObject prefab = objectArray[randomIndex];

            Instantiate(prefab, randomPosition, Quaternion.identity);
        }        
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-mapEdge, mapEdge);
        float z = Random.Range(-mapEdge, mapEdge);
        float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0f, z));

        return new Vector3(x, y, z);
    }

    private void GenerateFlame()
    {
        float flameHeight = Terrain.activeTerrain.SampleHeight(new Vector3(0f, 0f, 0f));
        Vector3 flamePosition = new Vector3(0f, flameHeight, 0f);
        Instantiate(flamePrefab, flamePosition, Quaternion.identity);
    }

    private void GenerateSpawners()
    {
        for (int n = 0; n < enemySpawners.Length; n++)
        {
            float angle = (float)(n * 72 * Mathf.PI / 180); // 72 degree rotations.  A pentagon.
            float x = -spawnerDistance * Mathf.Sin(angle);
            float z = spawnerDistance * Mathf.Cos(angle);
            float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0f, z));
            Vector3 spawnerPosition = new Vector3(x, y, z);

            Instantiate(flamePrefab, spawnerPosition, Quaternion.identity);
        }
    }
}
