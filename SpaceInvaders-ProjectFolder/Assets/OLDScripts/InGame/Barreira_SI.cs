using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreira_SI : MonoBehaviour {

	public GameObject otherBarreira;

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Enemy") {
			if (!ManagerGame_SI.FirstLineDown) {
				ManagerGame_SI.FirstLineDown = true;
				BossScript_SI.CanSpawnBoss = ManagerGame_SI.FirstLineDown;
				BossScript_SI.FindObjectOfType<BossScript_SI>().SpawnBoss();
			}
			EnemyMoviment_SI.needTransition = true;
			EnemyMoviment_SI.DirectcionRight = !EnemyMoviment_SI.DirectcionRight;
			otherBarreira.SetActive (true);
			gameObject.SetActive (false);
		}
	}

}