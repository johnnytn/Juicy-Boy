using UnityEngine;
using System.Collections;

public class EnemyGun : Gun {
    public EnemyAI enemyAI;
    // private Transform firePoint;
    //private bool searchForPlayer = false;
   // private float timeToSpawnEffect = 0;


    // Use this for initialization
    void Awake() {
        firePoint = this.transform;
        if (transform == null) {
            Debug.LogError("Fire error! No FirePoint?");
        }
    }

    void Update() {
        if (enemyAI.target == null) {
            return;
        }
        action();
    }

    public void action() {
        Vector2 firePointPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPos = new Vector2(enemyAI.target.position.x, enemyAI.target.position.x);
       // Debug.Log("fire:" + firePointPosition);
       // Debug.Log("player:"+playerPos);
        if (fireRate == 0) {
            shoot(firePointPosition, playerPos);
        } else if (Time.time > timeToFire) {
            timeToFire = Time.time + 1 / fireRate;
            shoot(firePointPosition, playerPos);

        }
    }

    // Check the target if it's null search for player
    //public bool checkTarget() {
    //    if (target == null) {
    //        if (!searchForPlayer) {
    //            searchForPlayer = true;
    //            StartCoroutine(findPlayer());
    //            return true;
    //        }
    //        return false;
    //    }
    //    return true;
    //}

    //// Find player
    //public IEnumerator findPlayer() {
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
    //    if (player == null) {
    //        yield return new WaitForSeconds(0.5f);
    //        StartCoroutine(findPlayer());
    //    } else {
    //        target = player.transform;            
    //    }
    //}
}
