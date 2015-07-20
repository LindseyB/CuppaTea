using UnityEngine;
using System.Collections;

public class TableFlip : RewindObject {
	private const float ROTATION_MAX = 360.0f;
	private const float ROTATE_TIME = 1.0f;
	private float timer = 0.0f;
	public bool animate = false;
	private GameState gameState;

	private Vector3 startPosition;
	private Quaternion startRotation;

	new void Start () {
		gameState = FindObjectOfType (typeof(GameState)) as GameState;
		startPosition = gameObject.transform.position;
		startRotation = gameObject.transform.rotation;

		base.Start();
	}

	new void Update () {
		if (gameState.InMainMenu) { return; }

		if(gameObject.transform.rotation == startRotation && gameObject.transform.position == startPosition) {
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
		} else {
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
		}

		if (Input.GetKeyDown((KeyCode)GameControls.Controls.Rage) && !animate){
			timer = 0;
			animate = true;
		}

		if (animate) {
			timer += Time.deltaTime;

			if (timer < ROTATE_TIME) {
				gameObject.transform.Rotate(-Vector3.right * Time.deltaTime * ROTATION_MAX / ROTATE_TIME);
			} 
		}

		base.Update();
	}

	void OnCollisionEnter(Collision collision) {
		if (animate && timer > ROTATE_TIME && 
		    collision.collider.name == "Floor" && 
		    gameObject.transform.forward.y < 0.8) {
			animate = false;
			timer = 0;
		}
	}
}
