using UnityEngine;
using System.Collections;

[RequireComponent(typeof (HandCursor))]
public class GrabAndDrop : MonoBehaviour {
	[SerializeField] public float reach;

	public GameObject grabbedObject;
	private float grabbedObjectSize;
	private HandCursor cursor;

	void Start () {
		cursor = FindObjectOfType (typeof(HandCursor)) as HandCursor;
	}

	public GameObject GetMouseHoverObject(float range) {
		RaycastHit[] hits;
		hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition).origin, 
		                          Camera.main.ScreenPointToRay(Input.mousePosition).direction,
		                          range);

		foreach (RaycastHit hit in hits) {
			if (hit.rigidbody && !hit.rigidbody.isKinematic) {
				return hit.rigidbody.gameObject;
			}
		}

		return null;
	}
	
	public void TryGrabObject(GameObject grabObject) {
		if (grabObject) {
			grabbedObject = grabObject;
			grabbedObjectSize = grabObject.GetComponent<Renderer>().bounds.size.magnitude;
			cursor.ToggleCursor();
		}
	}

	public void DropObject() {
		if (grabbedObject) {
			grabbedObject.GetComponent<Collider>().enabled = true;
			grabbedObject = null;
			cursor.ToggleCursor();
		}
	}

	void Update () {
		// pick up or drop object
		if (Input.GetMouseButtonDown (0)) {
			if (!grabbedObject) {
				TryGrabObject(GetMouseHoverObject(reach));
			} else {
				DropObject();
			}
		}

		// float object in front of camera 
		if (grabbedObject) {			
			Vector3 newPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin + Camera.main.ScreenPointToRay(Input.mousePosition).direction * grabbedObjectSize;
			grabbedObject.transform.position = newPosition;
			grabbedObject.GetComponent<Collider>().enabled = false;
		}

	}
}
