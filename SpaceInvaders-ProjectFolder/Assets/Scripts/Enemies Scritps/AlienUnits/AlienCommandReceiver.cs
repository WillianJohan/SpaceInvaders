using UnityEngine;

public class AlienCommandReceiver : MonoBehaviour
{
    [SerializeField] AlienMeshFilterController meshFilterController;

    public void Move(Vector3 direction)
    {
        transform.position = transform.position + direction;
        meshFilterController?.ChangeMesh();
    }

}
