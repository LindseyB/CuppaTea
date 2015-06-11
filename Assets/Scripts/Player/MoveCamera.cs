using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	private GameObject[] bounds;

	void Start() {
		bounds = GameObject.FindGameObjectsWithTag("Boundary");
	}

	void Update () {
		Vector3 position = gameObject.transform.position;

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			gameObject.transform.Translate(Vector3.forward * Time.deltaTime);
			boundsCheck(position);
		}

		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			gameObject.transform.Translate(Vector3.back * Time.deltaTime);
			boundsCheck(position);
		}

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			gameObject.transform.Translate(Vector3.right * Time.deltaTime);
			boundsCheck(position);
		}

		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			gameObject.transform.Translate(Vector3.left * Time.deltaTime);
			boundsCheck(position);
		}

		if (Input.GetKey(KeyCode.Space)) {
			gameObject.transform.Translate(Vector3.up * Time.deltaTime);
			boundsCheck(position);
		}
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

