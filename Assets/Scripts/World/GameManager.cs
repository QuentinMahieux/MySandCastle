using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public bool creative = false;
    
    [Header("Blocks")]
    public BlockData currentBlockData;
    public Vector3 currentCoordonee =  new Vector3(0,0,0);
    public BlockData[] blocks;

    [Header("Settings")] 
    public float distanceToPlaceBlock = 5f;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("[GameManager] instance already exists");
            Destroy(gameObject);
        }
    }
}
