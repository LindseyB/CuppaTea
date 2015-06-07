using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	[SerializeField] private GameObject snapObject;

	private GrabAndDrop grabber;
	private Vector3 adjust;
	
	void Start () {
		grabber = FindObjectOfType (typeof(GrabAndDrop)) as GrabAndDrop;
		adjust = new Vector3 (0, GameObject.Find(snapObject.name).GetComponent<Renderer>().bounds.size.y, 0);
	}

	void OnCollisionEnter(Collision collision) {
		if (gameObject == grabber.grabbedObject) {
			return;
		}

		if (collision.collider.name != snapObject.name) {
			gameObject.transform.position = GameObject.Find(snapObject.name).transform.position + adjust;
		}
	}
}
