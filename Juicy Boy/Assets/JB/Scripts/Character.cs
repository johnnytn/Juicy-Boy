using UnityEngine;

public class Character : MonoBehaviour {

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    /**
    *  Defines the type of character:
    *   player = 0;
    *   Enemy = 1;
    */
    public int type;
    public float maxHealth = 100f;
    private float _currentHealth;
    public float currentHealth {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Clamp(value, 0f, maxHealth); }
    }

    public void init(int type) {
        currentHealth = maxHealth;
        this.type = type;
    }

    public void damageCharacter(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            if (this.type == 0) {
                Player player = (Player)this;
                GameMaster.killPlayer(player);
            } else {
                Enemy enemy = (Enemy)this;
                GameMaster.killEnemy(enemy);
            }
        }
        updateStatusIndicator();
    }

    public void updateStatusIndicator() {
        StatusIndicator statusIndicator = getStatusIndicator();
        if (statusIndicator != null) {
            statusIndicator.setHealth(currentHealth, maxHealth);
        }
    }

    public void setStatusIndicator(StatusIndicator statusIndicator) {
        this.statusIndicator = statusIndicator;
    }

    public StatusIndicator getStatusIndicator() {
        return this.statusIndicator;
    }

}
