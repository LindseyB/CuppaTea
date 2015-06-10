using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
	

	// Update is called once per frame
	void Update () {
		Vector3 forward = Camera.main.transform.forward;
		forward.y = 0.0f;
		gameObject.transform.rotation = Quaternion.LookRotation(forward);
	}
}
