using UnityEngine;
using Pathfinding;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

    [HideInInspector]
    public Seeker seeker;
    [HideInInspector]
    public Rigidbody2D rb;
    // Calculated path
    public Path path;
    public Transform target;
    public ForceMode2D fMode;

    // Waypoint we are currently moving towards
    [HideInInspector]
    public int currentWaypoint = 0;
    // The max distance from the AI to the waypoint for it to continue to the next waypoint
    public float nextWayPointDistance = 3;

    [HideInInspector]
    public bool pathIsEnded = false;
    public float updateRate = 2f;
    // AI's speed per sec
    public float speed = 300f;

    public bool closeRange = false;
    public bool longRange = false;
    private bool searchForPlayer = false;
    [HideInInspector]
    public bool playerCloseEnough = false;


    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Best for update physics
    void FixedUpdate() {
        if (!checkTarget() || path == null) {
            return;
        }
        if (!playerCloseEnough) {
//            Debug.Log(playerCloseEnough+ "!");
            calculatePath();
        }
    }

    private IEnumerator updatePath() {
        if (checkTarget()) {
            int pos = closeRange ? 1 : 3;
            //int fallBack = longRange ? -1 : 1;
            // Start a new path to the target position, return the result to the OnPathComplete method
            seeker.StartPath(transform.position, target.position / pos, OnPathComplete);
        }

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(updatePath());
    }

    public void OnPathComplete(Path p) {
        //Debug.Log("We got a path. Did it have an error?" + p.error);
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Calculate and move the enemy towards the player
    private void calculatePath() {
        if (currentWaypoint >= path.vectorPath.Count) {
            if (pathIsEnded) {
                return;
            }
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        //Direction to the next waypoint
        Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        direction *= speed * Time.fixedDeltaTime;

        //Move the AI 
        rb.AddForce(direction, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWayPointDistance) {
            currentWaypoint++;
            return;
        }
    }

    // Check the target if it's null search for player
    public bool checkTarget() {
        if (target == null) {
            if (!searchForPlayer) {
                searchForPlayer = true;
                StartCoroutine(findPlayer());
                return true;
            }
            return false;
        }
        return true;
    }

    // Find player
    private IEnumerator findPlayer() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(findPlayer());
        } else {
            searchForPlayer = false;
            target = player.transform;
            StartCoroutine(updatePath());
        }
    }

}
