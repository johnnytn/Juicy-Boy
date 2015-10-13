using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Parallaxing : MonoBehaviour
{
	public List<Transform> backgroundList = new List<Transform>();
	//public Transform[] backgrounds; 	//Array (list) of all the back and foregrounds to be parallaxed
	private float[] paralaxSaceles; 	// the proportion of the camera's movement to move the backgrounds by
	public float smoothing = 1; 		// How smooth the parallax is going to be. Make sure to be above 0.

	private Transform cam;				// Reference to the main camares transform
	private Vector3 previousCamPos;		// The position of the camera in the previous frame

	// Called before the Start()
	void Awake ()
	{
		// Set up camera reference
		cam = Camera.main.transform;

	}

	// Use this for initialization
	void Start ()
	{

		// Storing previous cam position
		previousCamPos = cam.position;
		paralaxSaceles = new float[backgroundList.Count];

		// asigning coresponding parallaxScales
		for (int i = 0; i < backgroundList.Count; i++) {

			paralaxSaceles [i] = backgroundList[i].position.z * -1;

		}
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		for (int i = 0; i < backgroundList.Count; i++) {
			// The parallax is the opposite of the camera movement
			float parallax = (previousCamPos.x - cam.position.x) * paralaxSaceles[i];

			// Set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgroundList[i].position.x + parallax;

			// Create a target position which is the background's current position with its target position
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX,backgroundList[i].position.y,
			                                           backgroundList[i].position.z);

			backgroundList[i].position = Vector3.Lerp (backgroundList[i].position, 
			                                           backgroundTargetPos, smoothing * Time.deltaTime);			
		}

		previousCamPos = cam.position;
	
	}
}
