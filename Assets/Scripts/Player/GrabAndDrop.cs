using UnityEngine;
using System.Collections;

public class GrabAndDrop : MonoBehaviour {
	GameObject grabbedObject;
	float grabbedObjectSize;

	GameObject GetMouseHoverObject(float range) {
		Vector3 position = gameObject.transform.position;
		RaycastHit raycastHit;
		Vector3 target = position + Camera.main.transform.forward * range;

		if (Physics.Linecast (position, target, out raycastHit)) {
			// object under cursor
			if (raycastHit.collider.gameObject.name == "Floor") { return null; }

			return raycastHit.collider.gameObject;
		}

		// nothing found
		return null;
	}

	void TryGrabObject(GameObject grabObject) {
		if (grabObject) {
			grabbedObject = grabObject;
			grabbedObjectSize = grabObject.GetComponent<Renderer>().bounds.size.magnitude;

		}
	}

	void DropObject() {
		grabbedObject = null;
	}

	void Update () {
		// pick up or drop object
		if (Input.GetMouseButtonDown (0)) {
			if (!grabbedObject) {
				TryGrabObject(GetMouseHoverObject(5.0f));
			} else {
				DropObject();
			}
		}

		// float object in front of camera 
		if (grabbedObject) {
			Vector3 newPosition = gameObject.transform.position + Camera.main.transform.forward * grabbedObjectSize;
			grabbedObject.transform.position = newPosition;
		}

	}
}
