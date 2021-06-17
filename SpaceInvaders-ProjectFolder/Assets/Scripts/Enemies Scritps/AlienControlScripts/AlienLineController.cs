using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class AlienLineController
{
	public GameObject AlienPrefab;
	public event Action<Vector3> OnMoveCommand;

	public void MoveLine(Vector3 direction)
	{
		OnMoveCommand?.Invoke(direction);
	}

	public void AddAlien(GameObject alienInstance)
	{
		if (!alienInstance.TryGetComponent<AlienCommandReceiver>(out AlienCommandReceiver commandReceiver))
			return;

		commandReceiver.AssignController(this);
	}
}
