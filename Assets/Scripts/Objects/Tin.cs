using UnityEngine;
using System.Collections;

public class Tin : MonoBehaviour, Usable {
	private GrabAndDrop grabber;

	public void Start() {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
	}

	public void Use() {
		// grab some tea leaves
		grabber.TryGrabObject(gameObject.transform.GetChild(0).gameObject);
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}
}
