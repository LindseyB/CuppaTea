using UnityEngine;
using System.Collections;

public class TableFlip : MonoBehaviour {
	private const float ROTATION_MAX = 360.0f;
	private const float ROTATE_TIME = 1.0f;
	private float timer = 0.0f;
	public bool animate = false;
	private GameState gameState;
	
	void Start () {
		gameState = FindObjectOfType (typeof(GameState)) as GameState;
	}

	void Update () {
		if (gameState.InMainMenu) { return; }

		if (Input.GetKeyDown(".") && !animate){
			timer = 0;
			animate = true;
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
		}

		if (animate) {
			timer += Time.deltaTime;

			if (timer < ROTATE_TIME) {
				gameObject.transform.Rotate(-Vector3.right * Time.deltaTime * ROTATION_MAX / ROTATE_TIME);
			}
		}
	}
}
