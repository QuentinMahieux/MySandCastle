using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Scriptable Objects/BlockData")]
public class BlockData : ScriptableObject
{
    public string id;
    public BlockType blockType;

    public bool isWater;
    public bool isDestroyeble = true;
    public int durability = 5;


}
public enum BlockType
{
    full,
    transparent
}