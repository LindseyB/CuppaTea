using UnityEngine;
using System.Collections;

public class Volume : MonoBehaviour, Usable {
	private GrabAndDrop grabber;
	
	void Start () {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
	}

	public void Use() {
		if (grabber.grabbedObject) { return; }
		
		// change volume
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			AudioListener.volume += 0.1f;
		} else {
			AudioListener.volume -= 0.1f;
		}
	}
	
	public void OnMouseEnter() {
		if (GameState.InMainMenu) { return; }
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}
	
	public void OnMouseExit() {
		gameObject.transform.GetChild(0).gameObject.SetActive(false);
	}
}
