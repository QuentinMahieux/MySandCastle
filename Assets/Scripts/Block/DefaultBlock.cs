using UnityEngine;

public class DefaultBlock : MonoBehaviour
{
    public BlockData data;
    public BlockManager blockManager;
    public virtual void ClickLeft()
    {
        if (GameManager.instance.creative) blockManager.RemoveBlock(0);
        else if(!GameManager.instance.currentBlockData) blockManager.TakeBlock();
    }

    public virtual void ClickRight()
    {
        blockManager.PlaceBlock(1);
    }
    
}

