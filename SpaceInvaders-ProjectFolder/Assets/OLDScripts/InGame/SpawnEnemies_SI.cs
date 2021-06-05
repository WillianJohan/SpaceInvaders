using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies_SI : MonoBehaviour {

	public EnemyType_SI[] Lines;

	public float espacoEntreOsInimigos;
	public int QuantidadeInimigo = 11;
	public EnemyMoviment_SI ScriptMovimento_Enemy;

	public IEnumerator Spawnar () {
		ManagerGame_SI.FirstLineDown = false;
		BossScript_SI.CanSpawnBoss = false;
		ManagerGame_SI.canPause = false;
		ScriptMovimento_Enemy = FindObjectOfType<EnemyMoviment_SI> ();
		ScriptMovimento_Enemy.enabled = false;
		Player_SI.canShot = false;


		for (int i = Lines.Length - 1; i >= 0; i--) {
			for (int b = 0; b < QuantidadeInimigo; b++) {
				Transform LineTransform = Lines[i].GetComponent<Transform> ();
				Vector3 spawnPosition = new Vector3 (LineTransform.position.x + (b * espacoEntreOsInimigos), LineTransform.position.y, LineTransform.position.z);
				var spawnAsChild = Instantiate (Lines[i].enemyPrefab, spawnPosition, Quaternion.identity);
				spawnAsChild.transform.parent = Lines[i].gameObject.transform;
				ManagerGame_SI.enemiesLenght++;
				yield return new WaitForSeconds (0.02f);
			}
			yield return new WaitForSeconds (0.05f);
		}

		if (!ManagerGame_SI.gameIsRunning) {
			ManagerGame_SI.gameIsRunning = true;
			Player_SI.canMove = true;
		}
		Player_SI.canShot = true;

		ManagerGame_SI.currentEnemyCount = ManagerGame_SI.enemiesLenght;
		ScriptMovimento_Enemy.DirectionMoviment = 1;
		EnemyMoviment_SI.DirectcionRight = true;
		EnemyMoviment_SI.needTransition = false;
		ScriptMovimento_Enemy.enabled = true;
		ManagerGame_SI.canPause = true;
		EnemyMoviment_SI.frequencyMovement = 0.1f;
		EnemyMoviment_SI.distanceMovement = 0.2f;
	}
}