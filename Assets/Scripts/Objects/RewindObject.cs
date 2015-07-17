using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewindObject : MonoBehaviour {
	const int LIMIT = 250;
	private bool rewinding = false;
	private Queue<Vector3> history = new Queue<Vector3>();
	private Queue<Quaternion> rot_history = new Queue<Quaternion>();

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
			transform.position = history.Dequeue();
			transform.rotation = rot_history.Dequeue();
		}
	}
}