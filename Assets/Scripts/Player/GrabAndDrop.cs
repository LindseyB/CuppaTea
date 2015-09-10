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
	private GrabHandler grabHandler;

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

			// prioritize glitch bear for hover events
			foreach (RaycastHit hit in hits) {
				if(hit.rigidbody && hit.rigidbody.gameObject.transform.root.gameObject.name == "d20bear"){
					return hit.rigidbody.gameObject;
				}
			}
		} else {
			hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition).origin, 
			                          Camera.main.ScreenPointToRay(Input.mousePosition).direction,
			                          range);
		}

		foreach (RaycastHit hit in hits) {
			if (hit.rigidbody && (hit.rigidbody.gameObject.tag == "Interactable" || hit.rigidbody.gameObject.tag == "UsableOnly")) {
				return hit.rigidbody.gameObject;
			}
		}

		return null;
	}
	
	public void TryGrabObject(GameObject grabObject) {
		if (grabObject && grabObject.tag != "UsableOnly") {
			grabbedObject = grabObject;
			hh = grabbedObject.GetComponent<HoverHighlight>();

			if(grabHandler = grabObject.GetComponent<GrabHandler>()) { 
				grabbedObjectSize = grabHandler.grabObject.GetComponent<Renderer>().bounds.size.magnitude;
				if(grabbedObject.name != "root"){ grabbedObject = GameObject.Find("root"); }
			} else if(hh && hh.renderObject){
				grabbedObjectSize = hh.renderObject.GetComponent<Renderer>().bounds.size.magnitude;
			} else {
				grabbedObjectSize = grabObject.GetComponent<Renderer>().bounds.size.magnitude;
			}

			cursor.ToggleCursor();
			// disable the stripes overlay
			if(hh) {
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
		if (Input.GetButtonDown("Grab")) {
			if (!grabbedObject) {
				TryGrabObject(GetMouseHoverObject(reach));
			} else {
				DropObject();
			}
		}

		// float object in front of camera 
		if (grabbedObject) {			
			RepositionObject();
			if (Input.GetButton("RotateLeft")){
				grabbedObject.transform.Rotate(Vector3.left);
			}

			if (Input.GetButton("RotateForward")) {
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
