using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	public string		nextLevel;
	public string		prevLevel;

	public int			fireStock;
	public int			airStock;
	public int			waterStock;
	public int			earthStock;
	public float		speed;

	private Vector3		dest;
	private float		moveStart;

	private string		currMix;
	private bool		leftPower;

	private Color		leftColor;
	private Color		rightColor;

	private Color		brown;

	private Ice			ice;
	private Quicksand	quicksand;
	private Lava		lava;
	private Energy		energy;
	
	void Start () {
		dest = transform.position;
		leftColor = Color.black;
		rightColor = Color.black;
		leftPower = true;
		brown = new Color (0.5f, 0.25f, 0);
		currMix = "Not a Mix!";

		ice = this.GetComponent<Ice> ();
		quicksand = this.GetComponent<Quicksand> ();
		lava = this.GetComponent<Lava> ();
		energy = this.GetComponent<Energy> ();
	}

	void Update () {
		CheckForNewMix ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			CreateMix ();
		}

		if (Input.GetMouseButtonDown (0)) {
			dest = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			dest.z = 0;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}

		if (Input.GetKeyDown (KeyCode.J)) {
			Application.LoadLevel (prevLevel);
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			Application.LoadLevel (nextLevel);
		}

		if (Input.GetMouseButtonDown (1)) {
			if (currMix == "Ice") {
				if (waterStock > 0 && airStock > 0) {
					if (ice.tryToFreeze ()) {
						waterStock--;
						airStock--;
					}
				}
			}
			else if (currMix == "Quicksand") {
				if (waterStock > 0 && earthStock > 0) {
					if (quicksand.tryToLiquefy()) {
						waterStock--;
						earthStock--;
					}
				}
			}
			else if (currMix == "Lava") { 
				if (fireStock > 0 && earthStock > 0) {
					if (lava.tryToMelt()) {
						fireStock--;
						earthStock--;
					}
				}
			}
			else if (currMix == "Energy") { 
				if (fireStock > 0 && airStock > 0) {
					if (energy.tryToPower()) {
						fireStock--;
						airStock--;
					}
				}
			}
		}

		transform.position = Vector3.MoveTowards (transform.position, dest, speed * Time.deltaTime);
	}

	void OnGUI () {
		DrawMixRect (leftColor, rightColor);
		DrawResources ();
		GUI.skin.label.alignment = TextAnchor.UpperCenter;
		GUI.Label(new Rect(Screen.width * 0.46f, Screen.height * 0.01f, Screen.width * 0.08f, Screen.height * 0.5f),"Current Mix");
		GUI.Label(new Rect(Screen.width * 0.46f, Screen.height * 0.06f, Screen.width * 0.08f, Screen.height * 0.25f), currMix);
		GUI.skin.label.alignment = TextAnchor.UpperLeft;
	}
	
	void DrawMixRect (Color leftColor, Color rightColor) {
		GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.01f, Screen.width, Screen.height),"Mix Pool");

		Texture2D text = new Texture2D (1,1);
		GUIStyle style = new GUIStyle();

		text.SetPixel (0, 0, leftColor);
		text.Apply ();

		style.normal.background = text;
		GUI.Box (new Rect (Screen.width / 32, Screen.height / 16, Screen.width * 0.04f, Screen.height * 0.06f), GUIContent.none, style);

		text.SetPixel (0, 0, rightColor);
		text.Apply ();

		style.normal.background = text;
		GUI.Box (new Rect ((Screen.width / 32) + (Screen.width * 0.05f), Screen.height / 16, Screen.width * 0.04f, Screen.height * 0.06f), GUIContent.none, style);
	}

	void DrawResources () {
		GUI.Label(new Rect(Screen.width * 0.843f, Screen.height * 0.01f, Screen.width, Screen.height),"Resources");

		Texture2D text = new Texture2D (1, 1);
		GUIStyle style = new GUIStyle ();

		text.SetPixel (0, 0, Color.red);
		text.Apply ();
		style.normal.background = text;
		GUI.Box (new Rect (Screen.width * 0.78f, Screen.height / 16, Screen.width * 0.04f, Screen.height * 0.06f), new GUIContent(fireStock.ToString()), style);

		text.SetPixel (0, 0, brown);
		text.Apply ();
		style.normal.background = text;
		GUI.Box (new Rect ((Screen.width * 0.78f) + (Screen.width * 0.05f), Screen.height / 16, Screen.width * 0.04f, Screen.height * 0.06f), new GUIContent(earthStock.ToString()), style);

		text.SetPixel (0, 0, Color.blue);
		text.Apply ();
		style.normal.background = text;
		GUI.Box (new Rect ((Screen.width * 0.78f) + (Screen.width * 0.1f), Screen.height / 16, Screen.width * 0.04f, Screen.height * 0.06f), new GUIContent(waterStock.ToString()), style);

		text.SetPixel (0, 0, Color.white);
		text.Apply ();
		style.normal.background = text;
		GUI.Box (new Rect ((Screen.width * 0.78f) + (Screen.width * 0.15f), Screen.height / 16, Screen.width * 0.04f, Screen.height * 0.06f), new GUIContent(airStock.ToString()), style);
	}

	void CheckForNewMix () {
		if (Input.GetKeyDown (KeyCode.A)) {
			if (leftPower) {
				leftColor = Color.red;
			} else {
				rightColor = Color.red;
			}
			leftPower = !leftPower;
		}
		else if (Input.GetKeyDown (KeyCode.S)) {
			if (leftPower) {
				leftColor = brown;
			} else {
				rightColor = brown;
			}
			leftPower = !leftPower;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			if (leftPower) {
				leftColor = Color.blue;
			} else {
				rightColor = Color.blue;
			}
			leftPower = !leftPower;
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			if (leftPower) {
				leftColor = Color.white;
			} else {
				rightColor = Color.white;
			}
			leftPower = !leftPower;
		}
	}

	void CreateMix () {
		currMix = "Not a Mix!";
		if (leftColor == Color.red) {
			if (rightColor == brown) {
				currMix = "Lava";
			} else if (rightColor == Color.white) {
				currMix = "Energy";
			}
		} else if (leftColor == brown) {
			if (rightColor == Color.red) {
				currMix = "Lava";
			} else if (rightColor == Color.blue) {
				currMix = "Quicksand";
			}
		} else if (leftColor == Color.blue) {
			if (rightColor == brown) {
				currMix = "Quicksand";
			} else if (rightColor == Color.white) {
				currMix = "Ice";
			}
		} else if (leftColor == Color.white) {
			if (rightColor == Color.blue) {
				currMix = "Ice";
			} else if (rightColor == Color.red) {
				currMix = "Energy";
			} 
		} 
		Debug.Log (currMix);
	}
}