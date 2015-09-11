using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Helpers;

public class AchievementGet : MonoBehaviour {
	[SerializeField] private GameObject achievementCountDisplay;
	[SerializeField] private GameObject pointsDisplay;

	private const float SLIDE_DURATION = 1f;
	private const float DISPLAY_DURATION = 5f;

	private const int NUMBER_OF_ACHIEVEMENTS = 16;

	private Vector3 disabledPos;
	private Vector3 enabledPos;
	private bool moving = false;
	private int achievedCount = 0;

	void Start () {
		enabledPos = new Vector3(0, -Screen.height/7, 0);
		disabledPos = new Vector3(0, -Screen.height/7 - 80, 0);

		gameObject.transform.localPosition = disabledPos;
	}

	void Awake() {
		achievedCount = AchievementRecorder.readAchievements();
		achievementCountDisplay.GetComponent<TextMesh>().text = achievedCount.ToString();
		pointsDisplay.GetComponent<TextMesh>().text = "points: " + AchievementRecorder.totalPoints();
	}


	void OnApplicationQuit() {
		AchievementRecorder.writeAchievements();
	}

	public void TriggerAchievement(Achievement a) {
		if(!a.achieved) {
			achievedCount++;
			achievementCountDisplay.GetComponent<TextMesh>().text = achievedCount.ToString();
			a.Achieved();

			pointsDisplay.GetComponent<TextMesh>().text = "points: " + AchievementRecorder.totalPoints();

			if(achievedCount == NUMBER_OF_ACHIEVEMENTS) {
				DisplayAchievement(AchievementRecorder.metaAchievement);
				GameState.Blackhole = true;
			} else {
				DisplayAchievement(a);
			}
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

	public void resetAchievements() {
		AchievementRecorder.resetAchievements();
	}
}
