using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
	private GameObject parent;

	void Start() {
		parent = gameObject.transform.parent.gameObject;
	}

	void Update () {
		Vector3 forward = Camera.main.transform.forward;
		forward.y = 0.0f;

		if(parent.transform.forward.y < 1){
			// turn off if on side
			gameObject.SetActive(false);
		}


		gameObject.transform.rotation = Quaternion.LookRotation(forward);
	}
}
