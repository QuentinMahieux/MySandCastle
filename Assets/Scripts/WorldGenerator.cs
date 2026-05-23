using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public static WorldGenerator instance;

    [Header("Generation Settings")]
    private Vector3 sizeWorld =  Vector3.zero;
    public bool creative = false;

    public string code;
    public float distanceUnderBlock = 1f;

    [Header("Block")]
    public BlockData[] blockDatas;
    public List<DefaultBlock> blocks;


    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("[WorldGenerator] instance already exists");
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        ChargeSeed();
    }

    void ChargeSeed()
    {
        SeedGenerator.instance.Decode(code, out int x, out int y, out int z, out string seed);
        
        sizeWorld = new Vector3(x, y, z);

        StartCoroutine(Generate(seed));
    }

    IEnumerator Generate(string seed)
    {
        int index = 0;
        
        float distanceX = 0;
        float distanceY = 0;
        float distanceZ = 0;
        
        for (int y = 0; y < sizeWorld.y; y++)
        {
            distanceZ = 0;
            for (int z = 0; z < sizeWorld.z; z++)
            {
                distanceX = 0;
                for (int x = 0; x < sizeWorld.x; x++)
                {
                    if (seed.Length > index)
                    {
                        PlaceBlock(x, y, z, distanceX, distanceY, distanceZ, seed[index].ToString());
                    }
                    else
                    {
                        PlaceBlock(x, y, z, distanceX, distanceY, distanceZ, "T");
                    }
                    yield return new WaitForSeconds(0.0005f);

                    index++;
                    distanceX += distanceUnderBlock;
                }
                distanceZ += distanceUnderBlock;
            }
            distanceY += distanceUnderBlock;
        }
        yield return new WaitForSeconds(0.1f);
        AddNeighbours();
    }

    void PlaceBlock(int x, int y, int z, float distanceX, float distanceY, float distanceZ, string id)
    {
        GameObject block = Instantiate(FindBlock(id), new Vector3(distanceX, distanceY, distanceZ), Quaternion.identity);
        DefaultBlock blockComponent = block.GetComponent<DefaultBlock>();
        blocks.Add(blockComponent);
        
        blockComponent.Instantiate(x, y, z);
    }

    public GameObject FindBlock(string id)
    {
        foreach (BlockData data in blockDatas)
        {
            if (data.id == id)
            {
                return data.prefab;
            }
        }
        return blockDatas[^1].prefab;
    }
    
    public void AddNeighbours()
    {
        foreach (DefaultBlock block in blocks)
        {
            block.FindNeighbours();
        }
    }

    public DefaultBlock FindNeighbour(Vector3 neighbourCoordonee)
    {
        foreach (DefaultBlock block in blocks)
        {
            if (block.coordonee == neighbourCoordonee)
            {
                return block;
            }
        }
        return null;
    }

}
