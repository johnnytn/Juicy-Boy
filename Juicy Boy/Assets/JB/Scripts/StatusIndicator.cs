using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;

    void Awake() {
        if (healthBarRect == null || healthText == null) {
            Debug.LogError("No health bar or text  object");
        }
    }

    public void setHealth(float current, float max) {
        float value = current / max;

        healthBarRect.localScale = new Vector3(value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = current + "/" + max + " HP";
    }

}
