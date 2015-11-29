using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {


    public Transform bulletTrailPrefab;
    public Transform muzzleFlashPreFab;
    public Transform impactParticlesPrefab;
    [HideInInspector]
    public Transform firePoint;
    public LayerMask whatToHit;

    public float fireRate = 0;
    public float damage = 10;
    public float timeToFire = 0;
    [HideInInspector]
    public float timeToSpawnEffect = 0;
    [HideInInspector]
    public float effectSpawnRate = 10;
    // Handle camera shaking
    public float camShakeAmt = 0.05f;
    public float camShakeLenght = 0.1f;
    CameraShake camShake;

    void Start() {
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if (camShake == null) {
            Debug.LogError("NO CAM SHAKE!");
        }
    }


    // Basic shooting method
    public void shoot(Vector2 firePointPosition, Vector2 targetPosition) {
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, (targetPosition - firePointPosition), 100, whatToHit);

        if (hit.collider != null) {
            Character character = hit.collider.GetComponent<Character>();
            if (character != null) {
                character.damageCharacter(damage);
                Debug.Log("We hit" + hit.collider.name + " for" + damage);
            }
        }
        // Check and call effects
        checkEffect(firePointPosition, targetPosition, hit);
    }

    // Check if it's time to show some effects
    private void checkEffect(Vector2 firePointPosition, Vector2 targetPosition, RaycastHit2D hit) {
        // Verify if it's time to spawn the bullet effect
        if (Time.time >= timeToSpawnEffect) {
            Vector3 hitPos;
            Vector3 hitNormal;
            if (hit.collider == null) {
                hitPos = (targetPosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            } else {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }
            // Call effects
            effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time * 1 / effectSpawnRate;
        }
    }


    // Initiate the bullet effects
    private void effect(Vector3 hitPos, Vector3 hitNormal) {
        bulletTrail(hitPos, hitNormal);
        if (muzzleFlashPreFab != null) {
            muzzleFlash();
            camShake.shake(camShakeAmt, 0.2f);
        }
    }

    // Instantiate and destroy the bulletTrail
    private void bulletTrail(Vector3 hitPos, Vector3 hitNormal) {
        Transform trail = Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lineRenderer = trail.GetComponent<LineRenderer>();

        // Move de trail
        if (lineRenderer != null) {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitPos);
        }
        Destroy(trail.gameObject, 0.04f);

        if (hitNormal != new Vector3(9999, 9999, 9999)) {
            Transform impactParticles = Instantiate(impactParticlesPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
            Destroy(impactParticles.gameObject, 1f);
        }
    }

    // Instantiate and destroy the muzzleflash
    private void muzzleFlash() {
        Transform clone = Instantiate(muzzleFlashPreFab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;

        float size = Random.Range(0.6f, 0.9F);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, camShakeLenght);
    }
}
