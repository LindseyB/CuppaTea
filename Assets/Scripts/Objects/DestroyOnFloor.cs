using UnityEngine;
using System.Collections;

public class DestroyOnFloor : MonoBehaviour {
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.name == "Floor") {
			GameObject.Destroy(gameObject);
		}
	}
}
