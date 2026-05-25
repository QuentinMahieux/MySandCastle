using System.Collections.Generic;
using UnityEngine;

public class SaveElement : MonoBehaviour
{    
    public static SaveElement instance;
    public List<DefaultBlock> desactiveBlocks;
   

    void Awake()
    {
        if (!instance) instance = this;
        else
        {
            Debug.LogError("[SaveElement] instance already exists");
            Destroy(gameObject);
        }
    }

    public void RemoveBlock(DefaultBlock block)
    {
        desactiveBlocks.Add(block);
        block.gameObject.SetActive(false);
    }

    public DefaultBlock FindBlock(string id)
    {
        foreach (var block in desactiveBlocks)
        {
            if (block.data.id == id)
            {
                desactiveBlocks.Remove(block);
                return block;
            }
        }

        return null;
    }
}
