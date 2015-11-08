// 2D Sky FREE version: 1.0
// Author: Gold Experience Team (http://ge-team.com/pages/unity-3d/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

using UnityEngine;
using System.Collections;

// ######################################################################
// This class does strength sprite to fit the orthographic camera view
// ######################################################################

public class SkyBG : MonoBehaviour {

    // ######################################################################
    // Variables
    // ######################################################################

    #region Variables	

    public Camera m_Camera = null;                  // Handle Orthographic Camera in the scene

    Vector3 LeftMostOfScreen;                       // Vector3 of middle-left most position at the edge of the camera view
    Vector3 RightMostOfScreen;                      // Vector3 of middle-right most position at the edge of the camera view

    #endregion

    // TILING
    Vector3 screenSize;

    // Used to check if we need to instantiate 
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    public bool reverseScale = false;

    public float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;
    public int offSetX = 2;

    // ######################################################################
    // MonoBehaviour Functions
    // ######################################################################

    #region Monobehavior

    void Awake() {
        cam = Camera.main;
        myTransform = transform;
    }


    // Use this for initialization
    void Start() {
        findAndStrength();
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x * 500;
    }

    // Update is called once per frame
    void Update() {
        tiling();
    }

    private void findAndStrength() {
        // Find an Orthographic camera in the scene
        FindTheOrthographicCamera();

        // Strength this gameObject to fit the camera view
        screenSize = new Vector3((RightMostOfScreen.x - LeftMostOfScreen.x)
                                    / this.GetComponent<Renderer>().bounds.size.x, (m_Camera.orthographicSize * 2)
                                    / this.GetComponent<Renderer>().bounds.size.y, 1);
        this.transform.localScale = screenSize;
    }

    #endregion {Monobehavior}

    // ######################################################################
    // Functions
    // ######################################################################

    #region Functions

    // Find an Orthographic camera in the scene
    void FindTheOrthographicCamera() {
        if (m_Camera == null) {
            Camera[] CameraList = FindObjectsOfType<Camera>();
            foreach (Camera child in CameraList) {
                if (child.orthographic == true) {
                    // Keep only first Orthographic Camera
                    m_Camera = child;
                    break;
                }
            }
        }

        // Calculate Left/Right most position at the edge of camera view
        if (m_Camera != null) {
            LeftMostOfScreen = m_Camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
            RightMostOfScreen = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        }
    }

    private void tiling() {
        if (!hasALeftBuddy || !hasARightBuddy) {
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            float edgeVisiblePosRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePosLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            if ((cam.transform.position.x >= edgeVisiblePosRight - offSetX) && !hasARightBuddy) {
                makeNewBuddy(1);
                hasARightBuddy = true;

            } else if ((cam.transform.position.x <= edgeVisiblePosLeft + offSetX) && !hasALeftBuddy) {
                makeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    // Used to create a new Buddy on the required side
    void makeNewBuddy(int rightOrLeft) {
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft,
                                           myTransform.position.y,
                                           myTransform.position.z);

        // Instantiating new buddy
        Transform newBuddy = (Transform)Instantiate(myTransform, newPosition, myTransform.rotation);

        // Check if it's tilable, if not reverse it
        if (reverseScale) {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1,
                                               newBuddy.localScale.y,
                                               newBuddy.localScale.z);
        }
        newBuddy.parent = myTransform.parent;

        if (rightOrLeft > 0) {
            newBuddy.GetComponent<SkyBG>().hasALeftBuddy = true;
        } else {
            newBuddy.GetComponent<SkyBG>().hasARightBuddy = true;
        }
    }

    #endregion {Functions}
}
