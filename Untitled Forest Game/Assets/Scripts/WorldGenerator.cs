using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] int worldSize = 100;
    public float scale = 0.1f;
    public float heightMultiplier = 10f;
    public int octaves = 4;
    public float persistence = 0.5f;
    public float lacunarity = 2f;

    public GameObject[] objectPrefabs;
    public int maxObjects = 1000;
    private int mapEdge;

    private void Start()
    {
        GenerateTerrain();
        GenerateObjects();
        mapEdge = (int)(worldSize / 2f);
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
                heightmap[x, z] = height;
            }
        }

        terrainData.SetHeights(-mapEdge, -mapEdge, heightmap);
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
        float x = Random.Range(-mapEdge, mapEdge);
        float z = Random.Range(-mapEdge, mapEdge);
        float y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0f, z));

        return new Vector3(x, y, z);
    }
}
