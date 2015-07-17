using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	private GameObject[] bounds;
	private GameState gameState;
	private GrabAndDrop grabber;

	void Start() {
		bounds = GameObject.FindGameObjectsWithTag("Boundary");
		gameState = FindObjectOfType (typeof(GameState)) as GameState;
		grabber = GameObject.Find ("FPSController").GetComponent<GrabAndDrop>();
	}

	void Update () {
		if (gameState.InMainMenu) { return; }

		Vector3 position = gameObject.transform.position;

		if (Input.GetKey((KeyCode)GameControls.Controls.ForwardArrow) || Input.GetKey((KeyCode)GameControls.Controls.Forward)) {
			gameObject.transform.Translate(Vector3.forward * Time.deltaTime);
			boundsCheck(position);
		}

		if (Input.GetKey((KeyCode)GameControls.Controls.BackwardsArrow) || Input.GetKey((KeyCode)GameControls.Controls.Backwards)) {
			gameObject.transform.Translate(Vector3.back * Time.deltaTime);
			boundsCheck(position);
		}

		if (Input.GetKey((KeyCode)GameControls.Controls.RightArrow) || Input.GetKey((KeyCode)GameControls.Controls.Right)) {
			gameObject.transform.Translate(Vector3.right * Time.deltaTime);
			boundsCheck(position);
		}

		if (Input.GetKey((KeyCode)GameControls.Controls.LeftArrow) || Input.GetKey((KeyCode)GameControls.Controls.Left)) {
			gameObject.transform.Translate(Vector3.left * Time.deltaTime);
			boundsCheck(position);
		}

		if (Input.GetKey((KeyCode)GameControls.Controls.Up) || Input.GetKey(KeyCode.JoystickButton16)) {
			gameObject.transform.Translate(Vector3.up * Time.deltaTime);
			boundsCheck(position);
		}

		if(grabber.grabbedObject){ grabber.RepositionObject(); }
	}

	void boundsCheck(Vector3 position) {
		foreach (GameObject boundary in bounds) {
			if (gameObject.GetComponent<CharacterController>().bounds.Intersects(boundary.GetComponent<Renderer>().bounds)) {
				// if intersecting bounds object move back
				gameObject.transform.position = position;
				return;
			}
		}
	}
}

