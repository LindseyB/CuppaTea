using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Credits : MonoBehaviour {
	private string[] credits = { 
								"By Lindsey Bieda",
	                            "Glitch Bear By Todd Gizzi",
	                            "Purr Programming support by Dash"
								};
	private Text text;
	
	void Start () {
		text = gameObject.GetComponent<Text>() as Text;
		text.canvasRenderer.SetAlpha(0.0f);
		StartCoroutine("DisplayCredits");
	}


	IEnumerator DisplayCredits() {
		while(true){
			foreach(string credit in credits){
				text.text = credit;
				text.CrossFadeAlpha(1.0f, 5.0f, false);
				yield return new WaitForSeconds(5.0f);
				text.CrossFadeAlpha(0.0f, 5.0f, false);
				yield return new WaitForSeconds(5.0f);
			}
		}
	}
	
}
