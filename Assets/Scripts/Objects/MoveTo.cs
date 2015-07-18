using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	[SerializeField] private GameObject avoidObject;

	private GrabAndDrop grabber;
	private Vector3 startPosition;
	private Quaternion startRotation;
	private TableFlip tableFlip;
	private GameObject table;
	
	void Start () {
		grabber = FindObjectOfType (typeof(GrabAndDrop)) as GrabAndDrop;
		tableFlip = FindObjectOfType (typeof(TableFlip)) as TableFlip;
		startPosition = gameObject.transform.position;
		startRotation = gameObject.transform.rotation;
		table = GameObject.Find("table");
	}

	void OnCollisionEnter(Collision collision) {

		if (gameObject == grabber.grabbedObject || tableFlip.animate || table.transform.forward.y < 0.8) {
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
