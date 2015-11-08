using UnityEngine;

public class Enemy : Character {

    static public int difficultModifier = 1;

    public Transform deathParticles;
    public float shakeAmt = 0.1f;
    public float shakeLenght = 0.1f;
    public float damage = 40;

    void Start() {
        increaseDifficult();
        init(1);
        updateStatusIndicator();
        if (deathParticles == null) {
            Debug.Log("NO DEATH PARTICLES ON ENEMY");
        }
    }

    void OnCollisionEnter2D(Collision2D colInfo) {
        Player player = colInfo.collider.GetComponent<Player>();
        if (player != null) {
            player.damagePlayer(damage);
            damageEnemy(999);
        }
    }

    public void damageEnemy(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            GameMaster.killEnemy(this);
        }
        updateStatusIndicator();
    }

    private void increaseDifficult() {
        this.maxHealth *= difficultModifier;
        this.damage *= 1 + (difficultModifier / 2);
    }
}
