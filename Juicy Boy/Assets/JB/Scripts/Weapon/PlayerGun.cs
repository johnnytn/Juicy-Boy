using UnityEngine;
using System.Collections;

public class PlayerGun : Gun {

    // Use this for initialization
    void Awake() {
        firePoint = transform.FindChild("FirePoint");
        if (firePoint == null) {
            Debug.LogError("Fire error! No FirePoint?");
        }
    }

    // Update is called once per frame
    void Update() {
        action();
    }

    private void action() {
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        if (fireRate == 0) {
            if (Input.GetButtonDown("Fire1")) {
                shoot(firePointPosition,mousePosition);
            }
        } else {
            if (Input.GetButton("Fire1") && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                shoot(firePointPosition,mousePosition);
            }
        }
    }
}
