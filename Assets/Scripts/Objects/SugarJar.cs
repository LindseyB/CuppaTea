using UnityEngine;
using System.Collections;

public class SugarJar : MonoBehaviour, Usable {
	private GrabAndDrop grabber;
	private Vector3 scale;
	private GameObject sugarObject;
	private GameObject sugarObjectDupe;
	
	public void Start() {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		scale = gameObject.transform.GetChild(0).gameObject.transform.localScale;
		sugarObject = gameObject.transform.GetChild(0).gameObject;
	}
	
	public void Use() {
		// grab some sugar 
		sugarObjectDupe = (GameObject) Instantiate(sugarObject);
		sugarObjectDupe.transform.SetParent(gameObject.transform);
		sugarObjectDupe.transform.localScale = scale;

		grabber.TryGrabObject(sugarObjectDupe);
		sugarObjectDupe.SetActive(true);
	}

}