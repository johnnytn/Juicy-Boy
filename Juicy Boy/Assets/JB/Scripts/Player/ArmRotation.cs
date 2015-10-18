using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	public int rotationOffSet = 0;

	// Update is called once per frame
	void Update () {
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		difference.Normalize(); // the sum of the vectors(x,y,z) will be equal to 1

		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg; // Find the angle in degrees
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffSet);
	}
}
