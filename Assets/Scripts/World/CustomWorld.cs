using UnityEngine;
using UnityEngine.UIElements;

public class CustomWorld : MonoBehaviour
{
    public static CustomWorld instance;
    [Header("Settings")]
    public Vector3 customSize;
    public int sandHight = 2;
    public string customCode;
    public BlockData buildingBlock;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("[CustomWorld] instance already exists");
            Destroy(gameObject);
        }
    }

    public void CreateCustomWorld()
    {

        float surface = customSize.x * customSize.z;
        string seed = "";

        for (int i = 0; i < surface; i++)
        {
            seed += "B";
        }

        for (int i = 0; i < sandHight; i++)
        {
            for (int j = 0; j < surface; j++)
            {
                seed += "S";
            }
        }
        
        WorldGenerator.instance.ChargeWorld(SeedGenerator.instance.Encode(customSize,seed));
    }

    public void SaveCustomWorld()
    {
        GUIUtility.systemCopyBuffer = SeedGenerator.instance.Encode(customSize,
            SeedGenerator.instance.CreateSeed(WorldGenerator.instance.blocks));
    }

    public void LoadCustomWorld()
    {
        WorldGenerator.instance.ChargeWorld(customCode);
    }
}
