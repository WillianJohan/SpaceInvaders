using UnityEngine;

public class AlienLineControl : MonoBehaviour
{
    public GameObject AlienPrefab;

    public void MoveAliens(Vector3 direction)
    {
        AlienCommandReceiver[] aliens = GetComponentsInChildren<AlienCommandReceiver>();
        for (int i = 0; i < aliens.Length; i++)
            aliens[i].Move(direction);
    }
}
