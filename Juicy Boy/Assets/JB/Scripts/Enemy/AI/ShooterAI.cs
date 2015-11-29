using UnityEngine;
using System.Collections;

public class ShooterAI : EnemyAI {

    private int distance = 10;
    [SerializeField]
    ShooterEnemy enemy;

    void Update() {
        if (!checkTarget() || path == null) {
            return;
        }
        if (checkProximity()) {
            //Debug.Log(playerCloseEnough + "!");
            //StartCoroutine(updatePath());
            calculatePath();
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
        rb.AddForce(-direction, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWayPointDistance) {
            currentWaypoint++;
            return;
        }
    }

    private IEnumerator updatePath() {
        if (checkTarget()) {
            //int pos = closeRange ? 1 : 3;
            //int fallBack = longRange ? -1 : 1;
            // Start a new path to the target position, return the result to the OnPathComplete method
            seeker.StartPath(transform.position * -1, target.position, OnPathComplete);
        }

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(updatePath());
    }

    public bool checkProximity() {
        //  Debug.Log(Vector3.Distance(transform.position, target.position) + "!");
        if (Vector3.Distance(transform.position, target.position) < distance) {
            playerCloseEnough = true;
        } else {
            playerCloseEnough = false;
        }
        return playerCloseEnough;
    }
}
