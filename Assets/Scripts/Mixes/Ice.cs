using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour {

	public float	freezeLength;

	public bool tryToFreeze () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, 100.0f)) {
			if (hit.collider.transform.tag == "Spinner") {
				StartCoroutine (freeze (hit.collider));
				return true;
			}
		}

		return false;
	}

	IEnumerator freeze(Collider coll) {
		Spinner spinner = coll.GetComponent<Spinner> ();

		coll.renderer.material.color = Color.cyan;

		float tempMove = spinner.moveSpeed;
		float tempSpin = spinner.spinSpeed;
		spinner.moveSpeed = 0;
		spinner.spinSpeed = 0;
		spinner.tag = "Ice";

		yield return new WaitForSeconds(freezeLength);
		spinner.moveSpeed = tempMove;
		spinner.spinSpeed = tempSpin;
		coll.renderer.material.color = Color.white;
		spinner.tag = "Spinner";
	}
}
