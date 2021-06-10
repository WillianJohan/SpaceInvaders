using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class AlienLineController
{
	public GameObject AlienPrefab;
	List<AlienCommandReceiver> alienList;

	public void SendMoveCommand(Vector3 direction)
	{
		foreach (AlienCommandReceiver alien in alienList)
			alien?.Move(direction);
	}

	public void AddAlien(GameObject alienInstance)
	{
		if (!alienInstance.TryGetComponent<AlienCommandReceiver>(out AlienCommandReceiver commandReceiver))
			return;

		alienList.Add(commandReceiver);
	}
}
