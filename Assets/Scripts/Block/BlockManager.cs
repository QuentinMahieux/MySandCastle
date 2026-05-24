using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Vector3 coordonee;
    public DefaultBlock actualBlock;
    
    [Header("Block")]
    public DefaultBlock[] blocks;
    
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
        
        CanPlaceBlock();
    }
    
    public void FindBlock(string id)
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
    
    public void PlaceBlock(int hight)
    {
        if (WorldGenerator.instance.creative)
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
    }

    public void RemoveBlock(int hight)
    {
        
        if (WorldGenerator.instance.creative && actualBlock.data.id != "V")
        {
            if (hight == 0)
            {
                ChangeBlock("V");
                if(YP.block) YP.block.Gravity();
            }
            else if (hight == -1)
            {
                YN.block.ChangeBlock("V");
                Gravity();
            }
        }
        
    }

    public void ChangeBlock(string id)
    {
        foreach (DefaultBlock block in blocks)
        {
            block.gameObject.SetActive(false);
            if (id == block.data.id)
            {
                block.gameObject.SetActive(true);
                actualBlock = block;
            }
        }
        Gravity();
        CanPlaceBlock();
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

    public void CanPlaceBlock()
    {
        if (actualBlock.data.blockType == BlockType.full && YP.block)
        {
            if (YP.block.actualBlock.data.id == "V") YP.block.ChangeBlock("C");
        }
    }
    
}

[System.Serializable]
public class Neighbor
{
    public BlockManager block;
    public Vector3 coordonee = new Vector3(0,0,0);
}