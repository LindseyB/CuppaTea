using UnityEngine;
using System.Collections;

public class HoverHighlight : MonoBehaviour {
	[SerializeField] public Material hoverMat;

	GameObject hoverObject;

	public void Start() {
		hoverObject = Instantiate(gameObject) as GameObject;
		hoverObject.SetActive(false);
		removeJunk();	
		hoverObject.GetComponent<Renderer>().material = hoverMat;
	}

	public void OnMouseEnter() {
		hoverObject.SetActive(true);
	}

	public void OnMouseExit() {
		hoverObject.SetActive(false);
	}

	void removeJunk() {
		// remove all scripts
		foreach(MonoBehaviour script in hoverObject.GetComponents<MonoBehaviour>()) {
			script.enabled = false;
		}

		hoverObject.tag = "Untagged";
		hoverObject.GetComponent<Rigidbody>().isKinematic = true;

		foreach(BoxCollider collider in hoverObject.GetComponents<BoxCollider>()) {
			collider.enabled = false;
		}
	}
}
