using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public static OceanManager instance;

    [Header("Settings")]
    public AnimationCurve speedLevel;
    [Range(0, 100)]
    public int chanceToDespawn = 10;
    public int damage = 1;
    
    [Header("Information")]
    public int currentWaterLevel;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("[OceanManager] instance already exists");
            Destroy(gameObject);
        }
    }
}

