using UnityEngine;
using System.Collections;

public class Dirt : MonoBehaviour {
	
	void Start () {
		this.renderer.material.color = new Color(0.5f, 0.25f, 0.05f);
	}
}
