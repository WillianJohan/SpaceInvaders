using UnityEngine;

public class ChangeMeshOvertime : MonoBehaviour
{
    [SerializeField] Mesh[] meshes;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] float time = .4f;

    float currentTime = 0;
    int currentMesh = 0;

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime < time)
            return;

        currentTime = 0;

        currentMesh = (currentMesh + 1) % 2;
        meshFilter.mesh = meshes[currentMesh];
    }
}
