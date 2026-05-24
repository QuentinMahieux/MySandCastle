using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldGenerator : MonoBehaviour
{
    public static WorldGenerator instance;

    [Header("Generation Settings")]
    private Vector3 sizeWorld =  Vector3.zero;
    public bool creative = false;

    public float distanceUnderBlock = 1f;

    [Header("Block")]
    public GameObject blockPrefab;
    public List<BlockManager> blocks;

    [Header("Interface")] 
    public Slider sliderLoading;

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
        sliderLoading.gameObject.SetActive(false);
    }

    public void ChargeWorld(string code)
    {
        SeedGenerator.instance.Decode(code, out int x, out int y, out int z, out string seed);
        
        sizeWorld = new Vector3(x, y, z);
        
        sliderLoading.gameObject.SetActive(true);

        float surface = x * y * z;
        sliderLoading.maxValue = surface + surface / 5;


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
                        PlaceBlock(x, y, z, distanceX, distanceY, distanceZ, "V");
                    }
                    index++;
                    sliderLoading.value = index;
                    distanceX += distanceUnderBlock;
                }
                yield return new WaitForFixedUpdate();
                distanceZ += distanceUnderBlock;
            }
            distanceY += distanceUnderBlock;
        }
        yield return new WaitForSeconds(0.01f);
        sliderLoading.value = sliderLoading.maxValue;
        AddNeighbours();
        
        yield return new WaitForSeconds(0.05f);
        sliderLoading.gameObject.SetActive(false);
    }

    void PlaceBlock(int x, int y, int z, float distanceX, float distanceY, float distanceZ, string id)
    {
        GameObject block = Instantiate(blockPrefab, new Vector3(distanceX, distanceY, distanceZ), Quaternion.identity);
        BlockManager blockComponent = block.GetComponent<BlockManager>();
        blocks.Add(blockComponent);
        
        blockComponent.Instantiate(x, y, z, id);
    }

    
    
    public void AddNeighbours()
    {
        foreach (BlockManager block in blocks)
        {
            block.FindNeighbours();
        }
    }

    public BlockManager FindNeighbour(Vector3 neighbourCoordonee)
    {
        foreach (BlockManager block in blocks)
        {
            if (block.coordonee == neighbourCoordonee)
            {
                return block;
            }
        }
        return null;
    }

}
