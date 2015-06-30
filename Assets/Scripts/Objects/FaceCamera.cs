using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
	private GameObject parent;
	private float dist;

	void Start() {
		parent = gameObject.transform.parent.gameObject;
	}

	void Update () {
		Vector3 forward = Camera.main.transform.forward;
		forward.y = 0.0f;

		dist = Vector3.Distance(Camera.main.transform.position, gameObject.transform.position);
		gameObject.GetComponent<TextMesh>().fontSize = 60 + (int)(dist*20);

		if(parent.transform.forward.y < 1){
			// turn off if on side
			gameObject.SetActive(false);
		}


		gameObject.transform.rotation = Quaternion.LookRotation(forward);
	}
}
