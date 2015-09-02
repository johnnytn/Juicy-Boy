using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Transform bulletTrailPrefab;
	private Transform firePoint;
	public LayerMask whatToHit; 	

	public float fireRate = 0;
	public float damage = 10;
	private float timeToFire = 0;
	private float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;


	// Use this for initialization
	void Start () {
		firePoint = transform.FindChild("FirePoint");
		if (firePoint == null) {
			Debug.LogError("Fire error! No FirePoint?");
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if(Input.GetButtonDown("Fire1")) {
				shoot();
			}
		} else {
			if (Input.GetButton("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				shoot();
			}
		}
	
	}

	void shoot() {
		Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
		                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		RaycastHit2D hit = Physics2D.Raycast(firePointPosition, (mousePosition - firePointPosition), 100, whatToHit);
		if (Time.time >= timeToSpawnEffect) {
			effect ();
			timeToSpawnEffect = Time.time * 1/effectSpawnRate;
		}

		Debug.DrawLine(firePointPosition,mousePosition);
	}

	void effect() {
		Instantiate(bulletTrailPrefab,firePoint.position, firePoint.rotation);
	}
}
