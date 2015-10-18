using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public Camera mainCam;

    private float shakeAmount = 0;

    void Awake() {
        if (mainCam == null) {
            mainCam = Camera.main;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            shake(0.1f, 0.2f);
        }
    }

    public void shake(float amt, float lenght) {
        shakeAmount = amt;
        InvokeRepeating("doShake", 0, 0.01f);
        Invoke("stopShake", lenght);
    }

    private void doShake() {
        if (shakeAmount > 0) {
            Vector3 camPos = mainCam.transform.position;
            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }

    private void stopShake() {
        CancelInvoke("doShake");
        mainCam.transform.localPosition = Vector3.zero;
    }
}
