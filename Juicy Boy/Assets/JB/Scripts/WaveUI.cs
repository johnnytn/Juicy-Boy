using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

    [SerializeField]
    private WaveSpawner waveSpawner;

    [SerializeField]
    private Animator waveAnimator;

    [SerializeField]
    private Text waveCountDownText;

    [SerializeField]
    private Text waveCountText;

    private SpawnState previousState;

    // Use this for initialization
    void Start() {
        if (waveSpawner == null || waveAnimator == null || waveCountDownText == null || waveCountText == null) {
            Debug.LogError("No component referenced");
        }
    }

    // Update is called once per frame
    void Update() {
        switch (waveSpawner.State) {
            case SpawnState.COUNTING:
                {
                    updateCountingUI();
                    break;
                }
            case SpawnState.SPAWNING:
                {
                    updateSpawningUI();
                    break;
                }
        }
        previousState = waveSpawner.State;
    }

    void updateCountingUI() {
        if (previousState != SpawnState.COUNTING) {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountDown", true);
        }
        waveCountDownText.text = ((int)waveSpawner.WaveCountDown).ToString();
    }

    void updateSpawningUI() {
        if (previousState != SpawnState.SPAWNING) {
            waveAnimator.SetBool("WaveIncoming", true);
            waveAnimator.SetBool("WaveCountDown", false);
            waveCountText.text = (waveSpawner.WaveLabel).ToString();
        }
    }
}
