using UnityEngine;
using System.Collections;

public class DestroyOnFloor : MonoBehaviour {
	private GameObject floor;

	// Use this for initialization
	void Start () {
		floor = GameObject.Find("Floor");
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.name == floor.name) {
			GameObject.Destroy(gameObject);
		}
	}
}
