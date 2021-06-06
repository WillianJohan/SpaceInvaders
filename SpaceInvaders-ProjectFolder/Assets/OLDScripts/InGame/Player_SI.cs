using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_SI : MonoBehaviour {

	//Controll Options
	[Header ("Variaveis para controle")]
	[HideInInspector] public static bool canShot;
	[HideInInspector] public static bool canMove;

	private float velocityMouse = 35;
	public float DelayEachShot = 0.2f;

	[Header ("Game Object para Tiro")]
	public GameObject Referencia;
	public GameObject shotPrefab;

	//variaveis privadas
	private Rigidbody2D rb2d;
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		canMove = true;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0) && canShot && !ManagerGame_SI.isPaused) {
			Shot ();
			StartCoroutine (DelayShot ());
		}
		if (canMove) {
			rb2d.velocity = new Vector2 (Input.GetAxis ("Mouse X") * velocityMouse, 0);
		}

	}

	IEnumerator DelayShot () {
		canShot = false;
		yield return new WaitForSeconds (DelayEachShot);
		canShot = true;
	}

	private void Shot () {
		Instantiate (shotPrefab, Referencia.transform.position, Quaternion.identity).transform.parent = GameObject.Find("Game").transform;
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Shot") {
			FindObjectOfType<ManagerGame_SI> ().AddDamage ();
		} else if (other.gameObject.tag == "Enemy") {
			StartCoroutine(FindObjectOfType<ManagerGame_SI> ().GameOver ());
		}
	}

}