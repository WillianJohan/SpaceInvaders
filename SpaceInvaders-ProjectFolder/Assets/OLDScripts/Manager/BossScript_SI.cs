using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript_SI : MonoBehaviour {

	public GameObject boss;
	public static bool CanSpawnBoss;

	void Start () {
		CanSpawnBoss = false;
	}

	void Update () {
		if (CanSpawnBoss && ManagerGame_SI.FirstLineDown) {
			CanSpawnBoss = false;
			StartCoroutine (Spawn ());
		}
	}

	float Delay () {
		return Random.Range (30f, 60f);
	}

	IEnumerator Spawn () {
		yield return new WaitForSeconds (Delay ());
		SpawnBoss();
	}

	public void SpawnBoss () 
	{
		Instantiate (boss, transform.position, Quaternion.identity).transform.parent = GameObject.Find ("Game").transform;
	}

}