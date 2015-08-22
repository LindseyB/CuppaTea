using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Helpers;

public class RewindObject : MonoBehaviour {
	const int LIMIT = 250;
	public bool rewinding { get; set; }

	private LinkedList<Vector3> history = new LinkedList<Vector3>();
	private LinkedList<Quaternion> rot_history = new LinkedList<Quaternion>();

	private Vector3 startPos;
	private Quaternion startRot;

	private bool moving = false;
	private bool hitSet = false;

	private VHSPostProcessEffect vhs;
	private AchievementGet achievementGet;

	public void Start() {
		rewinding = false;
		startPos = gameObject.transform.position;
		startRot = gameObject.transform.rotation;

		vhs = GameObject.FindObjectOfType<VHSPostProcessEffect>();
		achievementGet = GameObject.FindObjectOfType<AchievementGet>() as AchievementGet;
	}

	void OnCollisionEnter(Collision collision) {
		// set on table hit to get the position after being pulled by gravity
		if (collision.collider.name == "table" && !hitSet) {
			hitSet = true;
			startPos = gameObject.transform.position;
			startRot = gameObject.transform.rotation;
		}
	}

	public void Update() {
		if(!rewinding) {
			if(history.Count == LIMIT) {
				history.RemoveFirst();
				rot_history.RemoveFirst();
			}

			history.AddLast(transform.position);
			rot_history.AddLast(transform.rotation);
		}

		if(Input.GetButton("Rewind")) {
			achievementGet.TriggerAchievement(AchievementRecorder.rewind);
			vhs.enabled = true;
			rewinding = true;
			GameState.Rewinding = true;
			Rewind();
		} else {
			vhs.enabled = false;
			rewinding = false;
			vhs.rewindAudio.Stop();
			GameState.Rewinding = false;
		}
	}

	void Rewind() {
		if(history.Last != null && rot_history.Last != null){
			Vector3 pos;
			Quaternion rot;
			do {
				pos = history.Last.Value;
				rot = rot_history.Last.Value;
				history.RemoveLast();
				rot_history.RemoveLast();
			} while (pos == gameObject.transform.position && 
			         rot == gameObject.transform.rotation &&
			         history.Last != null &&
			         rot_history.Last != null);

			StartCoroutine(MoveFromTo(pos, rot, 0.5f));
		} else {
			// return to start position
			StartCoroutine(MoveFromTo(startPos, startRot, 0.5f));
		}
	}

	IEnumerator MoveFromTo(Vector3 targetPos, Quaternion targetRot, float time){
		Vector3 startPos = transform.position;
		Quaternion startRot = transform.rotation;
		if (!moving){
			moving = true;
			float t = 0f;
			while (t < 1f){
				t += Time.deltaTime / time;
				transform.position = Vector3.Lerp(startPos, targetPos, t); 
				transform.rotation = Quaternion.Lerp(startRot, targetRot, t);
				yield return 0;
			}
			moving = false;

			Rigidbody rigidbody;
			if(rigidbody = gameObject.GetComponent<Rigidbody>()){
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
			}
		}
	}
}