using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public static OceanManager instance;

    public float[] speedLevel;
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

