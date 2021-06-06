using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class AlienMeshFilterController : MonoBehaviour
{
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] Mesh[] meshes;
    
    int currentMeshIndex = 0;

    void Start() => meshFilter.mesh = meshes[currentMeshIndex];

    public void ChangeMesh()
    {
        currentMeshIndex = (currentMeshIndex + 1) % 2;
        meshFilter.mesh = meshes[currentMeshIndex];
    }
}
