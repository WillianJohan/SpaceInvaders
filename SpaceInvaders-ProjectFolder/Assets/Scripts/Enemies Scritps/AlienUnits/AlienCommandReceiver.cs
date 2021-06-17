using UnityEngine;

public class AlienCommandReceiver : MonoBehaviour
{
    [SerializeField] AlienMeshFilterController meshFilterController;
    AlienLineController lineController;

    private void OnDestroy()
    {
        lineController.OnMoveCommand -= HandleMoveCommand;
    }

    public void AssignController(AlienLineController lineController)
    {
        this.lineController = lineController;
        this.lineController.OnMoveCommand += HandleMoveCommand;
    }

    public void HandleMoveCommand(Vector3 direction)
    {
        transform.position = transform.position + direction;
        meshFilterController?.ChangeMesh();
    }

}
