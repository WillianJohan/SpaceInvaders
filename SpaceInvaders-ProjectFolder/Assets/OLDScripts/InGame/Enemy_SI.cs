using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SI : MonoBehaviour {

	public GameObject ShotPrefab;
	public GameObject reference;
	public int Point;
	public Mesh[] Graphic;
	private MeshFilter MainMesh;
	private int indexMesh;
	private bool canShot;
	private bool facedToPlayer;
	private bool WaitingToCheck;
	private float Delay;

	void Start () {
		canShot = false;
		facedToPlayer = false;
		WaitingToCheck = false;
		MainMesh = GetComponentInChildren<MeshFilter> ();
		MainMesh.mesh = Graphic[0];
		Delay = Random.Range (2.0f, 4.0f);
	}

	public void NextMesh () {
		indexMesh++;
		if (indexMesh > Graphic.Length - 1) {
			indexMesh = 0;
		}

		MainMesh.mesh = Graphic[indexMesh];
	}

	void Update () {
		if (canShot && facedToPlayer && !WaitingToCheck) {
			StartCoroutine (DelayToShot ());
		} else if (!canShot && !facedToPlayer && !WaitingToCheck) {
			StartCoroutine (CheckToShot ());
		}
	}

	IEnumerator CheckToShot () {
		WaitingToCheck = true;
		yield return new WaitForSeconds (2.0f);
		RaycastHit2D hit = Physics2D.Raycast (reference.transform.position, -Vector2.up);
		if (hit.collider == null || hit.transform.tag != "Enemy") {
			yield return new WaitForSeconds (Delay);
			facedToPlayer = true;
			canShot = true;
		}
		WaitingToCheck = false;
	}

	IEnumerator DelayToShot () {
		canShot = false;
		Instantiate (ShotPrefab, reference.transform.position, Quaternion.identity).transform.parent = GameObject.Find ("Game").transform;
		yield return new WaitForSeconds (Delay);
		canShot = true;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Shot") {
			FindObjectOfType<ManagerGame_SI> ().AddPoints (Point,false);
			GameObject.Destroy (this.gameObject);
		} else if (other.gameObject.tag == "Barreira") {
			GameObject.Destroy (other.gameObject);
		}
	}

}