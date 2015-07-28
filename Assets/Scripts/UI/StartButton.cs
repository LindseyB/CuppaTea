using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StartButton : MonoBehaviour {
	[SerializeField] private Color startColor;
	[SerializeField] private Color hoverColor;

	private Text buttonText;
	private AudioListener audioListener;

	void Start(){
		audioListener = FindObjectOfType<AudioListener>() as AudioListener;
		buttonText = gameObject.transform.GetChild (0).GetComponent<Text> ();
		buttonText.color = startColor;
		gameObject.GetComponent<Button>().onClick.AddListener(() => { startGame(); }); 
	}

	private void startGame(){
		audioListener.enabled = true;
		foreach (Text text in GameObject.Find("MainMenuCanvas").GetComponentsInChildren<Text>()) {
			text.CrossFadeAlpha (0.0f, 0.5f, true);
		}

		foreach(AudioSource source in GameObject.FindObjectsOfType<AudioSource>()) {
			source.mute = false;
		}

		StartCoroutine("UnBlur");
		StartCoroutine("DisableCanvas");
	}

	IEnumerator UnBlur() {
		UnityStandardAssets.ImageEffects.Blur blur = GameObject.Find("FirstPersonCharacter").GetComponent<UnityStandardAssets.ImageEffects.Blur>();

		while(blur.blurSpread > 0){
			yield return new WaitForSeconds(0.01f);
			blur.blurSpread -= 0.05f;
		}
		yield return null;
	}


	IEnumerator DisableCanvas() {
		yield return new WaitForSeconds(0.6f);
		GameObject.Find("MainMenuCanvas").SetActive(false);
		GameObject.Find("FirstPersonCharacter").GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = false;
		GameState.InMainMenu = false;
		yield return null;
	}

	public void onEnter() {
		buttonText.CrossFadeColor(hoverColor, 0.5f, true, true);
	}

	public void onLeave() {
		buttonText.CrossFadeColor(startColor, 0.5f, true, true);
	}
}
