using UnityEngine;
using System.Collections;

public class GrabHandler : MonoBehaviour, Usable {
	[SerializeField] public GameObject grabObject;
	private GrabAndDrop grabber;

	void Start() {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
	}

	public void Use() {
		if (grabber.grabbedObject.name == "mug") {
			grabber.grabbedObject.GetComponent<Mug>().drink();
			grabber.DropObject();
		}
	}
}
