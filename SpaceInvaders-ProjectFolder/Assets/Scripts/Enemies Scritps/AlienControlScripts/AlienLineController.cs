using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class AlienLineController
{
	public GameObject AlienPrefab;
	List<AlienCommandReceiver> alienList = new List<AlienCommandReceiver>();

	public void SendMoveCommand(Vector3 direction)
	{
		for(int i = 0; i < alienList.Count; i++)
        {
			if (alienList[i] != null)
				alienList[i]?.Move(direction);
			else
				alienList.RemoveAt(i);
        }
	}

	public void AddAlien(GameObject alienInstance)
	{
		if (!alienInstance.TryGetComponent<AlienCommandReceiver>(out AlienCommandReceiver commandReceiver))
			return;

		alienList.Add(commandReceiver);
	}
}
