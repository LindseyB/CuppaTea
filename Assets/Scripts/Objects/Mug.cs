using UnityEngine;
using System.Collections;
using Helpers;

public class Mug : RewindObject, Usable {
	[SerializeField] private AudioSource collideAudio;

	private GrabAndDrop grabber;
	private GameObject kettle;
	private ParticleSystem steam;
	private GameObject normalWater;
	private GameObject spilledWater;
	private GameObject overflowingWater;
	private GameObject go;
	private GameObject milkSwirl;
	
	private bool hasWater;
	private int temp;
	private float timer;
	private float scrollSpeed;
	private Color teaColor;
	private Color milkColor;

	private int teaCount = 0;
	private int sugarCount = 0;
	private int creamCount = 0;
	private int lemonCount = 0;

	private bool hasOolong = false;
	private bool hasSencha = false;
	private bool hasPuerh  = false;
	private bool hasDarjeeling = false;

	private float oolongTimer = 0;
	private const float OOLONG_TIME = 100;

	private Hashtable teaColors = new Hashtable();
	private GameObject[] waterObjects;

	new void Start () {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		kettle = GameObject.Find("kettle");
		steam = GameObject.Find("mug-steam").GetComponent<ParticleSystem>();

		normalWater = gameObject.transform.GetChild(0).gameObject;
		milkSwirl = normalWater.transform.GetChild(0).gameObject;
		spilledWater = GameObject.Find("mug-spill");
		overflowingWater = gameObject.transform.GetChild(1).gameObject;

		waterObjects = new GameObject[]{ normalWater, spilledWater, overflowingWater };

		hasWater = false;
	
		temp = 20;
		timer = 20;
		scrollSpeed = 0.5f;

		// tea colors for each tea type
		teaColors.Add("puerh", new Color(0.2588f, 0.2f, 0.0509f, 0.1f));
		teaColors.Add("darjeeling", new Color(0.1412f, 0.0863f, 0.0196f, 0.1f));
		teaColors.Add("oolong", new Color(0.7411f, 0.6078f, 0.2549f, 0.1f));
		teaColors.Add("sencha", new Color(0.3647f, 0.5294f, 0.1058f, 0.1f));

		milkColor = new Color(1f, 1f, 1f, 0.8f);
		teaColor = new Color(1f, 1f, 1f, 0.0f);

		base.Start();
	}

	public void Use() {
		if (grabber.grabbedObject == kettle) {
			kettle.GetComponent<Kettle>().pourAudio.Play();
			if(hasWater){
				achievementGet.TriggerAchievement(AchievementRecorder.eTeaOverflow);
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
		} else if(grabber.grabbedObject && grabber.grabbedObject.name.Contains("sugar")) {
			UseDupedObject();
			sugarCount++;
		} else if(grabber.grabbedObject && grabber.grabbedObject.name.Contains("lemon-slice")) {
			UseDupedObject();
			lemonCount++;
			curdledCheck();
		} else if(grabber.grabbedObject && grabber.grabbedObject.name == "creamer") { 
			if(!hasWater){
				normalWater.SetActive(true);
				foreach(GameObject water in waterObjects){
					water.gameObject.GetComponent<Renderer>().material.SetColor("_TintColor", milkColor);
				}
			}
			go = grabber.grabbedObject;
			grabber.DropObject();
			go.GetComponent<MoveTo>().ResetPosition();
			creamCount++;
			curdledCheck();
		} else if (grabber.grabbedObject && grabber.grabbedObject.tag == "Tea") {
			SetTeaColor();
			UseDupedObject();
			teaCount++;
		} else {
			if(teaCount == 0){ achievementGet.TriggerAchievement(AchievementRecorder.cupOfWhat); }
			if(sugarCount > teaCount){ achievementGet.TriggerAchievement(AchievementRecorder.teaWithYourSugar); }
			if(lemonCount >= 5){ achievementGet.TriggerAchievement(AchievementRecorder.smartlyTartly); }

			drink();
		}
	}

	new void Update() {
		if (hasWater) {
			float offset = Time.time * scrollSpeed;
			foreach(GameObject water in waterObjects){
				water.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
				water.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", new Vector2(offset, 0));
			}

			if(hasOolong){ oolongTimer += Time.deltaTime; }
			if(hasOolong && oolongTimer >= OOLONG_TIME) { achievementGet.TriggerAchievement(AchievementRecorder.oolongLongTime); }
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

		if (hasWater && teaCount > 0 && temp > 40) {
			achievementGet.TriggerAchievement(AchievementRecorder.firstCuppa);

			// brewing
			normalWater.gameObject.SetActive(true);
			teaColor.a += (Time.deltaTime/51);
			if(teaColor.a > 0.8f){ teaColor.a = 0.8f; }
			foreach(GameObject water in waterObjects){
				water.gameObject.GetComponent<Renderer>().material.SetColor("_TintColor", teaColor);
			}
		}

		if (hasWater && gameObject.transform.forward.y < 0.8 && 
		    (!grabber.grabbedObject ||
		    grabber.grabbedObject.name != gameObject.name)) {
			achievementGet.TriggerAchievement(AchievementRecorder.teaPartyFoul);
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
			overflowingWater.SetActive(false); // hide overflowing water

			if (steam.isPlaying){ steam.Stop(); }
		} else if (spilledWater.GetComponent<Renderer>().enabled) {
			spilledWater.GetComponent<Renderer>().enabled = false;
		}

		base.Update();
	}

	private void curdledCheck() {
		if(lemonCount > 0 && creamCount > 0){
			achievementGet.TriggerAchievement(AchievementRecorder.curdledMess);
		}

		if(creamCount <= 3 && creamCount > 0){ 
			milkSwirl.SetActive(true);
			milkSwirl.GetComponent<FluidSim>().curdled = lemonCount > 0;
			milkSwirl.GetComponent<FluidSim>().StartAdd();
			
		} else if(creamCount > 3 && !milkSwirl.GetComponent<FluidSim>().curdled) {
			milkSwirl.SetActive(false);
			teaColor = (milkColor + teaColor)/2;
			foreach(GameObject water in waterObjects){
				water.gameObject.GetComponent<Renderer>().material.SetColor("_TintColor", teaColor);
			}
		}
	}

	private void UseDupedObject(){
		grabber.grabbedObject.transform.position = gameObject.transform.position;
		grabber.grabbedObject.tag = "Interactable";
		grabber.grabbedObject.transform.SetParent(gameObject.transform);
		grabber.DropObject();
	}

	private void SetTeaColor() {
		char[] delimiterChars = { '-' };
		string teaName = grabber.grabbedObject.name.Split(delimiterChars)[0];

		if(teaName == "oolong")     { hasOolong = true; }
		if(teaName == "puerh" )     { hasPuerh  = true; }
		if(teaName == "sencha")     { hasSencha = true; }
		if(teaName == "darjeeling") { hasDarjeeling = true; }

		if(teaCount == 0){
			teaColor = (Color)teaColors[teaName];
		} else {
			if(hasOolong && hasPuerh && hasSencha && hasDarjeeling){ achievementGet.TriggerAchievement(AchievementRecorder.uniTea); }
			teaColor = ((Color)teaColors[teaName] + teaColor)/2;
		}
	}

	void OnCollisionEnter(Collision collision) {
		if(!GameState.Rewinding && !GameState.InMainMenu){ collideAudio.Play(); }
	}

	public void drink() {
		normalWater.SetActive(false);
		overflowingWater.SetActive(false);
		spilledWater.GetComponent<Renderer>().enabled = false;
		hasWater = false;
		sugarCount = lemonCount = creamCount = teaCount = 0;

		foreach (Transform child in gameObject.GetComponentsInChildren<Transform>()) {
			if(!child.gameObject.name.Contains("mug")){ Destroy(child.gameObject); }
		}
	}

	public bool isGlitchBearFav() {
		return (!hasWater && (teaCount == 0) && (lemonCount == 0) && (sugarCount > 0) && (creamCount > 0)); 
	}
}
