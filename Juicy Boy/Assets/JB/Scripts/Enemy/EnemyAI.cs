using UnityEngine;
using Pathfinding;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

    private Seeker seeker;
    private Rigidbody2D rb;
    // Waypoint we are currently moving towards
    private int currentWaypoint = 0;
    // The max distance from the AI to the waypoint for it to continue to the next waypoint
    public float nextWayPointDistance = 3;

    // Calculated path
    public Path path;
    [HideInInspector]
    public bool pathIsEnded = false;

    public Transform target;
    public ForceMode2D fMode;
    public float updateRate = 2f;
    // AI's speed per sec
    public float speed = 300f;

    private bool searchForPlayer = false;


    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (checkTarget()) {
            // Start a new path to the target position, return the result to the OnPathComplete method
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }

        StartCoroutine(updatePath());
    }

    private IEnumerator updatePath() {
        if (checkTarget()) {
            // Start a new path to the target position, return the result to the OnPathComplete method
            seeker.StartPath(transform.position, target.position, OnPathComplete);
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

    // Best for update physics
    void FixedUpdate() {
        if (!checkTarget() || path == null) {
            return;
        }
        calculatePath();
    }

    // Calculate and move the enemy towards the player
    private void calculatePath() {
        if (currentWaypoint >= path.vectorPath.Count) {
            if (pathIsEnded) {
                return;
            }
            // Debug.Log("END OF THE PATH REACHED");
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
    private bool checkTarget() {
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
