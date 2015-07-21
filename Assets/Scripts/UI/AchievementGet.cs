using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementGet : MonoBehaviour {
	private const float SLIDE_DURATION = 1f;
	private const float DISPLAY_DURATION = 5f;

	private Vector3 disabledPos;
	private Vector3 enabledPos;
	private bool moving = false;


	void Start () {
		enabledPos = new Vector3(0f, -95f, 0f);
		disabledPos = new Vector3(0f, -125f, 0f);
	}

	void Update() {
		if(Input.GetKey(KeyCode.Alpha1)) {
			DisplayAchievement("You did a thing!");
		}
	}

	public void DisplayAchievement(string achievement) {
		gameObject.GetComponentInChildren<Text>().text = achievement;
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
