using UnityEngine;
using System.Collections;

public class Quicksand : MonoBehaviour {

	private Color 		orange;

	void Start () {
		orange = new Color (1.0f, 0.45f, 0.125f, 0.5f);
	}
	
	public bool tryToLiquefy () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit, 100.0f)) {
			if (hit.collider.transform.tag == "Dirt") {
				hit.collider.renderer.material.color = orange;
				hit.collider.enabled = false;
				return true;
			}
		}
		
		return false;
	}
}
