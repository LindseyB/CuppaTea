using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StartButton : MonoBehaviour {
	private GameState gameState;

	void Start(){
		gameState = FindObjectOfType (typeof(GameState)) as GameState;
		gameObject.GetComponent<Button>().onClick.AddListener(() => { startGame(); }); 
	}

	private void startGame(){
		gameState.InMainMenu = false;
		GameObject.Find("Canvas").SetActive(false);
	}
}
