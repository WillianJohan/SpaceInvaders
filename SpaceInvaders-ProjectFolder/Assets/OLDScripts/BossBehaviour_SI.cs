using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour_SI : MonoBehaviour {

	public GameObject referenceStart;
	public GameObject referenceEnd;
	private float vel = 6f;
	private Rigidbody2D rb2d;

	void Start () {
		transform.position = referenceStart.transform.position;
		rb2d = GetComponent<Rigidbody2D> ();
		BossScript_SI.CanSpawnBoss = false;
	}

	int Point () {
		return (int) Random.Range (100f, 500f);
	}

	void FixedUpdate () {
		rb2d.velocity = Vector3.right * vel;
	}

	private void OnTriggerEnter2D (Collider2D other) {

		if (other.gameObject.tag == "Shot") {
			FindObjectOfType<ManagerGame_SI> ().AddPoints (Point (), true);
			BossScript_SI.CanSpawnBoss = true;
			GameObject.Destroy (GameObject.Find ("Boss(Clone)"));
		} else if (other == referenceEnd.GetComponent<Collider2D> ()) {
			BossScript_SI.CanSpawnBoss = true;
			GameObject.Destroy (GameObject.Find ("Boss(Clone)"));
		}

	}

}