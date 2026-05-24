using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class VisualInterraction : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private Material[] originalMaterials;
    public Material outlineMaterial;
    private bool isGlow;
    

    void Awake()
    {
        originalMaterials =  meshRenderer.materials;
    }
    
    public virtual void ActiveOutLine()
    {
        if (isGlow) { return; }
        isGlow = true;
        
        Material[] curentMaterials = meshRenderer.materials;
        Material[] newMaterials = new Material[curentMaterials.Length + 1];
        for (int i = 0; i < curentMaterials.Length; i++)
        {
            newMaterials[i] = curentMaterials[i];
        }
        newMaterials[curentMaterials.Length] = outlineMaterial;
        meshRenderer.materials = newMaterials;
    }

    public virtual void DesactiveOutLine()
    {
        isGlow = false;
        meshRenderer.materials = originalMaterials;
    }
}
