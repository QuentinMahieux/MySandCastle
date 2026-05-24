using System;
using UnityEngine;

public class WaterBlock : DefaultBlock
{
    private float spawnWaterTime;
    
    [Header("Infiny")]
    public bool isInfiny;

    void OnEnable()
    {
        blockManager.Gravity();
        OceanManager.instance.currentWaterLevel++;
    }

    void OnDisable()
    {
        OceanManager.instance.currentWaterLevel--;
    }
    
    private void Update()
    {
        spawnWaterTime += Time.deltaTime;
        if (spawnWaterTime >= OceanManager.instance.speedLevel.Evaluate(blockManager.coordonee.y))
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
            if (blockManager.ZP.block.actualBlock.data.blockType == BlockType.transparent 
                && !blockManager.ZP.block.actualBlock.data.isWater)
            {
                blockManager.ZP.block.ChangeBlock("W");
                Despawn();
            }
            else
            {
                blockManager.ZP.block.TakeDamage(OceanManager.instance.damage);
            }
        }
        else if (random == 1 && blockManager.ZN.block)
        {
            if (blockManager.ZN.block.actualBlock.data.blockType == BlockType.transparent 
                && !blockManager.ZN.block.actualBlock.data.isWater)
            {
                blockManager.ZN.block.ChangeBlock("W");
                Despawn();
            }
            else
            {
                blockManager.ZN.block.TakeDamage(OceanManager.instance.damage);
            }
        }
        else if (random == 2 && blockManager.XP.block)
        {
            if (blockManager.XP.block.actualBlock.data.blockType == BlockType.transparent 
                && !blockManager.XP.block.actualBlock.data.isWater)
            {
                blockManager.XP.block.ChangeBlock("W");
                Despawn();
            }
            else
            {
                blockManager.XP.block.TakeDamage(OceanManager.instance.damage);
            }
        }
        else if (random == 3 && blockManager.XN.block)
        {
            if (blockManager.XN.block.actualBlock.data.blockType == BlockType.transparent  
                && !blockManager.XN.block.actualBlock.data.isWater)
            {
                blockManager.XN.block.ChangeBlock("W");
                Despawn();
            }
            else
            {
                blockManager.XN.block.TakeDamage(OceanManager.instance.damage);
            }
        }
        
    }

    void Despawn()
    {
        if (isInfiny) return; 
        
        int random =  UnityEngine.Random.Range(0, 100);
        if(random <= OceanManager.instance.chanceToDespawn) blockManager.RemoveBlock(0);
        
    }
}
