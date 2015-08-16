using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	private GameObject[] bounds;
	private GrabAndDrop grabber;

	private const float SPEED = 2f;

	void Start() {
		bounds = GameObject.FindGameObjectsWithTag("Boundary");
		grabber = GameObject.Find ("FPSController").GetComponent<GrabAndDrop>();
	}

	void Update () {
		if (GameState.InMainMenu) { return; }

		Vector3 position = gameObject.transform.position;

		if (Input.GetButton("MoveForward")) {
			gameObject.transform.Translate(Vector3.forward * Time.deltaTime * SPEED);
			boundsCheck(position);
		}

		if (Input.GetButton("MoveBack")) {
			gameObject.transform.Translate(Vector3.back * Time.deltaTime * SPEED);
			boundsCheck(position);
		}

		if (Input.GetButton("MoveRight")) {
			gameObject.transform.Translate(Vector3.right * Time.deltaTime * SPEED);
			boundsCheck(position);
		}

		if (Input.GetButton("MoveLeft")) {
			gameObject.transform.Translate(Vector3.left * Time.deltaTime * SPEED);
			boundsCheck(position);
		}

		if (Input.GetButton("Up")) {
			gameObject.transform.Translate(Vector3.up * Time.deltaTime * SPEED);
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

