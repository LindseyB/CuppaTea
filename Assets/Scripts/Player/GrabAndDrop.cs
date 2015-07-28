using UnityEngine;
using System.Collections;

[RequireComponent(typeof (HandCursor))]
public class GrabAndDrop : MonoBehaviour {
	[SerializeField] public float reach;

	public GameObject grabbedObject;
	private float grabbedObjectSize;
	private HandCursor cursor;
	private GameObject table;
	private HoverHighlight hh;

	void Start () {
		cursor = FindObjectOfType (typeof(HandCursor)) as HandCursor;
		table = GameObject.Find("table");
	}

	public GameObject GetMouseHoverObject(float range) {
		RaycastHit[] hits;
		if(grabbedObject) {
			hits = Physics.SphereCastAll(Camera.main.ScreenPointToRay(Input.mousePosition).origin, 
			                             grabbedObject.GetComponent<Collider>().bounds.extents.x,
			                             Camera.main.ScreenPointToRay(Input.mousePosition).direction,
			                             range);
		} else {
			hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition).origin, 
			                          Camera.main.ScreenPointToRay(Input.mousePosition).direction,
			                          range);
		}

		foreach (RaycastHit hit in hits) {
			if (hit.rigidbody && hit.rigidbody.gameObject.tag == "Interactable") {
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
			// disable the stripes overlay
			if(hh = grabbedObject.GetComponent<HoverHighlight>()) {
				hh.DisableHighlight();
			}

			if(grabbedObject.tag != "Tea"){ grabbedObject.tag = "Untagged"; }
		}
	}

	public void DropObject() {
		if (grabbedObject) {
			if(grabbedObject.tag != "Tea"){ grabbedObject.tag = "Interactable"; }
			grabbedObject = null;
			cursor.ToggleCursor();
		}
	}

	void Update () {
		if (GameState.InMainMenu) { return; }

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
			RepositionObject();
			if (Input.GetKey((KeyCode)GameControls.Controls.RotateLeft)){
				grabbedObject.transform.Rotate(Vector3.left);
			}

			if (Input.GetKey((KeyCode)GameControls.Controls.RotateForward)) {
				grabbedObject.transform.Rotate(Vector3.up);
			}
		}

	}

	public void RepositionObject() {
		Vector3 newPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin + Camera.main.ScreenPointToRay(Input.mousePosition).direction * grabbedObjectSize;
		if (!table.GetComponent<BoxCollider>().bounds.Contains(newPosition)){
			grabbedObject.transform.position = newPosition;
		}
		grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		grabbedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}
}
