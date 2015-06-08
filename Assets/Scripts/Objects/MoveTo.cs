using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	[SerializeField] private GameObject snapObject;

	private GrabAndDrop grabber;
	private Vector3 startPosition;
	
	void Start () {
		grabber = FindObjectOfType (typeof(GrabAndDrop)) as GrabAndDrop;
		startPosition = gameObject.transform.position;
	}

	void OnCollisionEnter(Collision collision) {
		if (gameObject == grabber.grabbedObject) {
			return;
		}

		if (collision.collider.name != snapObject.name) {
			gameObject.transform.position = startPosition;
		}
	}
}
