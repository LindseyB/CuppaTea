using UnityEngine;
using System.Collections;

public class Mug : MonoBehaviour, Usable {
	private GrabAndDrop grabber;
	private GameObject kettle;
	private ParticleSystem steam;
	private GameObject normalWater;
	private GameObject spilledWater;
	private GameObject overflowingWater;

	private bool hasTea;
	private bool hasWater;
	private bool hasSugar;
	private int temp;
	private float timer;
	private float scrollSpeed;
	private Color teaColor;

	void Start () {
		grabber = GameObject.Find ("FPSController").GetComponent<GrabAndDrop>();
		kettle = GameObject.Find("kettle");
		steam = GameObject.Find("steam").GetComponent<ParticleSystem>();

		normalWater = gameObject.transform.GetChild(0).gameObject;
		spilledWater = GameObject.Find("mug-spill");
		overflowingWater = gameObject.transform.GetChild(1).gameObject;

		hasTea = false;
		hasSugar = false;
		hasWater = false;
	
		temp = 20;
		timer = 20;
		scrollSpeed = 0.5f;

		// TODO: base this off of the type of tea brewing
		teaColor = new Color(0.1412f, 0.0863f, 0.0196f, 0.1f);
	}

	public void Use() {
		if (grabber.grabbedObject == kettle) {
			if(hasWater){
				normalWater.SetActive(false);
				spilledWater.GetComponent<Renderer>().enabled = false;
				overflowingWater.SetActive(true);
			} else {
				normalWater.SetActive(true);
				spilledWater.GetComponent<Renderer>().enabled = false;
				overflowingWater.SetActive(false);
				temp = kettle.GetComponent<Kettle>().temp;
				hasWater = true;
			}
		} else if(grabber.grabbedObject && grabber.grabbedObject.name == "sugar") {
			grabber.DropObject();
			hasSugar = true;
		} else if (grabber.grabbedObject && grabber.grabbedObject.tag == "Tea") {
			grabber.DropObject();
			hasTea = true;
		} else {
			// drink
			normalWater.SetActive(false);
			overflowingWater.SetActive(false);
			spilledWater.GetComponent<Renderer>().enabled = false;
			hasWater = false;
		}
	}

	void Update() {

		if (hasWater) {
			float offset = Time.time * scrollSpeed;
			normalWater.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
			normalWater.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(offset, 0));
			spilledWater.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
			spilledWater.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(offset, 0));
			overflowingWater.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
			overflowingWater.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(offset, 0));
		}

		// cool temp over time
		if (hasWater && temp != 20) {timer -= Time.deltaTime;}
		if (temp != 20 && timer <= 0) {
			timer = 20;
			temp--;
		}

		if (hasWater && temp >= 100 && steam.isStopped) {
			steam.Play();
		} else if ((!hasWater || temp < 100) && steam.isPlaying) {
			steam.Stop();
		}

		if (hasWater && hasTea && temp > 40) {
			// darken
			// TODO: update to tint the texture
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
			teaColor.a += (Time.deltaTime/51);
			if(teaColor.a > 0.8f){ teaColor.a = 0.8f; }
			gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = teaColor;
		}
		

		if (hasWater && gameObject.transform.forward.y < 1 && 
		    (!grabber.grabbedObject ||
		    grabber.grabbedObject.name != gameObject.name)) {
			spilledWater.GetComponent<Renderer>().enabled = true; // display spilled water

			// move to the location of the mug except for in y dir
			Vector3 newPosition = gameObject.transform.position;
			newPosition.y = spilledWater.transform.position.y;

			// rotate to face out of mug
			Quaternion newRotation = gameObject.transform.rotation;
			newRotation.y = 0;
			newRotation.w = 0;

			spilledWater.transform.rotation = newRotation;
			spilledWater.transform.position = newPosition;

			normalWater.SetActive(false); // hide normal water
			overflowingWater.SetActive(false) // hide overflowing water
		} else if (spilledWater.GetComponent<Renderer>().enabled) {
			spilledWater.GetComponent<Renderer>().enabled = false;
		}
	}
}
