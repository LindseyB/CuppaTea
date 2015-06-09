using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			gameObject.transform.Translate(Vector3.forward * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			gameObject.transform.Translate(Vector3.back * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			gameObject.transform.Translate(Vector3.right * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			gameObject.transform.Translate(Vector3.left * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.Space)) {
			gameObject.transform.Translate(Vector3.up * Time.deltaTime);
		}
	}
}
