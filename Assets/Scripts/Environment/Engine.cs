using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {

	public float			timePowered;
	public float			moveSpeed;

	private float			powerStart;
	public float			currSpeed;

	public Vector3			offset;
	
	private Vector3			start;
	private bool			toDest;

	// Use this for initialization
	void Start () {
		renderer.material.color = Color.magenta;
		currSpeed = 0;
		toDest = true;
		start = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (toDest) {
			transform.position = Vector3.MoveTowards (transform.position, start + offset, currSpeed * Time.deltaTime);
			if (transform.position == start + offset) {
				toDest = false;
				currSpeed = 0;
			}
		} else {
			transform.position = Vector3.MoveTowards (transform.position, start, currSpeed * Time.deltaTime);
			if (transform.position == start) {
				toDest = true;
				currSpeed = 0;
			}
		}
	}
}
