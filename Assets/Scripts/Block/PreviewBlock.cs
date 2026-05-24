using UnityEngine;

public class PreviewBlock : DefaultBlock
{
    public override void ClickLeft()
    {
        blockManager.RemoveBlock(-1);
    }

    public override void ClickRight()
    {
        blockManager.PlaceBlock(0);
    }

}
