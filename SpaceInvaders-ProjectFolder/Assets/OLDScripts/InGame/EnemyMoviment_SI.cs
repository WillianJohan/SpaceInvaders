using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoviment_SI : MonoBehaviour {

	private bool isMoving_X;
	private bool isMoving_Y;

	public static bool needTransition;
	public static bool DirectcionRight;

	public int DirectionMoviment;
	public GameObject[] lineList;
	public static float distanceMovement;
	private float distanceMovement_;
	public static float frequencyMovement;

	private float DistanceEachLine = 1.13f;

	void Start () {
		isMoving_X = false;
		isMoving_Y = false;
		needTransition = false;
		DirectcionRight = true;
		DirectionMoviment = 1;
	}

	void Update () {
		if (!isMoving_X) {
			distanceMovement_ = distanceMovement;
			if (!isMoving_Y && !needTransition) {
				StartCoroutine (MoveX ());
			} else if (!isMoving_Y && needTransition) {
				StartCoroutine (MoveY ());
			}
		}
	}

	public void ResetLines () {
		for (int i = 0; i < lineList.Length; i++) {
			Vector3 new_LinePosition = new Vector3 (transform.position.x, transform.position.y - (DistanceEachLine * i), transform.position.z);
			lineList[i].transform.position = new_LinePosition;
		}
	}

	IEnumerator MoveX () {
		isMoving_X = true;

		if (DirectcionRight) {
			DirectionMoviment = 1;
		} else {
			DirectionMoviment = -1;
		}

		for (int i = lineList.Length - 1; i >= 0; i--) {
			Vector3 nextPos = new Vector3 (lineList[i].transform.position.x + (distanceMovement_ * DirectionMoviment), lineList[i].transform.position.y, lineList[i].transform.position.z);
			lineList[i].transform.position = nextPos;
			EnemyType_SI scriptMesh = lineList[i].GetComponent<EnemyType_SI> ();
			scriptMesh.NextStep ();
			yield return new WaitForSeconds (frequencyMovement);
		}
		isMoving_X = false;
	}

	IEnumerator MoveY () {
		isMoving_Y = true;
		for (int i = lineList.Length - 1; i >= 0; i--) {
			Vector3 nextPos = new Vector3 (lineList[i].transform.position.x, lineList[i].transform.position.y - distanceMovement_, lineList[i].transform.position.z);
			lineList[i].transform.position = nextPos;
			yield return new WaitForSeconds (frequencyMovement);
		}
		needTransition = false;
		isMoving_Y = false;
	}

}