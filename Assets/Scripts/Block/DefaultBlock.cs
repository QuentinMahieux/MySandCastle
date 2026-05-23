using UnityEngine;

public class DefaultBlock : MonoBehaviour
{
    public Vector3 coordonee;
    public BlockData data;
    
    [Header("Voisin")] 
    public Neighbor XP = new Neighbor();
    public Neighbor XN = new Neighbor();
    
    public Neighbor YP = new Neighbor();
    public Neighbor YN = new Neighbor();
    
    public Neighbor ZP= new Neighbor();
    public Neighbor ZN= new Neighbor();
    
    
    public void Instantiate(float x, float y, float z)
    {
        coordonee = new Vector3(x, y, z);
        
        NeighborCoordonee(XP, new Vector3(x + 1,y,z));
        NeighborCoordonee(XN, new Vector3(x - 1,y,z));
        
        NeighborCoordonee(YP, new Vector3(x,y + 1,z));
        NeighborCoordonee(YN, new Vector3(x,y - 1,z));
        
        NeighborCoordonee(ZP, new Vector3(x,y,z + 1));
        NeighborCoordonee(ZN, new Vector3(x,y,z - 1));

        WorldGenerator.instance.AddNeighbours();
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
    }

    public void PlaceBlock()
    {
        if (WorldGenerator.instance.creative)
        {
            YP.block.ChangeBlock("S");
        }
    }

    public void RemoveBlock()
    {
        if (WorldGenerator.instance.creative)
        {
            ChangeBlock("V");
        }
    }

    public void ChangeBlock(string id)
    {
        GameObject newBlock = Instantiate(WorldGenerator.instance.FindBlock(id), transform.position, Quaternion.identity);
        newBlock.GetComponent<DefaultBlock>().Instantiate(coordonee.x, coordonee.y, coordonee.z);
        Destroy(gameObject);
    }
}

[System.Serializable]
public class Neighbor
{
    public DefaultBlock block;
    public Vector3 coordonee = new Vector3(0,0,0);
}