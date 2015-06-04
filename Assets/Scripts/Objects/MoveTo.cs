using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	[SerializeField] private GameObject snapObject;

	private GrabAndDrop grabber;
	private Vector3 adjustVec;
	
	void Start () {
		grabber = FindObjectOfType (typeof(GrabAndDrop)) as GrabAndDrop;
	}

	void OnCollisionEnter(Collision collision) {
		if (gameObject == grabber.grabbedObject) {
			return;
		}

		if (collision.collider.name != snapObject.name) {
			Vector3 newPosition = snapObject.transform.position * snapObject.GetComponent<Renderer>().bounds.size.magnitude;
			gameObject.transform.position = newPosition;
		}
	}
}
