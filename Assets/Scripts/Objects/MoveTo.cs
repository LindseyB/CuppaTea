using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	[SerializeField] private GameObject avoidObject;

	private GrabAndDrop grabber;
	private Vector3 startPosition;
	private Quaternion startRotation;
	
	void Start () {
		grabber = FindObjectOfType (typeof(GrabAndDrop)) as GrabAndDrop;
		startPosition = gameObject.transform.position;
		startRotation = gameObject.transform.rotation;
	}

	void OnCollisionEnter(Collision collision) {
		if (gameObject == grabber.grabbedObject) {
			return;
		}

		if (collision.collider.name == avoidObject.name) {
			ResetPosition();
		}
	}

	public void ResetPosition() {
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		gameObject.transform.rotation = startRotation;
		gameObject.transform.position = startPosition;
	}
}
