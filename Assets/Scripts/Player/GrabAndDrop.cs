using UnityEngine;
using System.Collections;

public class GrabAndDrop : MonoBehaviour {
	GameObject grabbedObject;
	float grabbedObjectSize;

	GameObject GetMouseHoverObject(float range) {

		RaycastHit hit = new RaycastHit();
		if (
			!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition).origin,
		                 Camera.main.ScreenPointToRay(Input.mousePosition).direction, out hit, 100,
		                 Physics.DefaultRaycastLayers)){
			return null;
		}

		// We need to hit a rigidbody that is not kinematic
		if (!hit.rigidbody || hit.rigidbody.isKinematic) {
			return null;
		}

		return hit.rigidbody.gameObject;
	}
	
	void TryGrabObject(GameObject grabObject) {
		if (grabObject) {
			grabbedObject = grabObject;
			grabbedObjectSize = grabObject.GetComponent<Renderer>().bounds.size.magnitude;

		}
	}

	void DropObject() {
		if (grabbedObject) {
			grabbedObject.GetComponent<Collider>().enabled = true;
			grabbedObject = null;
		}
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
			Vector3 newPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin + Camera.main.ScreenPointToRay(Input.mousePosition).direction * grabbedObjectSize;
			grabbedObject.transform.position = newPosition;
			grabbedObject.GetComponent<Collider>().enabled = false;
		}

	}
}
