using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {

	public float 			spinSpeed; 
	public float			moveSpeed;
	public Vector3			offset;

	private Vector3			start;
	private bool			toDest;

	void Start () {
		start = transform.position;
		toDest = true;
	}

	void Update () {
		transform.Rotate (Vector3.forward, Time.deltaTime * spinSpeed);
		if (toDest) {
			transform.position = Vector3.MoveTowards (transform.position, start + offset, moveSpeed * Time.deltaTime);
			if (transform.position == start + offset) {
				toDest = false;
			}
		} else {
			transform.position = Vector3.MoveTowards (transform.position, start, moveSpeed * Time.deltaTime);
			if (transform.position == start) {
				toDest = true;
			}
		}
	}

	void OnTriggerEnter(Collider that) {
		if (that.gameObject.tag == "Player") {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
