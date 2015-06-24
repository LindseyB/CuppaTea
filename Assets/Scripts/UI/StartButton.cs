using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StartButton : MonoBehaviour {
	[SerializeField] private Color startColor;
	[SerializeField] private Color hoverColor;

	private GameState gameState;
	private Text buttonText;

	void Start(){
		buttonText = gameObject.transform.GetChild (0).GetComponent<Text> ();
		gameState = FindObjectOfType (typeof(GameState)) as GameState;

		buttonText.color = startColor;
		gameObject.GetComponent<Button>().onClick.AddListener(() => { startGame(); }); 
	}

	private void startGame(){
		foreach (Text text in GameObject.Find("Canvas").GetComponentsInChildren<Text>()) {
			text.CrossFadeAlpha (0.0f, 0.5f, true);
		}
		StartCoroutine("DisableCanvas");
	}
	

	IEnumerator DisableCanvas() {
		yield return new WaitForSeconds(1);
		GameObject.Find("Canvas").SetActive(false);
		GameObject.Find("FirstPersonCharacter").GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>().enabled = false;
		gameState.InMainMenu = false;
		yield return null;
	}

	public void onEnter() {
		buttonText.CrossFadeColor(hoverColor, 0.5f, true, true);
	}

	public void onLeave() {
		buttonText.CrossFadeColor(startColor, 0.5f, true, true);
	}
}
