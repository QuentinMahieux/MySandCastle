using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SeedGenerator : MonoBehaviour
{
    public static SeedGenerator instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("[SeedGenerator] instance already exists");
            Destroy(gameObject);
        }
    }

    public string Encode(Vector3 size, string seed)
    {
        string code = "[" + size.x + "]" + "[" + size.y + "]" + "[" + size.z + "]" + seed;
        return code;
    }
    
    public bool Decode(string code, out int x, out int y, out int z, out string seed)
    {
        code = code.Trim();
        var match = Regex.Match(code, @"^\[(?<sizeX>\d+)\]\[(?<sizeY>\d+)\]\[(?<sizeZ>\d+)\](?<seed>[A-Z]+)$");

        if (!match.Success)
        {
            x = y = z = 0;
            seed = null;
            
            Debug.Log("[SeedGenerator] code could not be decoded");
            return false;
        }

        x    = int.Parse(match.Groups["sizeX"].Value);
        y    = int.Parse(match.Groups["sizeY"].Value);
        z    = int.Parse(match.Groups["sizeZ"].Value);
        seed = match.Groups["seed"].Value;

        Debug.Log("[SeedGenerator] code decoded successfully");
        return true;
    }

    public string CreateSeed(Dictionary<Vector3Int, BlockManager> blocks)
    {
        string seed = "";
        foreach (var block in blocks)
        {
            seed += block.Value.actualBlock.data.id;
        }
        return seed;
    }
}

public enum seedInformation
{
    sizeX,
    sizeY,
    sizeZ,
    data
}