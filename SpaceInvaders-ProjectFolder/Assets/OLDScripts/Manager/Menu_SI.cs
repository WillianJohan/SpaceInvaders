using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_SI : MonoBehaviour {

	public GameObject[] InstanciarPorVez;
	public float DelayTotal = 2f;
	public GameObject pressAnyKey_GO;
	private bool canPlay;

	public string NomeScena;

	public TextMesh HiScore_Text;
	public TextMesh CurrentScore_Text;

	// Use this for initialization
	void Start () {
		canPlay = false;

		if (!PlayerPrefs.HasKey ("HiScore_SI")){
			PlayerPrefs.SetInt ("HiScore_SI", 0000);
		}
		HiScore_Text.text = "Hi-Score: " + PlayerPrefs.GetInt ("HiScore_SI");
		if (CurrentScore_Text != null) {
			CurrentScore_Text.text = "You Score: " + ManagerGame_SI.currentScore;
		}

		for (int i = 0; i < InstanciarPorVez.Length; i++) {
			InstanciarPorVez[i].SetActive (false);
		}
		pressAnyKey_GO.SetActive (false);
		StartCoroutine (GameIniciou ());
	}

	void Update () {
		if (Input.anyKeyDown && canPlay) {
			canPlay = false;
			StartCoroutine (Play ());
		}

	}

	IEnumerator Play () {
		pressAnyKey_GO.SetActive (false);
		yield return new WaitForSeconds (0.3f);
		Application.LoadLevel (NomeScena);
	}

	IEnumerator GameIniciou () {
		float delay = (DelayTotal / InstanciarPorVez.Length);
		for (int i = 0; i < InstanciarPorVez.Length; i++) {
			InstanciarPorVez[i].SetActive (true);
			yield return new WaitForSeconds (delay);
		}
		pressAnyKey_GO.SetActive (true);
		canPlay = true;
		yield return null;
	}
}