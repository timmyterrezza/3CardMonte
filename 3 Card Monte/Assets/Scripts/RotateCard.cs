using UnityEngine;
using System.Collections;

public class RotateCard : MonoBehaviour {
    //To Be Reworked, have a better way just need to implement it.

	[SerializeField] private float rotationSpeed;
	[SerializeField] private float rotationAmount;

	void Start () {
		rotationAmount = 190;
	}

	void Update () {
		if (rotationAmount < 180) {
			transform.Rotate(Vector3.left * Time.deltaTime * rotationSpeed);
			rotationAmount += Time.deltaTime * rotationSpeed;
			if (rotationAmount > 180 && transform.rotation.eulerAngles.x > 0 && transform.rotation.eulerAngles.x < 10) {
				transform.localEulerAngles = new Vector3(0,180,180);
			
			}
			if (rotationAmount > 180 && transform.rotation.eulerAngles.x > 340 && transform.rotation.eulerAngles.x < 360) {
				transform.localEulerAngles = new Vector3(0,0,0);
			}
		}
	}

	public void TriggerRotation() {
		rotationAmount = 0;
	}

	public float GetRotationAmount() {
		return rotationAmount;
	}
}
