using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Helpers;

public class AchievementGet : MonoBehaviour {
	private const float SLIDE_DURATION = 1f;
	private const float DISPLAY_DURATION = 5f;

	private Vector3 disabledPos;
	private Vector3 enabledPos;
	private bool moving = false;

	void Start () {
		enabledPos = new Vector3(0, -Screen.height/7, 0);
		disabledPos = new Vector3(0, -Screen.height/7 - 80, 0);

		gameObject.transform.localPosition = disabledPos;
	}

	void Awake() {
		Helpers.AchievementRecorder.readAchievements();
	}


	void OnApplicationQuit() {
		Helpers.AchievementRecorder.writeAchievements();
	}

	public void TriggerAchievement(Achievement a) {
		if(!a.achieved) {
			a.Achieved();
			DisplayAchievement(a);
		}
	}

	void DisplayAchievement(Achievement a) {
		Text[] textObjects = gameObject.GetComponentsInChildren<Text>();
		textObjects[0].text = a.name;
		textObjects[1].text = a.description;
		StartCoroutine(SlideDisplay());
	}
	
	IEnumerator SlideDisplay() {
		if (!moving) {
			moving = true;
			yield return StartCoroutine(SlideDisplayUp());
			yield return new WaitForSeconds(DISPLAY_DURATION);
			yield return StartCoroutine(SlideDisplayDown());
			moving = false;
		}
	}

	IEnumerator SlideDisplayUp() {
		float time = SLIDE_DURATION;
		float t = 0f;
		while (t < 1f) {
			t += Time.deltaTime / time;
			transform.localPosition = Vector3.Lerp(disabledPos, enabledPos, t);
			yield return 0;
		}
	}

	IEnumerator SlideDisplayDown() {
		float time = SLIDE_DURATION;
		float t = 0f;
		while (t < 1f) {
			t += Time.deltaTime / time;
			transform.localPosition = Vector3.Lerp(enabledPos, disabledPos, t);
			yield return 0;
		}
	}
}
