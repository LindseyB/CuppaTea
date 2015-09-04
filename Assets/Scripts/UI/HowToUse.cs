using UnityEngine;
using System.Collections;

public class HowToUse : MonoBehaviour {
	private GameObject HTU;
	private GameObject HTUH;
	private bool firstPlay = true;

	private GrabAndDrop grabber;

	void Start () {
		firstPlay = !PlayerPrefs.HasKey("Played");
		PlayerPrefs.SetString("Played", "true");
		PlayerPrefs.Save();

		grabber = GameObject.Find("FPSController").GetComponent<GrabAndDrop>();
		HTU = gameObject.transform.FindChild("HowToUse").gameObject;

		if(gameObject.transform.FindChild("HowToUseHeld")){
			HTUH = gameObject.transform.FindChild("HowToUseHeld").gameObject;
			setAlpha(HTUH, 0f);
		}

		setAlpha(HTU, 0f);
	}

	void Update() {
		if(grabber.grabbedObject == gameObject && firstPlay && HTUH){
			setAlpha(HTU, 0f);
			setAlpha(HTUH, 1f);
		}
	}

	public void OnMouseEnter() {
		if (GameState.InMainMenu || !firstPlay) { return; }
		setAlpha(HTU, 1f);
	}

	public void OnMouseExit() {
		setAlpha(HTU, 0f);
		if(HTUH){ setAlpha(HTUH, 0f); }
	}

	private void setAlpha(GameObject parent, float alpha){
		if(!parent){ return; }
		Color color = parent.GetComponent<SpriteRenderer>().color;
		color.a = alpha;
		parent.GetComponent<SpriteRenderer>().color = color;
		foreach(SpriteRenderer sr in parent.GetComponentsInChildren<SpriteRenderer>()){
			sr.color = color;
		}
	}
}
