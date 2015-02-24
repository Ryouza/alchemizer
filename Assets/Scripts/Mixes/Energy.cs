using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {

	public bool tryToPower () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		if (Physics.Raycast (ray, out hit, 100.0f)) {
			if (hit.collider.transform.tag == "Engine") {
				StartCoroutine (power (hit.collider));
				return true;
			}
		}
		
		return false;
	}
	
	IEnumerator power(Collider coll) {
		Engine engine = coll.GetComponent<Engine> ();
		
		coll.renderer.material.color = Color.red;
		engine.currSpeed = engine.moveSpeed;
		
		yield return new WaitForSeconds(engine.timePowered);

		engine.currSpeed = engine.moveSpeed;
		coll.renderer.material.color = Color.magenta;
	}
}
