using UnityEngine;
using System.Collections;

public class DestroyOnFloor : MonoBehaviour {
	private GameObject table;

	void Start () {
		table =  GameObject.Find("Table");
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.name == "Floor" && table.transform.forward.y <= 0.8f) {
			GameObject.Destroy(gameObject);
		}
	}
}
