using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public string 		nextLevel;
	
	void Start () {
		this.renderer.material.color = Color.green;
	}

	void OnTriggerEnter(Collider that) {
 		if (that.gameObject.tag == "Player") {
			Application.LoadLevel (nextLevel);
		}
	}
}
