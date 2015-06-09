using UnityEngine;
using System.Collections;

public class HideIfNotHeld : MonoBehaviour {
	private GrabAndDrop grabber;
	
	void Start () {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
	}

	void Update () {
		if (gameObject != grabber.grabbedObject) {
			gameObject.SetActive(false);
		}
	}
}
