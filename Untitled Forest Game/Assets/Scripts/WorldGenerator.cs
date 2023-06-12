using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int worldSize = 100;
    public float scale = 0.1f;
    public float heightMultiplier = 10f;
    public int octaves = 4;
    public float persistence = 0.5f;
    public float lacunarity = 2f;

    public GameObject[] objectPrefabs;
    public int maxObjects = 1000;

    private void Start()
    {
        GenerateTerrain();
        GenerateObjects();
    }

    private void GenerateTerrain()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainData terrainData = new TerrainData();

        terrainData.heightmapResolution = worldSize + 1;
        terrainData.size = new Vector3(worldSize, 100, worldSize);
        terrain.terrainData = terrainData;

        float[,] heightmap = new float[worldSize, worldSize];

        int leftEdge = (int)(-worldSize / 2f);
        int rightEdge = (int)(-worldSize / 2f);
        for (int x = leftEdge; x < rightEdge; x++)
        {
            for (int z = leftEdge; z < rightEdge; z++)
            {
                float height = CalculateHeight(x, z);
                heightmap[x, z] = height;
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

    private void GenerateObjects()
    {
        for (int i = 0; i < maxObjects; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            int randomIndex = Random.Range(0, objectPrefabs.Length);
            GameObject prefab = objectPrefabs[randomIndex];

            Instantiate(prefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-worldSize / 2f, worldSize / 2f);
        float z = Random.Range(-worldSize / 2f, worldSize / 2f);
        float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0f, z));

        return new Vector3(x, y, z);
    }
}
