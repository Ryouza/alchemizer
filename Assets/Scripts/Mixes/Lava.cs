using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {
	
	public bool tryToMelt () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit, 100.0f)) {
			if (hit.collider.transform.tag == "Ice") {
				Vector3 temp = hit.collider.transform.localScale;
				temp.x *= 0.25f;
				temp.y *= 0.25f;
				temp.z *= 0.25f;
				hit.collider.transform.localScale = temp;
				return true;
			}
		}
		
		return false;
	}
}
