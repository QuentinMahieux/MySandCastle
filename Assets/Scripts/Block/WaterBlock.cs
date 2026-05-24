using System;
using UnityEngine;

public class WaterBlock : DefaultBlock
{
    private float spawnWaterTime;

    void OnEnable()
    {
        OceanManager.instance.currentWaterLevel++;
    }

    void OnDisable()
    {
        OceanManager.instance.currentWaterLevel--;
    }
    
    private void Update()
    {
        spawnWaterTime += Time.deltaTime;
        if (spawnWaterTime >= OceanManager.instance.speedLevel[(int)blockManager.coordonee.y])
        {
            spawnWaterTime = 0;
            
            SpawnWater();
        }
    }

    void SpawnWater()
    {
        //if(OceanManager.instance.currentWaterLevel > OceanManager.instance.maxWaterLevel) return;
            
        int random =  UnityEngine.Random.Range(0, 4);
       
        if (random == 0 && blockManager.ZP.block)
        {
            if (blockManager.ZP.block.actualBlock.data.blockType == BlockType.transparent)
            {
                blockManager.ZP.block.ChangeBlock("W");
                return;
            }
        }
        if (random == 1 && blockManager.ZN.block)
        {
            if (blockManager.ZN.block.actualBlock.data.blockType == BlockType.transparent)
            {
                blockManager.ZN.block.ChangeBlock("W");
                return;
            }
        }
        if (random == 2 && blockManager.XP.block)
        {
            if (blockManager.XP.block.actualBlock.data.blockType == BlockType.transparent)
            {
                blockManager.XP.block.ChangeBlock("W");
                return;
            }
        }
        if (random == 1 && blockManager.XN.block)
        {
            if (blockManager.XN.block.actualBlock.data.blockType == BlockType.transparent)
            {
                blockManager.XN.block.ChangeBlock("W");
                return;
            }
        }
    }
}
