using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour_SI : MonoBehaviour {

	private Rigidbody2D rb2d;
	private float velocityProjectile = 20.0f;
	public bool is_PlayerProjectile;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		Renderer material = GetComponent<Renderer> ();

		switch (is_PlayerProjectile) {
			case true:
				rb2d.velocity = new Vector2 (0, velocityProjectile);
				material.material.color = Color.green;
				break;
			case false:
				rb2d.velocity = new Vector2 (0, -velocityProjectile);
				material.material.color = Color.red;
				break;
		}

		GameObject.Destroy (this.gameObject, 5f);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Barreira") {
			GameObject.Destroy (other.gameObject);
		}
		GameObject.Destroy (this.gameObject);
	}
}