using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewindObject : MonoBehaviour {
	const int LIMIT = 250;
	private bool rewinding = false;
	private Queue<Vector3> history = new Queue<Vector3>();
	private Queue<Quaternion> rot_history = new Queue<Quaternion>();

	private bool moving = false;

	public void Update() {
		if(!rewinding) {
			if(history.Count == LIMIT) {
				history.Dequeue();
				rot_history.Dequeue();
			}

			history.Enqueue(transform.position);
			rot_history.Enqueue(transform.rotation);
		}

		if(Input.GetKey(KeyCode.R)) {
			rewinding = true;
			Rewind();
		} else {
			rewinding = false;
		}
	}

	void Rewind() {
		if(history.Count > 0){
			StartCoroutine(MoveFromTo(history.Dequeue(), rot_history.Dequeue(), 0.5f));
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