using UnityEngine;
using System.Collections;

public class Player : Character {

    void Start() {
        init(0);
        updateStatusIndicator();
    }

    void Update() {
        fallDeath();
    }

    public void damagePlayer(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            GameMaster.killPlayer(this);
        }
        updateStatusIndicator();
    }

    private void fallDeath() {
        if (transform.position.y <= -20) {
            damagePlayer(Mathf.Infinity);
        }
    }

    /* Old type of player stats

    [System.Serializable] //To make a class definition visible on unity
    public class PlayerStats {
        public float maxHealth = 100f;
        private float _currentHealth;
        public float currentHealth {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0f, maxHealth); }
        }

        public void init() {
            currentHealth = maxHealth;
        }
    }
    */
}
