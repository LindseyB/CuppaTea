using UnityEngine;
using System.Collections;

public class SugarJar : RewindObject, Usable {
	[SerializeField] private AudioSource collideAudio;

	private GrabAndDrop grabber;
	private Vector3 scale;
	private GameObject sugarObject;
	private GameObject sugarObjectDupe;
	
	new void Start() {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		scale = gameObject.transform.GetChild(0).gameObject.transform.localScale;
		sugarObject = gameObject.transform.GetChild(0).gameObject;

		base.Start();
	}
	
	public void Use() {
		if (grabber.grabbedObject) { return; }

		// grab some sugar 
		sugarObjectDupe = (GameObject) Instantiate(sugarObject);
		sugarObjectDupe.transform.SetParent(gameObject.transform);
		sugarObjectDupe.transform.localScale = scale;

		grabber.TryGrabObject(sugarObjectDupe);
		sugarObjectDupe.SetActive(true);
	}

	void OnCollisionEnter(Collision collision) {
		if(!GameState.Rewinding && !GameState.InMainMenu){ collideAudio.Play(); }
	}

}