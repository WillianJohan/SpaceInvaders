using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType_SI : MonoBehaviour {
	public GameObject enemyPrefab;


	public void NextStep()
	{
		Enemy_SI[] script = GetComponentsInChildren<Enemy_SI>();
		for(int i = 0; i < script.Length; i++){
			script[i].NextMesh();
		}
	}

}
