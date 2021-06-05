using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameScreen {
	Menu,
	InGame,
	GameOver
}

public class ManagerGame_SI : MonoBehaviour {

	/*
	***Passos***

	-inGame
		-New Wave

			Reseta valor de velocidade

		*/

	[Header ("Elementos para Iniciar o game")]
	public float spawnGameVelocity;
	public GameObject go_GAME;
	public GameObject go_Player;
	public GameObject go_Barreiras;
	public GameObject go_Enemies;

	[Space (15)]
	[Header ("Telas")]
	public GameObject GameOverScreen;
	public GameObject PauseScreen;
	public KeyCode pauseButton = KeyCode.Escape;

	[Space (15)]
	[Header ("Texto na tela")]
	public TextMesh Score_Text;
	public TextMesh Life_Text;

	//***********************************//

	[HideInInspector] public static bool FirstLineDown;
	[HideInInspector] public static bool gameIsRunning = false;
	[HideInInspector] public static bool canPause = false;
	[HideInInspector] public static bool isPaused = false;
	[HideInInspector] public static int currentEnemyCount;
	[HideInInspector] public static int enemiesLenght;
	[HideInInspector] public static int currentLife_Player;
	[HideInInspector] public static int currentScore;
	private int MaxLife_Player = 3;
	private int MinLife_Player = 0;
	private int HI_Score;
	private int WaveCount = 1;

	void Awake () {
		isPaused = false;
		PauseScreen.SetActive (isPaused);
		Cursor.visible = isPaused;
		currentScore = 0;
		enemiesLenght = 0;
		WaveCount = 1;
		currentLife_Player = MaxLife_Player;
		gameIsRunning = false;
		Life_Text.text = "Life: " + MaxLife_Player;
		Score_Text.text = "Score: " + currentScore + "x";
	}

	void Start () {
		StartCoroutine (NewGameSpawn ());
	}

	void Update () {
		if (Input.GetKeyDown (pauseButton) && canPause) {
			PauseGame ();
		}
	}

	public void GoTo (string nameScene) {
		Application.LoadLevel (nameScene);
	}

	public void PauseGame () {
		isPaused = !isPaused;
		PauseScreen.SetActive (isPaused);
		Cursor.visible = isPaused;
		if (isPaused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void NewWave () {
		FindObjectOfType<EnemyMoviment_SI> ().ResetLines ();

		Barreira_SI[] barreira = FindObjectsOfType<Barreira_SI> ();
		for (int i = 0; i < barreira.Length; i++) {
			barreira[i].gameObject.SetActive (true);
		}

		GameObject barreiras = GameObject.Find ("BarreiraPrefab(Clone)");
		GameObject.Destroy (barreiras);

		Instantiate (go_Barreiras, new Vector3 (0, -1.78f, 0), Quaternion.identity).transform.parent = go_GAME.transform;

		GameObject Spawn = Instantiate (go_Enemies, Vector3.zero, Quaternion.identity);
		Spawn.transform.parent = go_GAME.transform;

		StartCoroutine (FindObjectOfType<SpawnEnemies_SI> ().Spawnar ());
		WaveCount++;
	}

	public void AddPoints (int _point, bool isBossPoint) {
		if (!isBossPoint) {
			currentEnemyCount--;
			FrequencyEnemy ();
		}
		currentScore += _point;
		Score_Text.text = "Score: " + currentScore + "x";
		if (currentEnemyCount <= 0) {
			NewWave ();
		}
	}

	public void AddDamage () {
		currentLife_Player--;
		Life_Text.text = "Life: " + currentLife_Player + "x";

		if (currentLife_Player <= MinLife_Player) {
			GameObject.Destroy (FindObjectOfType<Player_SI> ().gameObject);
			StartCoroutine (GameOver ());
		}
	}

	public void FrequencyEnemy () {

		switch (currentEnemyCount) {
			case 54:
				EnemyMoviment_SI.frequencyMovement = 0.1f;
				EnemyMoviment_SI.distanceMovement = 0.2f;
				break;
			case 50:
				EnemyMoviment_SI.frequencyMovement = 0.09f;
				EnemyMoviment_SI.distanceMovement = 0.25f;
				break;
			case 40:
				EnemyMoviment_SI.frequencyMovement = 0.08f;
				EnemyMoviment_SI.distanceMovement = 0.28f;
				break;
			case 30:
				EnemyMoviment_SI.frequencyMovement = 0.05f;
				EnemyMoviment_SI.distanceMovement = 0.3f;
				break;
			case 20:
				EnemyMoviment_SI.frequencyMovement = 0.04f;
				EnemyMoviment_SI.distanceMovement = 0.36f;
				break;
			case 15:
				EnemyMoviment_SI.frequencyMovement = 0.03f;
				EnemyMoviment_SI.distanceMovement = 0.45f;
				break;
			case 10:
				EnemyMoviment_SI.frequencyMovement = 0.025f;
				EnemyMoviment_SI.distanceMovement = 0.6f;
				break;
			case 3:
				EnemyMoviment_SI.frequencyMovement = 0.007f;
				EnemyMoviment_SI.distanceMovement = 0.7f;
				break;
			case 1:
				EnemyMoviment_SI.frequencyMovement = 0.001f;
				EnemyMoviment_SI.distanceMovement = 1f;
				break;
			default:
				break;
		}
	}

	public IEnumerator GameOver () {
		if (currentScore > PlayerPrefs.GetInt ("HiScore_SI")) {
			PlayerPrefs.SetInt ("HiScore_SI", currentScore);
		}
		yield return new WaitForSeconds (0.3f);
		GameObject.Destroy (go_GAME);
		Instantiate (GameOverScreen, Vector3.zero, Quaternion.identity);
		yield return null;
	}

	IEnumerator NewGameSpawn () {
		yield return new WaitForSeconds (0.3f);

		Instantiate (go_Player, new Vector3 (0, -1.11f, 0), Quaternion.identity).transform.parent = go_GAME.transform;
		Player_SI.canMove = gameIsRunning;
		Player_SI.canShot = gameIsRunning;
		yield return new WaitForSeconds (spawnGameVelocity);

		Instantiate (go_Barreiras, new Vector3 (0, -1.78f, 0), Quaternion.identity).transform.parent = go_GAME.transform;
		yield return new WaitForSeconds (spawnGameVelocity);

		Instantiate (go_Enemies, Vector3.zero, Quaternion.identity).transform.parent = go_GAME.transform;
		StartCoroutine (FindObjectOfType<SpawnEnemies_SI> ().Spawnar ());

		yield return null;
	}
}