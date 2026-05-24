using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Scriptable Objects/BlockData")]
public class BlockData : ScriptableObject
{
    public string id;
    public BlockType blockType;
    public GameObject prefab;
    

}
public enum BlockType
{
    full,
    transparent
}