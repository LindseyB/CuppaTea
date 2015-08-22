using UnityEngine;
using System.Collections;
using Helpers;

public class GrabHandler : MonoBehaviour, Usable {
	[SerializeField] public GameObject grabObject;
	private GrabAndDrop grabber;
	private AchievementGet achievementGet;

	void Start() {
		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		achievementGet = GameObject.FindObjectOfType<AchievementGet>() as AchievementGet;
	}

	public void Use() {
		if (grabber.grabbedObject.name == "mug") {
			achievementGet.TriggerAchievement(AchievementRecorder.madTeaParty);
			grabber.grabbedObject.GetComponent<Mug>().drink();
		}
	}
}
