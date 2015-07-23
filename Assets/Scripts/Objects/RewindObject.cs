using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewindObject : MonoBehaviour {
	const int LIMIT = 250;
	private bool rewinding = false;
	private LinkedList<Vector3> history = new LinkedList<Vector3>();
	private LinkedList<Quaternion> rot_history = new LinkedList<Quaternion>();

	private Vector3 startPos;
	private Quaternion startRot;

	private bool moving = false;
	private bool hitSet = false;

	private VHSPostProcessEffect vhs;

	public void Start() {
		startPos = gameObject.transform.position;
		startRot = gameObject.transform.rotation;

		vhs = GameObject.FindObjectOfType<VHSPostProcessEffect>();
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

		if(Input.GetKey(KeyCode.R)) {
			vhs.enabled = true;
			rewinding = true;
			Rewind();
		} else {
			vhs.enabled = false;
			rewinding = false;
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
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
	}
}