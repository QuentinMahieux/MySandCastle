using UnityEngine;

public class DefaultBlock : MonoBehaviour
{
    public BlockData data;
    public BlockManager blockManager;
    public virtual void ClickLeft()
    {
        blockManager.RemoveBlock(0);
    }

    public virtual void ClickRight()
    {
        blockManager.PlaceBlock(1);
    }
    
}

