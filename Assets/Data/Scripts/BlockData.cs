using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Scriptable Objects/BlockData")]
public class BlockData : ScriptableObject
{
    public string id;
    public BlockType blockType;
    public GameObject prefab;

    public bool isWater;
    
    [Header("Destroyable")]
    public bool isDestroyeble = true;
    public int durability = 5;
    public BlockData loot;


}
public enum BlockType
{
    full,
    transparent
}