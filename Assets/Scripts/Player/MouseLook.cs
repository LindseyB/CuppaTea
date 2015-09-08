using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationX = 0F;
	float rotationY = 0F;
	
	Quaternion originalRotation;


	void Start () {
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>()) {
			GetComponent<Rigidbody>().freezeRotation = true;
		}
		originalRotation = transform.localRotation;

		if(PlayerPrefs.HasKey("MouseSensitivity")){
			float sensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
			sensitivityX = 15F * sensitivity;
			sensitivityY = 15F * sensitivity;
		}
	}
	
	void Update () {
		if (GameState.InMainMenu) { return; }

		if (axes == RotationAxes.MouseXAndY) {
			// Read the mouse input axis
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
			
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		} else if (axes == RotationAxes.MouseX) {
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		} else {
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			
			Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}
	

	
	public static float ClampAngle (float angle, float min, float max) {
		if (angle < -360F) {
			angle += 360F;
		}
		if (angle > 360F) {
			angle -= 360F;
		}
		return Mathf.Clamp (angle, min, max);
	}

	public void MouseSensitivity() {
		float sensitivity = GameObject.Find("Slider").GetComponent<Slider>().value;
		PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
		PlayerPrefs.Save();

		sensitivityX = 15F * sensitivity;
		sensitivityY = 15F * sensitivity;
	}
}