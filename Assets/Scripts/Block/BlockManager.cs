using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Vector3 coordonee;
    public DefaultBlock actualBlock;
    
    [Header("Durability")]
    public int currentDurability;
    
    [Header("Block")]
    //public DefaultBlock[] blocks;
    
    [Header("Voisin")] 
    public Neighbor XP = new Neighbor();
    public Neighbor XN = new Neighbor();
    
    public Neighbor YP = new Neighbor();
    public Neighbor YN = new Neighbor();
    
    public Neighbor ZP= new Neighbor();
    public Neighbor ZN= new Neighbor();
    
    
    public void Instantiate(float x, float y, float z, string id)
    {
        coordonee = new Vector3(x, y, z);
        
        NeighborCoordonee(XP, new Vector3(x + 1,y,z));
        NeighborCoordonee(XN, new Vector3(x - 1,y,z));
        
        NeighborCoordonee(YP, new Vector3(x,y + 1,z));
        NeighborCoordonee(YN, new Vector3(x,y - 1,z));
        
        NeighborCoordonee(ZP, new Vector3(x,y,z + 1));
        NeighborCoordonee(ZN, new Vector3(x,y,z - 1));

        ChangeBlock(id);
    }
    
    void NeighborCoordonee(Neighbor neighbor, Vector3 newCoordonee)
    {
        neighbor.coordonee = newCoordonee;
    }

    public void FindNeighbours()
    {
        var world = WorldGenerator.instance;
        if(!XP.block) XP.block = world.FindNeighbour(XP.coordonee);
        if(!XN.block) XN.block = world.FindNeighbour(XN.coordonee);
        
        if(!YP.block) YP.block = world.FindNeighbour(YP.coordonee);
        if(!YN.block) YN.block = world.FindNeighbour(YN.coordonee);
        
        if(!ZP.block) ZP.block = world.FindNeighbour(ZP.coordonee);
        if(!ZN.block) ZN.block = world.FindNeighbour(ZN.coordonee);
        
        if(GameManager.instance.creative) CanPlaceBlock(true);
    }
    
    /**public void FindBlock(string id)
    {
        foreach (DefaultBlock block in blocks)
        {
            if (block.data.id == id)
            {
                ChangeBlock(block.data.id);
                return;
            }
        }
        FindBlock("T");
    }
    **/
    
    public void PlaceBlock(int hight)
    {
        if (GameManager.instance.creative)
        {
            if (hight == 1)
            {
                YP.block.ChangeBlock(CustomWorld.instance.buildingBlock.id);
            }
            else if (hight == 0)
            {
                ChangeBlock(CustomWorld.instance.buildingBlock.id);
            }
            else if (hight == -1)
            {
                YN.block.ChangeBlock(CustomWorld.instance.buildingBlock.id);
            }
        }
        else
        {
            foreach (BlockManager block in WorldGenerator.instance.blocks.Values)
            {
                block.gameObject.SetActive(true);
                block.CanPlaceBlock(false);
            }
            if (hight == 1)
            {
                YP.block.ChangeBlock(GameManager.instance.currentBlockData.id);
            }
            else if (hight == 0)
            {
                ChangeBlock(GameManager.instance.currentBlockData.id);
            }
            else if (hight == -1)
            {
                YN.block.ChangeBlock(GameManager.instance.currentBlockData.id);
            }

            GameManager.instance.currentBlockData = null;
        }
    }

    public void RemoveBlock(int hight)
    {
        if (actualBlock.data.id != "V")
        {
            if (hight == 0)
            {
                ChangeBlock("V");
                if(YP.block) YP.block.Gravity();
            }
            else if (hight == -1 && YN.block.actualBlock.data.isDestroyeble)
            {
                YN.block.ChangeBlock("V");
                Gravity();
            }
        }
        if(YN.block) YN.block.CanPlaceBlock(true);
    }

    public void TakeBlock()
    {
        GameManager.instance.currentBlockData = actualBlock.data.loot;
        GameManager.instance.currentCoordonee = coordonee;
        RemoveBlock(0);

        foreach (BlockManager block in WorldGenerator.instance.blocks.Values)
        {
            Vector3 origine = new Vector3(coordonee.x, 0f, coordonee.z);
            Vector3 target = new Vector3(block.coordonee.x, 0f, block.coordonee.z);
            
            float distance = Vector3.Distance(origine, target);
            if (distance <= GameManager.instance.distanceToPlaceBlock)
            {
                block.CanPlaceBlock(true);
            }
            else
            {
                block.gameObject.SetActive(false);
            }
        }
    }

    public void ChangeBlock(string id)
    {
        if(actualBlock) SaveElement.instance.RemoveBlock(actualBlock);
        
        DefaultBlock defaultBlock = SaveElement.instance.FindBlock(id);

        if (defaultBlock)
        {
            actualBlock = defaultBlock;
            actualBlock.blockManager = this;
            
            actualBlock.gameObject.SetActive(true);
            actualBlock.transform.SetParent(transform);
            actualBlock.transform.localPosition = Vector3.zero;
            
            currentDurability = actualBlock.data.durability;
        }
        else
        {
            foreach (BlockData data in GameManager.instance.blocks)
            {
                if (id == data.id)
                {
                    GameObject block = Instantiate(data.prefab, transform.position, transform.rotation, transform);
                    DefaultBlock newDefaultBlock =  block.GetComponent<DefaultBlock>();
                
                    actualBlock = newDefaultBlock;
                    actualBlock.blockManager = this;
                    currentDurability = actualBlock.data.durability;
                }
            }
        }
        
        
        Gravity();
        if (GameManager.instance.creative) CanPlaceBlock(true);
    }

    public void Gravity()
    {
        if(!YN.block) return;
        if(YN.block.actualBlock.data.id == actualBlock.data.id) return;
        
        if (YN.block.actualBlock.data.blockType == BlockType.transparent)
        {
            YN.block.ChangeBlock(actualBlock.data.id);
            RemoveBlock(0);
        }
    }

    public void CanPlaceBlock(bool isActive)
    {
        if (actualBlock.data.blockType == BlockType.full && YP.block && isActive && !actualBlock.data.isWater)
        {
            if (YP.block.actualBlock.data.id == "V") YP.block.ChangeBlock("C");
        }
        else if (actualBlock.data.blockType == BlockType.full && YP.block && !isActive)
        {
            if (YP.block.actualBlock.data.id == "C") YP.block.ChangeBlock("V");

        }

    }

    public void TakeDamage(int damage)
    {
        if (actualBlock.data.isDestroyeble)
        {
            currentDurability -= damage;
            
            if(currentDurability <= 0) RemoveBlock(0);
        }
    }
    
}

[System.Serializable]
public class Neighbor
{
    public BlockManager block;
    public Vector3 coordonee = new Vector3(0,0,0);
}