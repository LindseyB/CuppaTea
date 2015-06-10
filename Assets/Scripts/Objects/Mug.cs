using UnityEngine;
using System.Collections;

public class Mug : MonoBehaviour, Usable {
	private GrabAndDrop grabber;
	private GameObject kettle;
	private bool hasTea;
	
	void Start () {
		grabber = GameObject.Find ("FPSController").GetComponent<GrabAndDrop>();
		kettle = GameObject.Find("kettle");
		hasTea = false;
	}

	public void Use() {
		if (grabber.grabbedObject == kettle) {
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
		} else if (grabber.grabbedObject && grabber.grabbedObject.tag == "Tea") {
			grabber.DropObject();
			hasTea = true;
		} else {
			// drink
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}
	}
}
