using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	//private Parallaxing parallaxing;

	public int offSetX = 2;

	// Used to check if we need to instantiate 
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;

	public bool reverseScale = false;

	public float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;

	void Awake() {

		//parallaxing = GetComponent<Parallaxing>();
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasALeftBuddy || !hasARightBuddy) {
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

			float edgeVisiblePosRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePosLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;

			if ((cam.transform.position.x >= edgeVisiblePosRight - offSetX) && !hasARightBuddy) {
				makeNewBuddy(1);
				hasARightBuddy = true;

			} else if ((cam.transform.position.x <= edgeVisiblePosLeft + offSetX) && !hasALeftBuddy ) { 
				makeNewBuddy(-1);
				hasALeftBuddy = true;
			}
		}
	
	}

	// Used to create a new Buddy on the required side
	void makeNewBuddy(int rightOrLeft) {
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft,
		                                   myTransform.position.y, 
		                                   myTransform.position.z);

		// Instantiating new buddy
		Transform newBuddy = (Transform) Instantiate(myTransform, newPosition, myTransform.rotation);

		// Check if it's tilable, if not reverse it
		if(reverseScale) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1,
			                                   newBuddy.localScale.y,
			                                   newBuddy.localScale.z);
		}
		newBuddy.parent = myTransform.parent;

		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		} else {
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}

		//parallaxing.backgroundList.Add(myTransform);
	}
}
